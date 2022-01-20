using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class APIntupController : MonoBehaviour
{
    private static APIntupController instance;
    public static APIntupController Instance => instance;

    public APPlayerController player;
    [SerializeField] private Animator clickVisual;
    private Vector2 clickPos;

    private bool drag = false;

    [SerializeField] private float timeForObjInfoReqwest = 1;
    private Coroutine infoCoroutine;

    private InventoryCard itemInHand;

    private void Awake()
    {
        instance = this;
    }

    public void InputActive()
    {
        if (itemInHand != null)
        {
            itemInHand.CancelItemUse();
            itemInHand = null;
        }

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

    public void InputDeactive(InventoryCard _itemInHand = null)
    {
        if (!GameStateController.Instance.gameIsPlayed) return;

        drag = false;

        if (itemInHand == null) itemInHand = _itemInHand;
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
            if (hit.collider.GetComponent<APInteractbleObjController>() && hit.collider.GetComponent<APInteractbleObjController>().needUse)//если ИО
            {
                if (itemInHand != null)// если в руке есть итем
                {
                    if (hit.collider.GetComponent<APInteractbleObjController>().NeedItem(itemInHand))// нужен ли итем для активации ИО? если да, то соответствуют ли ID
                    {
                        player.SetNewMoveTarget(hit.point, hit.collider.GetComponent<APInteractbleObjController>());
                        itemInHand.CorrectItemUse();
                    }
                    else
                    {
                        itemInHand.CancelItemUse();
                        itemInHand = null;
                    }
                }
                else//если в руке нет итема
                {
                    if (!hit.collider.GetComponent<APInteractbleObjController>().NeedItem())//для активации этого ИО точно не нужен итем?
                        player.SetNewMoveTarget(hit.point, hit.collider.GetComponent<APInteractbleObjController>());
                    else
                        player.SetNewMoveTarget(hit.point);
                }
            }
            else
            {
                if (itemInHand != null)
                {
                    itemInHand.CancelItemUse();
                    itemInHand = null;
                    return;
                }

                if (hit.collider.GetComponent<APItemController>())//если итем контроллер
                    player.SetNewMoveTarget(hit.point, hit.collider.GetComponent<APItemController>());
                else
                {
                    if (hit.collider.GetComponent<EnlargedObject>())
                        player.SetNewMoveTarget(hit.point, hit.collider.GetComponent<EnlargedObject>());
                    else
                        player.SetNewMoveTarget(hit.point);
                }
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
