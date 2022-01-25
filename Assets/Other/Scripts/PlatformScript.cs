using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// скрипт вешаеца на движательную платформу
/// </summary>
public class PlatformScript : MonoBehaviour
{
    enum StatePlatform    // состояние платформы
    {
        up,
        down,
        none
    }

    private PointUseItem pointUseItem;

    //[SerializeField]
    private Vector3 startPointPlatform;        // Начальная точкаплатформы

    [Header("Скорость перемещения платформы")]
    [SerializeField]
    private float stepMovePlatform = 1.0f;

    [Header("Конечная точка платформы")]
    [SerializeField]
    private Vector3 finishPointStopPlatform;

    private Vector3 currentFinishPointStopPlatform; // финишная позиция положения платформы

    private StatePlatform statePlatform = StatePlatform.none;

    [Header("Задержка перед событием")]
    [SerializeField]
    private float delayAction = 0f;

    private void Start()
    {
        startPointPlatform = transform.position;
        pointUseItem = GetComponent<PointUseItem>();
    }

    /// <summary>
    /// Если обьект зашел на платформу
    /// </summary>
    /// <param name="col"></param>
    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.GetComponent<Rigidbody2D>())
        {
            col.transform.parent = transform;                                               // привязываем  его к платформе
            currentFinishPointStopPlatform = startPointPlatform + finishPointStopPlatform;  // конечная точка движения 
            statePlatform = StatePlatform.down;                                           // движемся вниз
            StartCoroutine(MovePlatform());                                               // запускаем движение
        }
    }

    /// <summary>
    /// Если обьект вышел с платформы
    /// </summary>
    /// <param name="col"></param>
    void OnCollisionExit2D(Collision2D col)
    {
        if (col.gameObject.GetComponent<Rigidbody2D>())
        {
            col.transform.parent = null;                            // отвязываем  его от платформы 
            currentFinishPointStopPlatform = startPointPlatform;    // конечная точка движения 
            statePlatform = StatePlatform.up;                        // движемся вверх
            StartCoroutine(MovePlatform());                          // запускаем движение
        }
    }

    /// <summary>
    /// Корутина движения платформы
    /// </summary>
    /// <returns></returns>
    IEnumerator MovePlatform()
    {
        while (true)
        {
            //  Debug.Log(" Move ");

            transform.position = Vector3.Lerp(transform.position, currentFinishPointStopPlatform, stepMovePlatform * Time.deltaTime);

            if (statePlatform == StatePlatform.down)
            {
                if (currentFinishPointStopPlatform.y + 0.2f > transform.position.y)
                {
                    StartCoroutine(ActionPlatform());
                    //  Debug.Log(" Move stop down");
                    break;                             //выходим
                }
            }

            if (statePlatform == StatePlatform.up)
            {
                if (currentFinishPointStopPlatform.y - 0.2f < transform.position.y)
                {
                    //  Debug.Log(" Move stop up ");
                    break;
                }
            }

            yield return null;
        }
    }

    IEnumerator ActionPlatform()
    {

        yield return new WaitForSeconds(delayAction); // продолжить примерно через delayAction

        if (pointUseItem.animatorEvent)
        {
            pointUseItem.animatorEvent.enabled = true;
        }

    }



}
