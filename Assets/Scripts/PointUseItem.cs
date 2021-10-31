using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointUseItem : MonoBehaviour
{
    /// <summary>
    /// Класс вешается на префаб точки использования вещей
    /// </summary>
    [Header("Имя точки использования предмета")]
    public string namePointUseItem;

    [Header("ID предмета для использования")]
    public List<int> id;

    [Header("Не нужен предмет для активации")]
    [SerializeField]
    public bool needNotItem;

    [Header("Многоразовое использование")]
    [SerializeField]
    public bool multipleUse;

    [Header("Ссылка на обьект события")]
    public Animator animatorEvent;

    ///// <summary>
    ///// возвращает массив ID предметов для использования
    ///// </summary>
    ///// <returns></returns>
    //public List<int> GetIDPointUseItem()
    //{
    //    List<int> getIDPointUseItem = new List<int>() { -1 }; // пустой id

    //    getIDPointUseItem = id;   //.GetComponent<PointUseItem>().id;

    //    return getIDPointUseItem;
    //}


    ///// <summary>
    ///// возврат true если не нужен предмет для активации
    ///// </summary>
    ///// <returns></returns>
    //public bool NeedNotItem()
    //{
    //    return needNotItem;
    //}

    ///// <summary>
    ///// возврат true если Многоразовое использование
    ///// </summary>
    ///// <returns></returns>
    //public bool MultipleUse()
    //{
    //    return multipleUse;
    //}

    ///// <summary>
    ///// "Ссылка на обьект события
    ///// </summary>
    ///// <returns></returns>
    //public Animator GetAnimatorEvent()
    //{
    //    return animatorEvent;
    //}

}
