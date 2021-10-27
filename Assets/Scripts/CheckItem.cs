using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckItem : MonoBehaviour
{
    /// <summary>
    /// ������ �������� ��  ��
    /// ������ ���� Item �� �����
    /// </summary>
    //[HideInInspector]
    public bool isItem;
    private GameObject currentItem;

    /// <summary>
    /// ������ ��������� �� ����� �����
    /// </summary>
    /// <param name="collision"></param>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Item")
        {
            isItem = true;
            currentItem = collision.gameObject;
        }
    }

    /// <summary>
    /// ������ ��������� �� ����� �����
    /// </summary>
    /// <param name="collision"></param>
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Item")
        {
            isItem = false;
            currentItem = null;
        }
    }

    /// <summary>
    /// ���������� GameObject ����
    /// </summary>
    /// <returns></returns>
    public GameObject GetItem()
    {
        GameObject getItem = null;

        if (isItem)
        {
            getItem = currentItem;
        }

        return getItem;
    }


}
