using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class APIntupController : MonoBehaviour
{
    [SerializeField] private APPlayerController player;
    [SerializeField] private Animator clickVisual;
    private Vector2 clickPos;

    private bool drag = false;

    [SerializeField] private float timeForObjInfoReqwest = 1;
    private Coroutine infoCoroutine;

    public void InputActive()
    {
        if (!GameStateController.Instance.gameIsPlayed) return;

        drag = true;

        if (infoCoroutine != null)
            StopCoroutine(infoCoroutine);

        infoCoroutine = StartCoroutine(InfoTimer());
    }

    public void InputDeactive()
    {
        if (!GameStateController.Instance.gameIsPlayed) return;

        drag = false;

        ClickData();

        if (infoCoroutine != null)
            StopCoroutine(infoCoroutine);
    }

    private void ClickData()
    {
#if UNITY_AN1DROID
        clickPos = Touchscreen.current.position.ReadValue();
        clickVisual.gameObject.GetComponent<RectTransform>().position = clickPos;
        clickVisual.SetTrigger("Start");

        RaycastHit2D hit;
        hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(clickPos), Vector2.zero);

        if (hit.collider != null)
        {
            if (hit.collider.GetComponent<APInteractbleObjController>() && hit.collider.GetComponent<APInteractbleObjController>().needUse)
                player.SetNewMoveTarget(hit.point, hit.collider.GetComponent<APInteractbleObjController>());
            else
                player.SetNewMoveTarget(hit.point);
        }
#else
        clickPos = Mouse.current.position.ReadValue();
        clickVisual.gameObject.GetComponent<RectTransform>().position = clickPos;
        clickVisual.SetTrigger("Start");

        RaycastHit2D hit;
        hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(clickPos), Vector2.zero);

        if (hit.collider != null)
        {
            if (hit.collider.GetComponent<APInteractbleObjController>() && hit.collider.GetComponent<APInteractbleObjController>().needUse)
                player.SetNewMoveTarget(hit.point, hit.collider.GetComponent<APInteractbleObjController>());
            else
            {
                if (hit.collider.GetComponent<APItemController>())
                    player.SetNewMoveTarget(hit.point, hit.collider.GetComponent<APItemController>());
                else
                    player.SetNewMoveTarget(hit.point);
            }
        }
#endif
    }

    private IEnumerator InfoTimer()
    {
        yield return new WaitForSeconds(timeForObjInfoReqwest);

#if UNITY_AND1ROID
        clickPos = Touchscreen.current.position.ReadValue();
#else
        clickPos = Mouse.current.position.ReadValue();
#endif
        RaycastHit2D hit;
        hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(clickPos), Vector2.zero);

        if (hit.collider != null && hit.collider.GetComponent<AnswerController>())
            hit.collider.GetComponent<AnswerController>().OnReuestObjInfo(player);
    }
}
