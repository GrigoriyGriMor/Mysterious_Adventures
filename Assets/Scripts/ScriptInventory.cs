
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
    [SerializeField]
    private List<Image> arrayShow;

    [Header("������ ���������")]
   // [SerializeField]
   // [HideInInspector]
    public List<GameObject> arrayItems;

    

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
        Destroy(delItem);
        ShowSlots();
    }

    
}
