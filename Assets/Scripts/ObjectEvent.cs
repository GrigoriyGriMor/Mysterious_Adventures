using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ObjectEvent : MonoBehaviour
{
    /// <summary>
    /// Класс вешается на обьект события
    /// </summary>

    //[HideInInspector]
    public bool isActivateEvent;  // флаг активации

    [Header("ID для активации события")]
    public int id;

    /// <summary>
    /// Активация события
    /// </summary>
    public void ActivateEvent()
    {
        if (!isActivateEvent)
        {
            CurrentEvent();
            isActivateEvent = true;
        }
    }

    /// <summary>
    /// Метод для события
    /// </summary>
    public void CurrentEvent()
    {

        Debug.Log("Event done");

    }
}