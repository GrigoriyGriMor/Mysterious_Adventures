using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnlargedObject : MonoBehaviour
{
    [SerializeField] private EnlargedObjectController controller;

    [SerializeField] private Sprite visual;

    [Header("Триггер Анимации взаимодействий")]
    [SerializeField] private string triggersForAnim = "";

    private void Awake()
    {
        if (controller == null)
            controller = FindObjectOfType<EnlargedObjectController>();
    }

    public string UseObject()
    {
        controller.ActivatePanel(visual, Camera.main.WorldToScreenPoint(transform.position));
        return triggersForAnim;
    }
}
