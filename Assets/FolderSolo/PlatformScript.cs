using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// ������ ������� �� ������������ ���������
/// </summary>
public class PlatformScript : MonoBehaviour
{
    enum StatePlatform    // ��������� ���������
    {
        up,
        down,
        none
    }

    private PointUseItem pointUseItem;

    //[SerializeField]
    private Vector3 startPointPlatform;        // ��������� ��������������

    [Header("�������� ����������� ���������")]
    [SerializeField]
    private float stepMovePlatform = 1.0f;

    [Header("�������� ����� ���������")]
    [SerializeField]
    private Vector3 finishPointStopPlatform;

    private Vector3 currentFinishPointStopPlatform; // �������� ������� ��������� ���������

    private StatePlatform statePlatform = StatePlatform.none;

    [Header("�������� ����� ��������")]
    [SerializeField]
    private float delayAction = 0f;

    private void Start()
    {
        startPointPlatform = transform.position;
        pointUseItem = GetComponent<PointUseItem>();
    }

    /// <summary>
    /// ���� ������ ����� �� ���������
    /// </summary>
    /// <param name="col"></param>
    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.GetComponent<Rigidbody2D>())
        {
            col.transform.parent = transform;                                               // �����������  ��� � ���������
            currentFinishPointStopPlatform = startPointPlatform + finishPointStopPlatform;  // �������� ����� �������� 
            statePlatform = StatePlatform.down;                                           // �������� ����
            StartCoroutine(MovePlatform());                                               // ��������� ��������
        }
    }

    /// <summary>
    /// ���� ������ ����� � ���������
    /// </summary>
    /// <param name="col"></param>
    void OnCollisionExit2D(Collision2D col)
    {
        if (col.gameObject.GetComponent<Rigidbody2D>())
        {
            col.transform.parent = null;                            // ����������  ��� �� ��������� 
            currentFinishPointStopPlatform = startPointPlatform;    // �������� ����� �������� 
            statePlatform = StatePlatform.up;                        // �������� �����
            StartCoroutine(MovePlatform());                          // ��������� ��������
        }
    }

    /// <summary>
    /// �������� �������� ���������
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
                    break;                             //�������
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

        yield return new WaitForSeconds(delayAction); // ���������� �������� ����� delayAction

        if (pointUseItem.animatorEvent)
        {
            pointUseItem.animatorEvent.enabled = true;
        }

    }



}
