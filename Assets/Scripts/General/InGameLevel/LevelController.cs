using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

//установка стартовых позиций + система сохранения
public class LevelController : MonoBehaviour
{
    private static LevelController instance;
    public static LevelController Instance => instance;

    [Header("Интерактивные объекты на сцене")]
    [SerializeField] private SceneInteractbleObj[] interactiveObj = new SceneInteractbleObj[0];

    [Header("Подбираемые предметы на сцене")]
    [SerializeField] private SceneItems[] sceneItems = new SceneItems[0];

    [Header("PlayerGameObject")]
    [SerializeField] private GameObject player;

    private void Awake()
    {
        instance = this;

        if (SaveController.Instance.LoadlevelId() != -1 &&
            (SaveController.Instance.LoadlevelId() == UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex)) InstantiateLevel();
        else
        {
            for (int i = 0; i < interactiveObj.Length; i++)
            {
                int number = i;
                interactiveObj[number]._object.activation.AddListener(() =>
                {
                    interactiveObj[number].state = 1;
                });
            }
        }
    }

    private void InstantiateLevel()
    {
        player.transform.position = SaveController.Instance.LoadPlayerPos();

        int[] stateLoad = SaveController.Instance.LoadObjState();
        int lenght = (stateLoad.Length < interactiveObj.Length) ? stateLoad.Length : interactiveObj.Length;

        for (int i = 0; i < lenght; i++)
        {
            if (stateLoad[i] == 1)
                interactiveObj[i]._object.UseObject();

            int number = i;
            interactiveObj[number]._object.activation.AddListener(() => 
            {
                interactiveObj[number].state = 1;
            });  
        }

        int[] _inventoryItems = SaveController.Instance.LoadInventoryItems();
        for (int j = 0; j < sceneItems.Length; j++)
            for (int x = 0; x < _inventoryItems.Length; x++)
                if (sceneItems[j].ID == _inventoryItems[x])
                {
                    sceneItems[j]._object.SetActive(false);
                    break;
                }
    }

    public int[] GetInteractiveObjState()
    {
        int[] stateLoad = new int[interactiveObj.Length];

        for (int i = 0; i < stateLoad.Length; i++)
            stateLoad[i] = interactiveObj[i].state;

        return stateLoad;
    }

    public Vector2 GetPlayerPos()
    {
        return player.transform.position;
    }
}

[Serializable]
public class SceneInteractbleObj
{
    public APInteractbleObjController _object;
    public int state;
}

[Serializable]
public class SceneItems
{
    public GameObject _object;
    public int ID;
}

