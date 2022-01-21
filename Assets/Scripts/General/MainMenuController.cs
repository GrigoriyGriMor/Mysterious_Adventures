using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    [Header("Главная панель")]
    [SerializeField] private GameObject mainUIPanel;
    [SerializeField] private Button B_Continue;
    [SerializeField] private Button B_NewGame;
    [SerializeField] private Button B_LogPanel;
    [SerializeField] private Button B_CreatersInfo;

    [Header("Панель дневник персонажа")]
    [SerializeField] private GameObject logUIPanel;
    [SerializeField] private Button b_LogBack;

    [Header("Панель информации о разработчиках")]
    [SerializeField] private GameObject CreatersInfoUIPanel;
    [SerializeField] private Button b_CreatersBack;

    [Header("Номер первого уровня")]
    [SerializeField] private int firstLevelID = 2;
    private int lastPlayedLevelID = -1;

    [SerializeField] private AudioClip backgroundClip;

    private void Start()
    {
        StartCoroutine(Initialize());

        if (backgroundClip != null && SoundManagerAllControll.Instance) SoundManagerAllControll.Instance.BackgroundClipPlay(backgroundClip);
    }

    private IEnumerator Initialize()
    {
        lastPlayedLevelID = SaveController.Instance.LoadlevelId();

        mainUIPanel.SetActive(true);
        logUIPanel.SetActive(true);
        CreatersInfoUIPanel.SetActive(true);
        yield return new WaitForFixedUpdate();

        if (lastPlayedLevelID != -1)//Если игрок еще не начинал играть и нет сохранений, то не показываем ему кнопку "Продолжить", а если он уже играл, о показываем
        {
            B_Continue.gameObject.SetActive(true);
            B_Continue.onClick.AddListener(() => SceneManager.LoadScene(lastPlayedLevelID));
        }
        else
            B_Continue.gameObject.SetActive(false);

        B_NewGame.onClick.AddListener(() =>
        {
            SaveData _data = new SaveData();
            _data.LevelId = -1;
            SaveController.Instance.SaveInfo(_data);
            SceneManager.LoadScene(firstLevelID);
        });

        B_LogPanel.onClick.AddListener(() =>
        {
            mainUIPanel.SetActive(false);
            logUIPanel.SetActive(true);
        });

        B_CreatersInfo.onClick.AddListener(() =>
        {
            mainUIPanel.SetActive(false);
            CreatersInfoUIPanel.SetActive(true);
        });

        b_LogBack.onClick.AddListener(() =>
        {
            mainUIPanel.SetActive(true);
            logUIPanel.SetActive(false);
        });

        b_CreatersBack.onClick.AddListener(() =>
        {
            mainUIPanel.SetActive(true);
            CreatersInfoUIPanel.SetActive(false);
        });

        yield return new WaitForFixedUpdate();
        logUIPanel.SetActive(false);
        CreatersInfoUIPanel.SetActive(false);
    }
}
