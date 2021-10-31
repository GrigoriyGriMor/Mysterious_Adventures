using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonAction : MonoBehaviour
{
    private ScriptInventory scriptInventory;

    [Header("Class CheckItem")]
    [SerializeField]
    private CheckItem checkItem;
    private readonly object Action;

    //[Header("Class CheckPointUseItem")]
    //[SerializeField]
    //private CheckPointUseItem checkPointUseItem;

    private void Start()
    {
        scriptInventory = GetComponent<ScriptInventory>();
    }

    /// <summary>
    /// Кнопка ACTION
    /// Подбор и использование вещей
    /// </summary>
    public void ActionButton()
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
        GameObject getItem = checkItem.GetItem();

        if (getItem)
        {
            scriptInventory.AddItem(getItem);
            getItem.SetActive(false);
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
            foreach (GameObject Item in scriptInventory.arrayItems)
            {
                foreach (int id in getIDPointUseItem)
                {
                    if (Item.GetComponent<Item>().id == id)
                    {
                        ActionPoint();
                        if (!checkItem.GetMultipleUse())
                        {
                           scriptInventory.DelItem(Item);  // удаление из инвентаря
                        }
                        return;
                    }
                }
            }
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
    /// <param name="id"></param>
    public void ActionPoint()
    {
        checkItem.GetAnimatorEvent().SetTrigger("Activate");
    }
}
