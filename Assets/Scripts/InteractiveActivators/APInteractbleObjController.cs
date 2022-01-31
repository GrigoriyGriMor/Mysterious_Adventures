using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class APInteractbleObjController : MonoBehaviour
{
    [Header("Нужен ли итем?")]
    public bool needItem = false;
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

    private InventoryCard itemBeLoad = null;

    public string UseObject()//если мы отправляем запрос без данных, то используется логика этой функкции
    {
        if (needItem)
        {
            if (itemBeLoad != null)
            {
                itemBeLoad.CompliteItemUse();
                itemBeLoad = null;
            }
            else
                return triggersForAnim;
        }

        StartCoroutine(UseObj());

        return triggersForAnim;
    }

    //возвращает ответ на вопрос нужен ли итем, что бы использовать "рычаг" 
    public bool NeedItem(InventoryCard itemID = null)
    {
        if (needItem && (itemID != null && itemID.GetID() == needItemID))
        {
            itemBeLoad = itemID;
            // ниже очень важный момент (по идее такого делать не стоит, но тут пришлось), если нам передали какой-то объект из инвентаря на проверку, этот рычаг подписывается на событие "отмену использования" предмета из инвентаря.
            // нужно это было для того, что бы передать итем напрямую сразу после клика на него нужным предметом, а не передачи через плеерконтроллер и другие классы.
            itemBeLoad.cancelUse.AddListener(() =>
            {
                itemBeLoad.cancelUse.RemoveAllListeners();
                itemBeLoad = null;
            });
            return true;
        }       

        return false;
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
