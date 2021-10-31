using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointUseItem : MonoBehaviour
{
    /// <summary>
    /// ����� �������� �� ������ ����� ������������� �����
    /// </summary>
    [Header("��� ����� ������������� ��������")]
    public string namePointUseItem;

    [Header("ID �������� ��� �������������")]
    public List<int> id;

    [Header("�� ����� ������� ��� ���������")]
    [SerializeField]
    public bool needNotItem;

    [Header("������������ �������������")]
    [SerializeField]
    public bool multipleUse;

    [Header("������ �� ������ �������")]
    public Animator animatorEvent;

    ///// <summary>
    ///// ���������� ������ ID ��������� ��� �������������
    ///// </summary>
    ///// <returns></returns>
    //public List<int> GetIDPointUseItem()
    //{
    //    List<int> getIDPointUseItem = new List<int>() { -1 }; // ������ id

    //    getIDPointUseItem = id;   //.GetComponent<PointUseItem>().id;

    //    return getIDPointUseItem;
    //}


    ///// <summary>
    ///// ������� true ���� �� ����� ������� ��� ���������
    ///// </summary>
    ///// <returns></returns>
    //public bool NeedNotItem()
    //{
    //    return needNotItem;
    //}

    ///// <summary>
    ///// ������� true ���� ������������ �������������
    ///// </summary>
    ///// <returns></returns>
    //public bool MultipleUse()
    //{
    //    return multipleUse;
    //}

    ///// <summary>
    ///// "������ �� ������ �������
    ///// </summary>
    ///// <returns></returns>
    //public Animator GetAnimatorEvent()
    //{
    //    return animatorEvent;
    //}

}
