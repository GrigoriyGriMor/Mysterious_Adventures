using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Events;

public class GameStateController : MonoBehaviour
{
    private static GameStateController instance;
    public static GameStateController Instance => instance;

    [Header("UI Elements")]
    [SerializeField] private GameObject[] inGameUIPanels = new GameObject[2];
    [SerializeField] private GameObject winGameUIPanel;
    [SerializeField] private GameObject loseGameUIPanel;

    [Header("ID следующего уровн€")]
    [SerializeField] private int nextLevelID = 1;

    [HideInInspector] public bool gameIsPlayed = false;

    [Header("—обыти€ состо€ний игры")]
    public UnityEvent gameStarted = new UnityEvent();
    public UnityEvent gameEnded = new UnityEvent();

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        if (SceneManager.GetActiveScene().buildIndex != SaveController.Instance.LoadlevelId())
            SaveInitialization();

        winGameUIPanel.SetActive(false);
        loseGameUIPanel.SetActive(false);
        for (int i = 0; i < inGameUIPanels.Length; i++)
            inGameUIPanels[i].SetActive(true);

        Invoke("GameStart", 1);
    }

    public void GoToMainMenu()
    {
        SceneManager.LoadScene(1);
    }

    public void NextLevel()
    {
        SceneManager.LoadScene(nextLevelID);
    }

    //ѕолностью перезагружает данный уровень
    public void RestartAllLevel()
    {
        SaveData _data = new SaveData();
        _data.LevelId = -1;
        SaveController.Instance.SaveInfo(_data);

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    //перезагружает уровень на крайнюю точку сохранени€
    public void RestartInLastSave()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void GameStart()
    {
        gameStarted.Invoke();
        gameIsPlayed = true;
    }

    public void GameEnd(bool win)
    {
        gameEnded.Invoke();
        gameIsPlayed = false;

        if (win)
        {
            for (int i = 0; i < inGameUIPanels.Length; i++)
                inGameUIPanels[i].SetActive(false);

            winGameUIPanel.SetActive(true);
        }
        else
        {
            for (int i = 0; i < inGameUIPanels.Length; i++)
                inGameUIPanels[i].SetActive(false);

            loseGameUIPanel.SetActive(true);
        }
    }

    public void SaveInitialization()
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

}
