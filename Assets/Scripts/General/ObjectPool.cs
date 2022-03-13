using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ObjectPool : MonoBehaviour
{
    [SerializeField] private GameObject Diamond;
    [SerializeField] private int SpawnDiamondNumber;
    [SerializeField] private float Speed;
    [SerializeField] private GameObject[] DiamonPos;
    [SerializeField] private ParticleSystem Particle;
    [SerializeField] private int Price;
    [SerializeField] private float DelayToPrice;
    [SerializeField] private GameObject ConvertGameObject;

    private CoinMove CoinMove;
    private GameObject[] diamondArray;

    private void Awake()
    {
        CoinMove = GetComponent<CoinMove>();

        diamondArray = new GameObject[SpawnDiamondNumber];

        for (int i = 0; i < SpawnDiamondNumber; i++)
        {
            diamondArray[i] = Instantiate(Diamond);
            diamondArray[i].SetActive(false);
        }
    }

    public void Convert12()
    {
        for (int i = 0; DimondLoot.Diamond > 0; i++)
        {
            if (diamondArray[i].transform.position == DiamonPos[i].transform.position)
                continue;

            else
            {
                DimondLoot.Diamond--;

                diamondArray[i].transform.position = transform.position;
                diamondArray[i].GetComponent<DimondLoot>().StartConvert();

                diamondArray[i].SetActive(true);

                StartCoroutine(DiamondMove(diamondArray[i], DiamonPos[i]));

                if (Particle != null)
                    Particle.Play();
            }
        }
    }

    //переменна для того что бы следить сколько раз вызывали функцию CoinMove.Convert
    public bool CoinMoveStart = false;

    private IEnumerator DiamondMove(GameObject diamond, GameObject targetPos)
    {
        while (diamond.transform.position != targetPos.transform.position)
        {
            diamond.transform.position = Vector2.MoveTowards(diamond.transform.position, targetPos.transform.position, Speed * Time.deltaTime);
            yield return new WaitForFixedUpdate();
        }

        while (true)
        {
            if (CoinMoveStart == false)
            {
                CoinMove.StartCoroutine(CoinMove.Convert(Price, ConvertGameObject));
                CoinMoveStart = true;
                break;
            }

            yield return new WaitForFixedUpdate();
        }
    }
}
