using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Класс вешается на префаб вещи 
/// </summary>
public class Item : MonoBehaviour
{
    [Header("Имя предмета")]
    public string nameItem;

    [Header("ID предмета")]
    public int id;
}
