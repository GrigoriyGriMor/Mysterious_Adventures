using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerScript : MonoBehaviour
{
    /// <summary>
    /// ����� �������� �� ������ ������
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
    public ObjectEvent objectEvent;
}
