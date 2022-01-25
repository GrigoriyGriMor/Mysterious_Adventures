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
    [Header("������ ������ ��������� � ���������")]
    //[SerializeField]
    public List<Image> arrayShow;

    [Header("������ ���������")]
    // [HideInInspector]
    public List<Item> arrayItems;

    //[HideInInspector]
    public Sprite defualtImage;

    [HideInInspector]
    public Image dragImage; // ������ ������� �������

    [HideInInspector]
    public int idSelectedItem = -1; // id ���������� ������� � ���������

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
                arrayShow[indexArrayShow].sprite = arrayItems[indexArrayShow].GetComponent<SpriteRenderer>().sprite;
            }
            else
            {
                arrayShow[indexArrayShow].sprite = defualtImage;
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
            arrayItems.Add(addItem.GetComponent<Item>());
            ShowSlots();
        }

    }


    /// <summary>
    /// �������� �������� �� ���������
    /// </summary>
    /// <param name="delItemID"></param>
    public void DelItem(int delItemID)
    {
        foreach (Item item in arrayItems)
        {
            if (item.id == delItemID)
            {
                arrayItems.Remove(item);
                //Destroy(delItemID);
                ShowSlots();
                return;
            }
        }
    }

    /// <summary>
    /// ������� ID ���������� � ��������� �������
    /// </summary>
    /// <returns></returns>
    public int GetIdSelectedItem()   
    {
        return idSelectedItem;
    }

}
