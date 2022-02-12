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

    private GameObject[] diamondArray;

    private void Awake()
    {
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
            DimondLoot.Diamond--;

            if (diamondArray[i].transform.position == DiamonPos[i].transform.position)
                continue;

            diamondArray[i].transform.position = transform.position;
            diamondArray[i].GetComponent<DimondLoot>().StartConvert();

            diamondArray[i].SetActive(true);

            StartCoroutine(DiamondMove(diamondArray[i], DiamonPos[i]));

            if (Particle != null)
                Particle.Play();
        }
    }

    private IEnumerator DiamondMove(GameObject diamond, GameObject targetPos)
    {
        while (true)
        {
            if (diamond.transform.position == targetPos.transform.position)
                StopCoroutine(DiamondMove(diamond, targetPos));

            diamond.transform.position = Vector2.MoveTowards(diamond.transform.position, targetPos.transform.position, Speed * Time.deltaTime);
            yield return new WaitForFixedUpdate();
        }
    }
}
