using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class SaveController : MonoBehaviour
{
    private static SaveController instance;
    public static SaveController Instance => instance;
    private SaveData _data;

    private void Awake()
    {
        instance = this;
    }

    //Если внесли изменения в переменную дата сразу запарсили в PlayerPrefs

    private void Start()
    {
        if (PlayerPrefs.HasKey("DataSave"))
            _data = JsonUtility.FromJson<SaveData>(PlayerPrefs.GetString("DataSave"));
    }

    // Функция загружает все данные в класс
    public void SaveInfo(int _levelId, Vector2 playerPos, string[] InventoryItems, int[] ObjState)
    {
        _data.LevelId = _levelId;
        _data.PlayerPosX = playerPos.x;
        _data.PlayerPosY = playerPos.y;
        _data.InventoryItems = InventoryItems;
        _data.ObjState = ObjState;

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