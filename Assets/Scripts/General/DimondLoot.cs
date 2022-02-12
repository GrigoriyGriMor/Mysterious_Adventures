using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DimondLoot : MonoBehaviour
{
    public static int Diamond = 0;

    [Header("Скорость монетки до текста")]
    [SerializeField] private float SpeedToText;

    [Header("Скорость монетки до центра")]
    [SerializeField] private float SpeedToCenter;

    [Header("Обеъкт который весит в центре экрана")]
    [SerializeField] private GameObject ScoreGameObject;

    [Header("Обеъкт который весит в центре экрана")]
    [SerializeField] private GameObject Center;


    private bool coinLoot = false;
    private bool objPool = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (objPool == false)
        {
            if (collision.GetComponent<APPlayerController>())
            {
                if (coinLoot == false)
                {
                    coinLoot = true;

                    StartCoroutine(CoinMoveToCenter());
                }
            }

            if (collision.gameObject.tag == "CoinCenter")
                StartCoroutine(CoinLocalScale());

            if (collision.gameObject.tag == "CoinDie")
            {
                Diamond++;

                Destroy(gameObject);
                StopCoroutine(CoinMoveToScore());
            }
        }
    }

    private IEnumerator CoinMoveToCenter()
    {
        while (true)
        {
            gameObject.transform.position = Vector2.MoveTowards(gameObject.transform.position, Center.transform.position, SpeedToCenter * Time.deltaTime);
            yield return new WaitForFixedUpdate();
        }
    }

    private IEnumerator CoinLocalScale()
    {
        while (transform.localScale.x < 2 && transform.localScale.y < 2)
        {
            transform.localScale = new Vector2(transform.localScale.x + Time.deltaTime, transform.localScale.y + Time.deltaTime);
            yield return new WaitForFixedUpdate();
        }

        while (transform.localScale.x > 1 && transform.localScale.y > 1)
        {
            transform.localScale = new Vector2(transform.localScale.x - Time.deltaTime, transform.localScale.y - Time.deltaTime);
            yield return new WaitForFixedUpdate();
        }

        StartCoroutine(CoinMoveToScore());
        StopCoroutine(CoinLocalScale());
    }

    private IEnumerator CoinMoveToScore()
    {
        while (true)
        {
            gameObject.transform.position = Vector2.MoveTowards(gameObject.transform.position, ScoreGameObject.transform.position, SpeedToText * Time.deltaTime * 2);

            yield return new WaitForFixedUpdate();
        }
    }

    public void StartConvert()
    {
        objPool = true;
    }
}
