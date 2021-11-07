using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ������ �������� ��  ��
/// ������ ���� Item �� �����
/// </summary>
public class CheckItem : MonoBehaviour
{
    [HideInInspector]
    public bool isItem;
    [HideInInspector]
    public GameObject currentItem;

    [Header("��������� �� ��������")]
    public float distanceToItem;

    

    /// <summary>
    /// ������ ��������� �� ����� �����
    /// </summary>
    /// <param name="collision"></param>
    //private void OnTriggerEnter2D(Collider2D collision)
    //{
    //  //  if (collision.tag == "Item")
    //  //  {
    //        isItem = true;
    //        currentItem = collision.gameObject;
    //   // }
    //}

    /// <summary>
    /// ������ ��������� �� ����� �����
    /// </summary>
    /// <param name="collision"></param>
    //private void OnTriggerExit2D(Collider2D collision)
    //{
    //  //  if (collision.tag == "Item")
    //  //  {
    //        isItem = false;
    //        currentItem = null;
    //  //  }
    //}




    /// <summary>
    /// ���������� true ���� ��� �������
    /// </summary>
    /// <returns></returns>
    public bool GetIsItem()
    {
        return isItem;
    }

    /// <summary>
    /// ���������� GameObject
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

    /// <summary>
    /// ������� ID ��������
    /// </summary>
    /// <returns></returns>
    public int GetIdItem()
    {
        int currentId = -1;

        if (isItem)
        {
            currentId = currentItem.GetComponent<Item>().id;

        }
        return currentId;
    }

    /// <summary>
    /// ������� ��������� ���������� ����� ��� ���� ��������
    /// </summary>
    /// <returns></returns>
    public bool GetIsActivePoint()
    {
        return currentItem.GetComponent<PointUseItem>().isActivePoint;
    }

    /// <summary>
    /// ����� ��������� ���������� ����� ��� ���� ��������
    /// </summary>
    /// <param name="isActive"></param>
    public void SetIsActivePoint(bool isActive)
    {
         currentItem.GetComponent<PointUseItem>().isActivePoint = isActive;
    }

    /// <summary>
    /// ���������� ������ ID ��������� ��� ������������� �� �����
    /// </summary>
    /// <returns></returns>
    public List<int> GetIDPointUseItem()
    {
        List<int> getIDPointUseItem = new List<int>() { -1 }; // ������ id

        if (isItem)
        {
            getIDPointUseItem = currentItem.GetComponent<PointUseItem>().id;
        }
        return getIDPointUseItem;
    }

    /// <summary>
    /// ������� true ���� ������������ �������������
    /// </summary>
    /// <returns></returns>
    public bool GetMultipleUse()
    {
        return currentItem.GetComponent<PointUseItem>().multipleUse;
    }


    /// <summary>
    /// ������� Tag ��������
    /// </summary>
    /// <returns></returns>
    public string GetTagItem()
    {
        string currentTag = "";

        if (isItem)
        {
            currentTag = currentItem.tag;
        }

        return currentTag;
    }

    /// <summary>
    /// "������ �� ������ �������
    /// </summary>
    /// <returns></returns>
    public Animator GetAnimatorEvent()
    {
        Animator animatorEvent = currentItem.GetComponent<PointUseItem>().animatorEvent;

        return animatorEvent;
    }

}
