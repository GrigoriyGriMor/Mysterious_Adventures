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
    private GameObject currentPointUseItem;


    /// <summary>
    /// ������ ��������� �� ����� �����
    /// </summary>
    /// <param name="collision"></param>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "PointUseItem")
        {
            isPointUseItem = true;
            currentPointUseItem = collision.gameObject;
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
    /// ���������� ID ����� ������������� ����
    /// </summary>
    /// <returns></returns>
    public int GetIDPointUseItem()
    {
        int getIDPointUseItem = -1;

        if (isPointUseItem)
        {
            getIDPointUseItem = currentPointUseItem.GetComponent<PointUseItem>().id;
        }

        return getIDPointUseItem;
    }
}
