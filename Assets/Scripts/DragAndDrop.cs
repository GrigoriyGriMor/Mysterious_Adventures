using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// ГОВНОскрипт  весит на инвентаре
/// </summary>

enum StateDragAndDrop
{
    None,
    Selected,
    Drag,
    EndSelected,
    EndDrag
}

public class DragAndDrop : MonoBehaviour
{
    [Header("Ссылка на класс ItemAction из инвентаря")]
    private ItemAction itemAction;

    [Header("Ссылка на класс ScriptInventory из инвентаря")]
    private ScriptInventory scriptInventory;

    [Header("Ссылка на dragAndDropObject из инвентаря")]
    [SerializeField]
    private Image dragAndDropObject;

    //Флаг выбранного обьекта
    // [HideInInspector]
    public bool isSelectedItem;

    //имя выбранного обьекта
    // [HideInInspector]
    public string nameSelectedItem;

    [SerializeField]
    private Image currentImageSlot; // текущий выбронный слот

    private StateDragAndDrop stateDragAndDrop; // состояние дрыг и прыг

    private void Start()
    {
        scriptInventory = GetComponent<ScriptInventory>();
        itemAction = GetComponent<ItemAction>();
    }

    private void Update()
    {
        if (Input.GetMouseButton(0))   // чекаем мышь
        {
            if (stateDragAndDrop == StateDragAndDrop.None)
            {
                FindIDItem();  
            }

            if (stateDragAndDrop == StateDragAndDrop.Selected)
            {
                DragObject();
            }
        }
        else
        {
            if (stateDragAndDrop == StateDragAndDrop.Selected)
            {
                NotActiveDragAndDrop();
            }
        }
    }

    /// <summary>
    /// ищем в слотах обьект по имени
    /// </summary>
    private void FindIDItem()
    {
        int index = 0;
        if (isSelectedItem && scriptInventory.arrayItems.Count > 0)
        {
            foreach (Image imageSlot in scriptInventory.arrayShow)
            {
                if (imageSlot.sprite.name == nameSelectedItem)
                {
                    currentImageSlot = imageSlot;
                    ActiveDragAndDropObject();
                    Debug.Log("Index array " + index);
                    scriptInventory.idSelectedItem = scriptInventory.arrayItems[index].id;
                    break;
                }
                index++;
            }
        }
    }

    /// <summary>
    /// Берем обьект из инвентаря
    /// </summary>
    private void ActiveDragAndDropObject()
    {
        dragAndDropObject.gameObject.SetActive(true);
        dragAndDropObject.sprite = currentImageSlot.sprite;
        currentImageSlot.gameObject.SetActive(false);
        currentImageSlot.sprite = scriptInventory.defualtImage;
        stateDragAndDrop = StateDragAndDrop.Selected;
        scriptInventory.ShowSlots();
    }

    /// <summary>
    /// Возвращаем обратно в инвентарь
    /// </summary>
    private void NotActiveDragAndDrop()
    {
        currentImageSlot.gameObject.SetActive(true);
        dragAndDropObject.gameObject.SetActive(false);
        stateDragAndDrop = StateDragAndDrop.None;
        nameSelectedItem = "";
        scriptInventory.ShowSlots();

    }

    /// <summary>
    /// Перемещение обьекта  из инвентаря
    /// </summary>
    private void DragObject()
    {
        dragAndDropObject.transform.position = Input.mousePosition;
    }



}
