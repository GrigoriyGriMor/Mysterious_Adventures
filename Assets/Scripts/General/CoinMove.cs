using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinMove : MonoBehaviour
{
    [SerializeField] private int StartIntSpawn;
    [SerializeField] private float Speed;
    [SerializeField] private GameObject TargetMove;
    [SerializeField] private GameObject CoinPrefab;

    public GameObject[] coin;
    private ObjectPool ObjectPool;

    private void Awake()
    {
        ObjectPool = GetComponent<ObjectPool>();

        coin = new GameObject[StartIntSpawn];

        for (int i = 0; i < StartIntSpawn; i++)
        {
            coin[i] = Instantiate(CoinPrefab);
            coin[i].gameObject.SetActive(false);
        }
    }

    public IEnumerator Convert(int price, GameObject convertPosition)
    {
        Debug.Log(price);
        for (int i = 0, index = 0; i < price; i++, index++)
        {
            if (i % StartIntSpawn == 0)
                index = 0;

            Debug.Log(index);

            yield return new WaitForSeconds(0.1f);

            coin[index].transform.position = convertPosition.transform.position;
            coin[index].gameObject.SetActive(true);

            StartCoroutine(StartMove(coin[index]));
        }
        Debug.Log("STOP");
        ObjectPool.CoinMoveStart = false;
    }

    private IEnumerator StartMove(GameObject coin)
    {
        while (coin.transform.position != TargetMove.transform.position)
        {
            yield return new WaitForFixedUpdate();
            coin.transform.position = Vector2.MoveTowards(coin.transform.position, TargetMove.transform.position, Speed * Time.deltaTime);
        }

        coin.gameObject.SetActive(false);
        AllScore.Coin++;
    }
}
