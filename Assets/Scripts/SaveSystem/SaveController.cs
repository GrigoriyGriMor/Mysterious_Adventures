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
            _data.LevelId = -1;//ID уровня
            _data.PlayerPosX = 0;//Позиция игрока по Х
            _data.PlayerPosY = 0;//Позиция игрока по Y
            _data.InventoryItemsID = new int[0];//Название итема в инвентаре привязанные к ID в массиве
            _data.ObjState = new int[0];//состояние интерактивного объекта: 0 - деактивирован, 1 - активирован

            PlayerPrefs.SetString("DataSave", JsonUtility.ToJson(_data));
        }
    }

    // Функция загружает все данные в класс
    public void SaveInfo(SaveData newData)
    {
        _data.LevelId = newData.LevelId;
        _data.PlayerPosX = newData.PlayerPosX;
        _data.PlayerPosY = newData.PlayerPosY;
        _data.InventoryItemsID = newData.InventoryItemsID;
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

    public int[] LoadInventoryItems()
    {
        return _data.InventoryItemsID;
    }

    public int[] LoadObjState()
    {
        return _data.ObjState;
    }
}

// Класс в котором инициилизируются все данные, которые нам нужны конкретно в нашем проекте
[Serializable]
public class SaveData 
{
    public int LevelId;
    public float PlayerPosX;
    public float PlayerPosY;
    public int[] InventoryItemsID = new int[0];
    public int[] ObjState = new int[0];
}

//ПРИМЕР ВЫЗОВА СОХРАНЕНИЯ ИЗ ДРУГОГО КЛАССА
/*     
 *  public void SaveInitialization()
    {
        SaveData _data = new SaveData();
        _data.LevelId = SceneManager.GetActiveScene().buildIndex;
        _data.ObjState = LevelController.Instance.GetInteractiveObjState();
        Vector2 playerPos = LevelController.Instance.GetPlayerPos();
        _data.PlayerPosX = playerPos.x;
        _data.PlayerPosY = playerPos.y;
        _data.InventoryItems = new string[0];

        SaveController.Instance.SaveInfo(_data);
    }
*/