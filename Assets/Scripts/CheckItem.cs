using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Скрипт вешается на  ГГ
/// Чекает вещи Item на сцене
/// </summary>
public class CheckItem : MonoBehaviour
{
    [HideInInspector]
    public bool isItem;
    [HideInInspector]
    public GameObject currentItem;

    [Header("Дистанция до предмета")]
    public float distanceToItem;

    

    /// <summary>
    /// чекает коллайдер ГГ когда вошел
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
    /// чекает коллайдер ГГ когда вышел
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
    /// возвращает true если это предмет
    /// </summary>
    /// <returns></returns>
    public bool GetIsItem()
    {
        return isItem;
    }

    /// <summary>
    /// возвращает GameObject
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
    /// Возврат ID предмета
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
    /// возврат состояние активности точки для испл предмета
    /// </summary>
    /// <returns></returns>
    public bool GetIsActivePoint()
    {
        return currentItem.GetComponent<PointUseItem>().isActivePoint;
    }

    /// <summary>
    /// сетим состояние активности точки для испл предмета
    /// </summary>
    /// <param name="isActive"></param>
    public void SetIsActivePoint(bool isActive)
    {
         currentItem.GetComponent<PointUseItem>().isActivePoint = isActive;
    }

    /// <summary>
    /// возвращает массив ID предметов для использования на точке
    /// </summary>
    /// <returns></returns>
    public List<int> GetIDPointUseItem()
    {
        List<int> getIDPointUseItem = new List<int>() { -1 }; // пустой id

        if (isItem)
        {
            getIDPointUseItem = currentItem.GetComponent<PointUseItem>().id;
        }
        return getIDPointUseItem;
    }

    /// <summary>
    /// возврат true если Многоразовое использование
    /// </summary>
    /// <returns></returns>
    public bool GetMultipleUse()
    {
        return currentItem.GetComponent<PointUseItem>().multipleUse;
    }


    /// <summary>
    /// Возврат Tag предмета
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
    /// "Ссылка на обьект события
    /// </summary>
    /// <returns></returns>
    public Animator GetAnimatorEvent()
    {
        Animator animatorEvent = currentItem.GetComponent<PointUseItem>().animatorEvent;

        return animatorEvent;
    }

}
