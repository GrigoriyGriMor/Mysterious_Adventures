using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Класс контроллер запуска игры, определяет какие объекты не будут уничтожаться в дальнейшем, а так же показывает превью окна при необходимости
public class PreviewController : MonoBehaviour
{
    [Header("DontDestroyOnLoad")]
    [SerializeField] private GameObject[] dontDestroyObj = new GameObject[2];

    [SerializeField] private int MainMenuLevelID = 1;

    private void Awake()
    {
        for (int i = 0; i < dontDestroyObj.Length; i++)
            DontDestroyOnLoad(dontDestroyObj[i]);

        UnityEngine.SceneManagement.SceneManager.LoadScene(MainMenuLevelID);
    }
}
