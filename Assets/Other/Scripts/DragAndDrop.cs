using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// �����������  ����� �� ���������
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
    [Header("������ �� ����� ItemAction �� ���������")]
    private ItemAction itemAction;

    [Header("������ �� ����� ScriptInventory �� ���������")]
    private ScriptInventory scriptInventory;

    [Header("������ �� dragAndDropObject �� ���������")]
    [SerializeField]
    private Image dragAndDropObject;

    //���� ���������� �������
    // [HideInInspector]
    public bool isSelectedItem;

    //��� ���������� �������
    // [HideInInspector]
    public string nameSelectedItem;

    [SerializeField]
    private Image currentImageSlot; // ������� ��������� ����

    private StateDragAndDrop stateDragAndDrop; // ��������� ���� � ����

    private void Start()
    {
        scriptInventory = GetComponent<ScriptInventory>();
        itemAction = GetComponent<ItemAction>();
    }

    private void Update()
    {
        if (Input.GetMouseButton(0))   // ������ ����
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
    /// ���� � ������ ������ �� �����
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
                    //Debug.Log("Index array " + index);
                    scriptInventory.idSelectedItem = scriptInventory.arrayItems[index].id;
                    break;
                }
                index++;
            }
        }
    }

    /// <summary>
    /// ����� ������ �� ���������
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
    /// ���������� ������� � ���������
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
    /// ����������� �������  �� ���������
    /// </summary>
    private void DragObject()
    {
        dragAndDropObject.transform.position = Input.mousePosition;
    }



}
