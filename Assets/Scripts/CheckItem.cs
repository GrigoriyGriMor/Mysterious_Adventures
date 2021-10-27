using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckItem : MonoBehaviour
{
    /// <summary>
    /// Скрипт вешается на  ГГ
    /// Чекает вещи Item на сцене
    /// </summary>
    //[HideInInspector]
    public bool isItem;
    private GameObject currentItem;

    /// <summary>
    /// чекает коллайдер ГГ когда вошел
    /// </summary>
    /// <param name="collision"></param>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Item")
        {
            isItem = true;
            currentItem = collision.gameObject;
        }
    }

    /// <summary>
    /// чекает коллайдер ГГ когда вышел
    /// </summary>
    /// <param name="collision"></param>
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Item")
        {
            isItem = false;
            currentItem = null;
        }
    }

    /// <summary>
    /// возвращает GameObject вещи
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


}
