using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

/// <summary>
/// Скрипт весит на инвентаре для обработки использования предметов и точек для предметов
/// </summary>
public class ItemAction : MonoBehaviour
{
    private ScriptInventory scriptInventory;

    [Header("Class CheckItem")]
    [SerializeField]
    private CheckItem checkItem;

    [Header("Ссылка на аниматор ГГ")]
    [SerializeField]
    private Animator animator;

    [Header("Номер сцены для загрузки")]
    [SerializeField]
    private int countScene;

    private GameObject getItem; // предмет подбора

    private bool isCanAction = true;

    private void Start()
    {
        scriptInventory = GetComponent<ScriptInventory>();
    }

    private void Update()
    { // Проверяем  нажатие кнопки мыши
        if (Input.GetMouseButton(0))
        {
            CastRay();
        }
    }

    /// <summary>
    /// Проверяем обьект под мышкой
    /// </summary>
    void CastRay()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 mousePos2D = new Vector2(mousePos.x, mousePos.y);

        RaycastHit2D hit = Physics2D.Raycast(mousePos2D, Vector2.zero);

        if (hit.collider != null)
        {
            if (hit.collider.tag == "Item" || hit.collider.tag == "PointUseItem" || hit.collider.tag == "Trigger")
            {
                float dist = Vector3.Distance(hit.collider.gameObject.transform.position, checkItem.gameObject.transform.position);
                Debug.Log(hit.collider.gameObject.name + " dist " + dist);

                if (dist < checkItem.distanceToItem)
                {
                    checkItem.isItem = true;
                    checkItem.currentItem = hit.collider.gameObject;
                    ActionItem();
                }
            }
            else
            {
                checkItem.isItem = false;
                checkItem.currentItem = null;
            }

        }
    }


    /// <summary>
    /// Кнопка ACTION
    /// Подбор и использование вещей
    /// </summary>
    public void ActionItem()
    {


        switch (checkItem.GetTagItem())
        {
            case "Item":
                TakeItem();
                break;

            case "PointUseItem":
                ActionPointUseItem();
                break;

            case "Trigger":
                ActionTrigger();
                break;

            default:
                break;
        }

    }

    /// <summary>
    /// Подбор предмета и поместим его в массив
    /// </summary>
    private void TakeItem()
    {

        Debug.Log("TakeItem");
        getItem = checkItem.GetItem();

        if (checkItem.GetIdItem() == 10)
        {
            Debug.Log("Load Scene");
            SceneManager.LoadScene(countScene);
        }

        if (getItem && isCanAction)
        {
            getItem.transform.localScale = new Vector3(2.0f, 2.0f, 2.0f);  // увеличиваем обьект

            scriptInventory.AddItem(getItem);

            isCanAction = false;

            float showTime = 0.5f;
            Invoke("NotActiveObject", showTime); // ждем чебы была видна какой он бальшой

            if (animator)
            {
                animator.SetTrigger("Selection"); // Запускаем анимацию

            }
        }


    }

    /// <summary>
    /// Использование предмета
    /// </summary>
    private void ActionPointUseItem()
    {

        Debug.Log("PointUseItem");

        List<int> getIDPointUseItem = checkItem.GetIDPointUseItem();

        // Нахождение ID предмета 
        if (getIDPointUseItem != null)
        {
            //foreach (Item item in scriptInventory.arrayItems)
            // {
            int idItem = scriptInventory.GetIdSelectedItem();
            foreach (int id in getIDPointUseItem)
            {
                if (idItem == id && checkItem.GetIsActivePoint())  // проверяем точка активна
                {
                    checkItem.SetIsActivePoint(false);           // выкл точку

                    isCanAction = false;

                    scriptInventory.dragImage.sprite = scriptInventory.defualtImage;

                    ActionPoint();

                    if (!checkItem.GetMultipleUse())
                    {
                        scriptInventory.DelItem(idItem);  // удаление из инвентаря
                    }

                    return;
                }
            }
            //}
        }
    }


    /// <summary>
    /// Переключение Рычага
    /// </summary>
    private void ActionTrigger()
    {
        Debug.Log("Trigger");

    }


    /// <summary>
    /// Использование предмета 
    /// </summary>
    public void ActionPoint()
    {
        if (animator)
        {
            animator.SetTrigger("Selection");  // Запускаем анимацию
        }
        checkItem.GetAnimatorEvent().SetTrigger("Activate");
        isCanAction = true;
    }


    /// <summary>
    /// Возврвщаем все обратно
    /// </summary>
    private void NotActiveObject()
    {
        bool active = false;
        getItem.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
        getItem.SetActive(active);
        isCanAction = true;
    }


}
