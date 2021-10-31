using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPointUseItem : MonoBehaviour
{
    /// <summary>
    /// Скрипт вешается на  ГГ
    /// Чекает точки использования предметов на сцене
    /// </summary>
    //[HideInInspector]
    public bool isPointUseItem;
    //private GameObject currentPointUseItem;
    private PointUseItem currentPointUseItem;


    /// <summary>
    /// чекает коллайдер ГГ когда вошел
    /// </summary>
    /// <param name="collision"></param>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "PointUseItem")
        {
            isPointUseItem = true;
            currentPointUseItem = collision.GetComponent<PointUseItem>();
        }
    }

    /// <summary>
    /// чекает коллайдер ГГ когда вышел
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
    /// возвращает true если это точка использования предмета
    /// </summary>
    /// <returns></returns>
    public bool GetIsPointUseItem()
    {
        return isPointUseItem;
    }

    /// <summary>
    /// возвращает массив ID предметов для использования
    /// </summary>
    /// <returns></returns>
    public List<int> GetIDPointUseItem()
    {
        List<int> getIDPointUseItem = new List<int>() { -1 }; // пустой id

        if (isPointUseItem)
        {
            getIDPointUseItem = currentPointUseItem.id;   //.GetComponent<PointUseItem>().id;
        }
        return getIDPointUseItem;
    }


    /// <summary>
    /// возврат true если не нужен предмет для активации
    /// </summary>
    /// <returns></returns>
    public bool NeedNotItem()
    {
        return currentPointUseItem.needNotItem;
    }

    /// <summary>
    /// возврат true если Многоразовое использование
    /// </summary>
    /// <returns></returns>
    public bool MultipleUse()
    {
        return currentPointUseItem.multipleUse;
    }

    /// <summary>
    /// "Ссылка на обьект события
    /// </summary>
    /// <returns></returns>
    public ObjectEvent GetObjectEvent()
    {
        return null; // currentPointUseItem.objectEvent;
    }


}
