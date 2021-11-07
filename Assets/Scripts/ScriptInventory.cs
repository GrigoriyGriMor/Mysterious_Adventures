using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScriptInventory : MonoBehaviour
{
    /// <summary>
    /// Скрипт вешается на инвентарь
    /// управляет инвентарем
    /// </summary>
    [Header("Массив слотов предметов в инвенторе")]
    //[SerializeField]
    public List<Image> arrayShow;

    [Header("Массив предметов")]
    // [HideInInspector]
    public List<Item> arrayItems;

    //[HideInInspector]
    public Sprite defualtImage;

    [HideInInspector]
    public Image dragImage; // обьект который дрыгаем

    [HideInInspector]
    public int idSelectedItem = -1; // id выбраннова обьекта в инвентаре

    void Start()
    {
        ShowSlots();
    }

    /// <summary>
    /// Отображение предмета в инвентаре
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
    /// Добавление предмета в инвентарь
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
    /// Удаление предмета из инвентаря
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
    /// возврат ID выбранного в инвентаре обьекта
    /// </summary>
    /// <returns></returns>
    public int GetIdSelectedItem()   
    {
        return idSelectedItem;
    }

}
