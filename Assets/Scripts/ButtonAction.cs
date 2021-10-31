using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonAction : MonoBehaviour
{
    private ScriptInventory scriptInventory;

    [Header("Class CheckItem")]
    [SerializeField]
    private CheckItem checkItem;

    [Header("������ �� �������� ��")]
    [SerializeField]
    private Animator animator;

    //[Header("Class CheckPointUseItem")]
    //[SerializeField]
    //private CheckPointUseItem checkPointUseItem;

    [Header("����� ����� ��� ��������")]
    [SerializeField]
    private int countScene;

    private void Start()
    {
        scriptInventory = GetComponent<ScriptInventory>();
    }

    /// <summary>
    /// ������ ACTION
    /// ������ � ������������� �����
    /// </summary>
    public void ActionButton()
    {
        switch (checkItem.GetTagItem())
        {
            case "Item":
                TakeItem();
                break;

            case "PointUseItem":
                ActionPointUseItem();
                break;

            case "Trigger":
                ActionTrigger();
                break;

            default:
                break;
        }
    }

    /// <summary>
    /// ������ �������� � �������� ��� � ������
    /// </summary>
    private void TakeItem()
    {
        Debug.Log("TakeItem");
        GameObject getItem = checkItem.GetItem();

        if (checkItem.GetIdItem() == 10)
        {
            Debug.Log("Load Scene");
            SceneManager.LoadScene(countScene);
        }

        if (getItem)
        {
            scriptInventory.AddItem(getItem);
            getItem.SetActive(false);

            animator.SetTrigger("Selection"); // ��������� ��������
        }

        
    }

    /// <summary>
    /// ������������� ��������
    /// </summary>
    private void ActionPointUseItem()
    {
        Debug.Log("PointUseItem");

        List<int> getIDPointUseItem = checkItem.GetIDPointUseItem();

        // ���������� ID �������� 
        if (getIDPointUseItem != null)
        {
            foreach (GameObject Item in scriptInventory.arrayItems)
            {
                foreach (int id in getIDPointUseItem)
                {
                    if (Item.GetComponent<Item>().id == id)
                    {

                        ActionPoint();

                        if (!checkItem.GetMultipleUse())
                        {
                            scriptInventory.DelItem(Item);  // �������� �� ���������
                        }

                        return;
                    }
                }
            }
        }
    }


    /// <summary>
    /// ������������ ������
    /// </summary>
    private void ActionTrigger()
    {
        Debug.Log("Trigger");

    }

    /// <summary>
    /// ������������� �������� 
    /// </summary>
    /// <param name="id"></param>
    public void ActionPoint()
    {
        animator.SetTrigger("Selection");  // ��������� ��������

        checkItem.GetAnimatorEvent().SetTrigger("Activate");
    }
}