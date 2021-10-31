using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPointUseItem : MonoBehaviour
{
    /// <summary>
    /// ������ �������� ��  ��
    /// ������ ����� ������������� ��������� �� �����
    /// </summary>
    //[HideInInspector]
    public bool isPointUseItem;
    //private GameObject currentPointUseItem;
    private PointUseItem currentPointUseItem;


    /// <summary>
    /// ������ ��������� �� ����� �����
    /// </summary>
    /// <param name="collision"></param>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "PointUseItem")
        {
            isPointUseItem = true;
            currentPointUseItem = collision.GetComponent<PointUseItem>();
        }
    }

    /// <summary>
    /// ������ ��������� �� ����� �����
    /// </summary>
    /// <param name="collision"></param>
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "PointUseItem")
        {
            isPointUseItem = false;
            currentPointUseItem = null;
        }
    }


    /// <summary>
    /// ���������� true ���� ��� ����� ������������� ��������
    /// </summary>
    /// <returns></returns>
    public bool GetIsPointUseItem()
    {
        return isPointUseItem;
    }

    /// <summary>
    /// ���������� ������ ID ��������� ��� �������������
    /// </summary>
    /// <returns></returns>
    public List<int> GetIDPointUseItem()
    {
        List<int> getIDPointUseItem = new List<int>() { -1 }; // ������ id

        if (isPointUseItem)
        {
            getIDPointUseItem = currentPointUseItem.id;   //.GetComponent<PointUseItem>().id;
        }
        return getIDPointUseItem;
    }


    /// <summary>
    /// ������� true ���� �� ����� ������� ��� ���������
    /// </summary>
    /// <returns></returns>
    public bool NeedNotItem()
    {
        return currentPointUseItem.needNotItem;
    }

    /// <summary>
    /// ������� true ���� ������������ �������������
    /// </summary>
    /// <returns></returns>
    public bool MultipleUse()
    {
        return currentPointUseItem.multipleUse;
    }

    /// <summary>
    /// "������ �� ������ �������
    /// </summary>
    /// <returns></returns>
    public ObjectEvent GetObjectEvent()
    {
        return null; // currentPointUseItem.objectEvent;
    }


}
