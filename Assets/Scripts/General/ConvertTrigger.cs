using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConvertTrigger : MonoBehaviour
{
    [Header("Триггер Анимации взаимодействий")]
    [SerializeField] private string triggersForAnim = "";

    [SerializeField] private ObjectPool ObjectPool;

    private void Awake()
    {
        gameObject.transform.GetComponent<BoxCollider2D>().isTrigger = false;
        gameObject.transform.GetComponent<BoxCollider2D>().isTrigger = true;
    }

    public string UseObject()
    {
        ObjectPool.Convert12();

        return triggersForAnim;
    }
}
