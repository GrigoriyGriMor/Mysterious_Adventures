using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class SaveController : MonoBehaviour
{
    private static SaveController instance;
    public static SaveController Instance => instance;
    private SaveData _data = new SaveData();

    private void Awake()
    {
        instance = this;
    }

    //Если внесли изменения в переменную дата сразу запарсили в PlayerPrefs

    private void Start()
    {
        if (PlayerPrefs.HasKey("DataSave"))
            _data = JsonUtility.FromJson<SaveData>(PlayerPrefs.GetString("DataSave"));
        else
        {
            _data.LevelId = -1;
            _data.PlayerPosX = 0;
            _data.PlayerPosY = 0;
            _data.InventoryItems = new string[0];
            _data.ObjState = new int[0];

            PlayerPrefs.SetString("DataSave", JsonUtility.ToJson(_data));
        }
    }

    // Функция загружает все данные в класс
    public void SaveInfo(SaveData newData)
    {
        _data.LevelId = newData.LevelId;
        _data.PlayerPosX = newData.PlayerPosX;
        _data.PlayerPosY = newData.PlayerPosY;
        _data.InventoryItems = newData.InventoryItems;
        _data.ObjState = newData.ObjState;

        PlayerPrefs.SetString("DataSave", JsonUtility.ToJson(_data));
    }

    // Загружает все данные в игру
    public int LoadlevelId()
    {
        return _data.LevelId;
    }

    public Vector2 LoadPlayerPos()
    {
        Vector2 playerPos = new Vector2(_data.PlayerPosX, _data.PlayerPosY);
        return playerPos;
    }

    public string[] LoadInventoryItems()
    {
        return _data.InventoryItems;
    }

    public int[] LoadObjState()
    {
        return _data.ObjState;
    }
}

// Тут сохраняются все данные
[Serializable]
public class SaveData 
{
    public int LevelId;
    public float PlayerPosX;
    public float PlayerPosY;
    public string[] InventoryItems;
    public int[] ObjState;
}