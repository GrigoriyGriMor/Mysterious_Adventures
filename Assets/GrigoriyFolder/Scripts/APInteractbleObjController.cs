using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class APInteractbleObjController : MonoBehaviour
{
    [Header("Нужен ли итем?")]
    [SerializeField] private bool NeedItem = false;
    [SerializeField] private int needItemID = 0;

    [Header("Анимация Рычага")]
    [SerializeField] private Animator objAnim;

    [Header("Задержка")]
    [SerializeField] private float waitTime = 1;

    [Header("Анимация активного обьекта")]
    [SerializeField] private Animator useObjAnim;
    [SerializeField] private ParticleSystem particle;

    [SerializeField] private bool oneShot = false;
    private bool oneShotActive = false;

    public bool needUse = false;

    [Header("Триггер Анимации взаимодействий")]
    [SerializeField] private string triggersForAnim = "";

    [Header("Точка выхода из уровня?")]
    [SerializeField] private bool endLevelPoint;

    public UnityEvent activation = new UnityEvent();

    public string UseObject()//если мы отправляем запрос без данных, то используется логика этой функкции
    {
        if (NeedItem) return "";

        StartCoroutine(UseObj());

        return triggersForAnim;
    }

    public string UseObject(int itemID)//если мы отправляем запрос c данными, то используется логика этой функкции
    {
        if (itemID == needItemID)
            StartCoroutine(UseObj());

        return triggersForAnim;
    }

    private IEnumerator UseObj()
    {
        if (oneShot)
        {
            if (oneShotActive) yield break;

            oneShotActive = true;

            if (objAnim != null)
                objAnim.SetTrigger("Start");

            yield return new WaitForSeconds(waitTime);

            if (useObjAnim != null)
                useObjAnim.SetTrigger("Start");

            if (particle != null)
                particle.Play();

            activation.Invoke();
        }
        else
        {
            if (objAnim != null)
                objAnim.SetTrigger("Start");

            yield return new WaitForSeconds(waitTime);

            if (useObjAnim != null)
                useObjAnim.SetTrigger("Start");

            if (particle != null)
                particle.Play();

            activation.Invoke();
        }

        if (endLevelPoint)
        {
            yield return new WaitForSeconds(1);
            GameStateController.Instance.GameEnd(true);
        }
    }
}
