using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


// скрипт весит на предмете
public class PointerClick : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private DragAndDrop dragAndDrop;
    private bool isSelectedItem;
    private string nameSelectedItem;
    private Image imageItem;

    private void Start()
    {
        dragAndDrop = FindObjectOfType(typeof(DragAndDrop)) as DragAndDrop;    //  кешируем
        imageItem = GetComponent<Image>();                                     // компоненты
    }


    /// <summary>
    /// Мыша над обьектом инвентаря
    /// </summary>
    /// <param name="eventData"></param>
    public void OnPointerEnter(PointerEventData eventData)
    {
        isSelectedItem = true;
       // Debug.Log(" Is Selected " + isSelectedItem);
        dragAndDrop.isSelectedItem = isSelectedItem;
        dragAndDrop.nameSelectedItem = imageItem.sprite.name;
    }

    /// <summary>
    /// мыша убежала
    /// </summary>
    /// <param name="eventData"></param>
    public void OnPointerExit(PointerEventData eventData)
    {
        isSelectedItem = false;
      //  Debug.Log(" Is Selected " + isSelectedItem);
        dragAndDrop.isSelectedItem = isSelectedItem;
        dragAndDrop.nameSelectedItem = "";
    }


}
