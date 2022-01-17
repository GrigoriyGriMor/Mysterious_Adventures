using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class APInteractbleObjController : MonoBehaviour
{
    [Header("�������� ������")]
    [SerializeField] private Animator objAnim;

    [Header("��������")]
    [SerializeField] private float waitTime = 1;

    [Header("�������� ��������� �������")]
    [SerializeField] private Animator useObjAnim;
    [SerializeField] private ParticleSystem particle;

    [SerializeField] private bool oneShot = false;
    private bool oneShotActive = false;

    public bool needUse = false;

    [Header("������� �������� ��������������")]
    [SerializeField] private string triggersForAnim = "";

    [Header("����� ������ �� ������")]
    [SerializeField] private bool endLevelPoint;

    public UnityEvent activation = new UnityEvent();

    public string UseObject()
    {
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
