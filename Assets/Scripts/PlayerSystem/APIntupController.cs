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

    public void InputActive()//сюда идут ссылки из объекта на сцене "InputSystem"
    {
        if (itemInHand != null)// если мы держим какой либо итем из инвентаря в руке, то аннулируем его использование(тк игрок ткнул повторно в другую точку, а должен был перетащить предмет)
        {
            itemInHand.CancelItemUse();
            itemInHand = null;
        }

        if (!GameStateController.Instance.gameIsPlayed) return;

        drag = true;

        if (infoCoroutine != null)
            StopCoroutine(infoCoroutine);

        infoCoroutine = StartCoroutine(InfoTimer());// определитель зажатия на объекте. (если игрок держит объект зажатым более 1.5 сек, то выводим подсказку)
    }

    public void InputDeactive()//сюда идут ссылки из объекта на сцене "InputSystem"
    {
        if (!GameStateController.Instance.gameIsPlayed) return;

        drag = false;

        ClickData();//класс обработки нажатия

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
#if UNITY_EDITOR
        clickPos = Mouse.current.position.ReadValue();
#elif UNITY_ANDROID
        clickPos = Touchscreen.current.position.ReadValue();
#endif

        clickVisual.gameObject.GetComponent<RectTransform>().position = clickPos;
        clickVisual.SetTrigger("Start");

        RaycastHit2D hit;
        hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(clickPos), Vector2.zero);
      
        if (hit.collider != null)
        {
            switch (hit.collider.GetComponent<MonoBehaviour>())
            {
                case APInteractbleObjController _obj:
                    if (hit.collider.GetComponent<APInteractbleObjController>().needUse)//если ИО
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
                  break;

                case APItemController _obj:
                    if (itemInHand != null)
                    {
                        itemInHand.CancelItemUse();
                        itemInHand = null;
                        return;
                    }

                    player.SetNewMoveTarget(hit.point, hit.collider.GetComponent<APItemController>());
                    break;

                case EnlargedObject _obj:
                    if (itemInHand != null)
                    {
                        itemInHand.CancelItemUse();
                        itemInHand = null;
                        return;
                    }

                    player.SetNewMoveTarget(hit.point, hit.collider.GetComponent<EnlargedObject>());
                    break;

                default:
                    if (itemInHand != null)
                    {
                        itemInHand.CancelItemUse();
                        itemInHand = null;
                        return;
                    }

                    player.SetNewMoveTarget(hit.point);
                    break;
            }
        }
    }

    private IEnumerator InfoTimer()
    {
        yield return new WaitForSeconds(timeForObjInfoReqwest);

#if UNITY_ANDROID
        clickPos = Touchscreen.current.position.ReadValue();
#elif UNITY_EDITOR
        clickPos = Mouse.current.position.ReadValue();
#endif
        RaycastHit2D hit;
        hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(clickPos), Vector2.zero);

        if (hit.collider != null && hit.collider.GetComponent<AnswerController>())
            hit.collider.GetComponent<AnswerController>().OnReuestObjInfo(player);
    }
}