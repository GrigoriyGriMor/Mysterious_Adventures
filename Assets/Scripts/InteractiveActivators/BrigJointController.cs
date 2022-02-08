using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrigJointController : MonoBehaviour
{
    [Header("base param")]
    [SerializeField] private Joint2D[] joints = new Joint2D[2];
    [SerializeField] private Rigidbody2D _rb;
    [SerializeField] private Collider2D _collider;
    [SerializeField] private Animator anim;// состояние спокойствия, состояние повреждения, состояние поломки

    [Header("")]
    [SerializeField] private float breakTime = 1.5f;

    private Coroutine breakCoroutine = null;

    private void Start()
    {
        if (_rb == null)
            _rb = GetComponent<Rigidbody2D>();

        if (_collider == null)
            _collider = GetComponent<Collider2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<APPlayerController>() && breakCoroutine == null)
            breakCoroutine = StartCoroutine(BreakCoroutine());
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.GetComponent<APPlayerController>() && breakCoroutine != null)
        {
            StopCoroutine(breakCoroutine);
            anim.SetBool("OnBrig", false);
            breakCoroutine = null;
        }
    }

    private IEnumerator BreakCoroutine()
    {
        float time = 0;
        anim.SetBool("OnBrig", true);

        while (time < breakTime)
        {
            time += Time.deltaTime;
            Debug.LogWarning(time);
            yield return new WaitForFixedUpdate();
        }

        anim.SetTrigger("BreakBrig");
        _collider.isTrigger = true;
        for (int i = 0; i < joints.Length; i++)
            joints[i].enabled = false;
    }
}
