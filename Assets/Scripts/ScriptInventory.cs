
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
    [SerializeField]
    private List<Image> arrayShow;

    [Header("Массив предметов")]
   // [SerializeField]
   // [HideInInspector]
    public List<GameObject> arrayItems;

    

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
                arrayShow[indexArrayShow].GetComponent<Image>().sprite = arrayItems[indexArrayShow].GetComponent<SpriteRenderer>().sprite;
            }
            else
            {
                arrayShow[indexArrayShow].GetComponent<Image>().sprite = null;
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
            arrayItems.Add(addItem);
            ShowSlots();
        }
    }


    /// <summary>
    /// Удаление предмета из инвентаря
    /// </summary>
    /// <param name="delItem"></param>
    public void DelItem(GameObject delItem)
    {
        arrayItems.Remove(delItem);
        Destroy(delItem);
        ShowSlots();
    }

    
}
