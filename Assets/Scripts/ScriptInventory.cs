
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScriptInventory : MonoBehaviour
{
    /// <summary>
    /// ������ �������� �� ���������
    /// ��������� ����������
    /// </summary>
    [Header("Array show slots items")]
    [SerializeField]
    private List<Image> arrayShow;

    [Header("Array Items")]
    [SerializeField]
    private List<GameObject> arrayItems;

    [Header("Class CheckItem")]
    [SerializeField]
    private CheckItem checkItem;

    [Header("Class CheckPointUseItem")]
    [SerializeField]
    private CheckPointUseItem checkPointUseItem;

    void Start()
    {
        ShowSlots();
    }

    /// <summary>
    /// ����������� �������� � ���������
    /// </summary>
    public void ShowSlots()
    {
        for (int indexArrayShow = 0; indexArrayShow < arrayShow.Count; indexArrayShow++)
        {
            if (indexArrayShow < arrayItems.Count)
            {
                arrayShow[indexArrayShow].GetComponent<Image>().sprite = arrayItems[indexArrayShow].GetComponent<SpriteRenderer>().sprite;
            }
            else
            {
                arrayShow[indexArrayShow].GetComponent<Image>().sprite = null;
            }
        }
    }

    /// <summary>
    /// ���������� �������� � ���������
    /// </summary>
    /// <param name="addItem"></param>
    public void AddItem(GameObject addItem)
    {
        if (arrayItems.Count < arrayShow.Count)
        {
            arrayItems.Add(addItem);
            ShowSlots();
        }
    }


    /// <summary>
    /// �������� �������� �� ���������
    /// </summary>
    /// <param name="delItem"></param>
    public void DelItem(GameObject delItem)
    {
        arrayItems.Remove(delItem);
        ShowSlots();
    }

    /// <summary>
    /// ������ ACTION
    /// ������ � ������������� �����
    /// </summary>
    public void ButtonAction()
    {   
        //������ �������� � �������� �� � ������
        GameObject getItem = checkItem.GetItem();

        if (getItem)
        {
            AddItem(getItem);
            getItem.SetActive(false);
        }

        // ���������� ID �������� � �������� �� ���������
        int getIDPointUseItem = checkPointUseItem.GetIDPointUseItem();

        if (getIDPointUseItem > -1)
        {
            foreach (GameObject Item in arrayItems)
            {
                if (Item.GetComponent<Item>().id == getIDPointUseItem)
                {
                    Action(getIDPointUseItem);
                    DelItem(Item);
                    break;
                }
            }
        }
    }

    /// <summary>
    /// ������������� �������� 
    /// </summary>
    /// <param name="id"></param>
    public void Action(int id)
    {
        Debug.Log("ACTION id Item = "+ id );
    }
}
