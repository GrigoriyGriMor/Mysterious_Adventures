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
      //  if (collision.tag == "Item")
      //  {
            isItem = true;
            currentItem = collision.gameObject;
       // }
    }

    /// <summary>
    /// ������ ��������� �� ����� �����
    /// </summary>
    /// <param name="collision"></param>
    private void OnTriggerExit2D(Collider2D collision)
    {
      //  if (collision.tag == "Item")
      //  {
            isItem = false;
            currentItem = null;
      //  }
    }

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

    public List<int> GetIDPointUseItem()
    {
        List<int> getIDPointUseItem = new List<int>() { -1 }; // ������ id

        if (isItem)
        {
            getIDPointUseItem = currentItem.GetComponent<PointUseItem>().id;
        }
        return getIDPointUseItem;
    }


    public bool GetMultipleUse()
    {
        return currentItem.GetComponent<PointUseItem>().multipleUse;
    }

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
