using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveController : MonoBehaviour
{
    private static SaveController instance;
    public static SaveController Instance => instance;

    private void Awake()
    {
        instance = this;
    }

    public int GetLastPlayedLevelNumber()
    {
        int i;
        if (PlayerPrefs.HasKey("LastPlayedLevelID"))
            i = PlayerPrefs.GetInt("LastPlayedLevelID");
        else
            i = -1;

        SetPrefs();

        return i;
    }

    private void SetPrefs()
    {
        PlayerPrefs.SetInt("LastPlayedLevelID", 2);
    }
}
