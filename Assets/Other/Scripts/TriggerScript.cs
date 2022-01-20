using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerScript : MonoBehaviour
{
    /// <summary>
    /// Класс вешается на префаб рычага
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
    public ObjectEvent objectEvent;
}
