using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AllScore : MonoBehaviour
{
    [SerializeField] private Text ScoreDiamond;
    [SerializeField] private Text ScoreCoin;

    private void FixedUpdate()
    {
        ScoreDiamond.text = DimondLoot.Diamond.ToString();
    }
}
