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

    //���� ������ ��������� � ���������� ���� ����� ��������� � PlayerPrefs

    private void Start()
    {
        if (PlayerPrefs.HasKey("DataSave"))
            _data = JsonUtility.FromJson<SaveData>(PlayerPrefs.GetString("DataSave"));
        else
        {
            _data.LevelId = -1;//ID ������
            _data.Diamond = 0;//���������� �������
            _data.PlayerPosX = 0;//������� ������ �� �
            _data.PlayerPosY = 0;//������� ������ �� Y
            _data.InventoryItemsID = new int[0];//�������� ����� � ��������� ����������� � ID � �������
            _data.ObjState = new int[0];//��������� �������������� �������: 0 - �������������, 1 - �����������

            PlayerPrefs.SetString("DataSave", JsonUtility.ToJson(_data));
        }
    }

    // ������� ��������� ��� ������ � �����
    public void SaveInfo(SaveData newData)
    {
        _data.LevelId = newData.LevelId;
        _data.Diamond = newData.Diamond;
        _data.PlayerPosX = newData.PlayerPosX;
        _data.PlayerPosY = newData.PlayerPosY;
        _data.InventoryItemsID = newData.InventoryItemsID;
        _data.ObjState = newData.ObjState;

        PlayerPrefs.SetString("DataSave", JsonUtility.ToJson(_data));
    }

    // ��������� ��� ������ � ����
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

// ����� � ������� ���������������� ��� ������, ������� ��� ����� ��������� � ����� �������
[Serializable]
public class SaveData 
{
    public int LevelId;
    public int Diamond;
    public float PlayerPosX;
    public float PlayerPosY;
    public int[] InventoryItemsID = new int[0];
    public int[] ObjState = new int[0];
}

//������ ������ ���������� �� ������� ������
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