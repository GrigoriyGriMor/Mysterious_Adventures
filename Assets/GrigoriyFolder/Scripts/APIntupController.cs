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

    public void InputActive()
    {
        drag = true;
        ClickData();
    }

    public void InputDeactive()
    {
        drag = false;
    }

    private void ClickData()
    {


#if UNUTY_ANDROID
        clickPos = Touchscreen.current.position.ReadValue();
        clickVisual.gameObject.GetComponent<RectTransform>().position = clickPos;
        clickVisual.SetTrigger("Start");

        RaycastHit hit;
        if (Physics.Raycast(Camera.main.ScreenPointToRay(clickPos), out hit))
            player.SetNewMoveTarget(hit.point);
#else
        clickPos = Mouse.current.position.ReadValue();
        clickVisual.gameObject.GetComponent<RectTransform>().position = clickPos;
        clickVisual.SetTrigger("Start");

        /* RaycastHit hit;
         Physics.Raycast(Camera.main.ScreenPointToRay(clickPos), out hit);*/

        RaycastHit2D hit;
        hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(clickPos), Vector2.zero);

        if (hit.collider != null)
        {
            Debug.Log(hit.collider.name);

            if (hit.collider.GetComponent<APInteractbleObjController>())
                player.SetNewMoveTarget(hit.point, hit.collider.GetComponent<APInteractbleObjController>());
            else
                player.SetNewMoveTarget(hit.point);
        }
#endif
    }







}
