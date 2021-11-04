using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuHandler : MonoBehaviour
{
    [SerializeField] private GameObject continueButton;
    private int currentLevel;

    /// <summary>
    /// На старте мы получаем инфу об уровне, на котором остановился игрок. В игре должен быть скрипт, записывающий в PlayerPrefs id сцены этого уровня.
    /// </summary>
    private void Start()
    {
        GetCurrentLevel();
        
    }

    /// <summary>
    /// Выходим из игры
    /// </summary>
    public void ApplicationQuit()
    {
        Application.Quit();
    }

    /// <summary>
    /// Продолжаем игру с уровня, на котором остановились
    /// </summary>
    public void ContinueGame()
    {
        SceneManager.LoadScene(currentLevel);
    }

    /// <summary>
    /// Узнаём последний уровень, на котором остановились
    /// </summary>
    public void GetCurrentLevel()
    {
        if (PlayerPrefs.HasKey("CurrentLevel"))
        {
            currentLevel = PlayerPrefs.GetInt("CurrentLevel");
        }
    }

    /// <summary>
    /// Загружаем первый уровень, если хотим новую игру.
    /// </summary>
    public void FirstLevel()
    {
        SceneManager.LoadScene(1);
    }

    /// <summary>
    /// Проверяем в PlayerPrefs, была ли сыграна игра хотя бы раз. Требуется наличие заранее созданной переменной PlayerPrefs GameWasPlayed
    /// </summary>
    /// <returns></returns>
    private bool GameWasPlayed()
    {
        bool gameWasPlayed = false;
        if (PlayerPrefs.HasKey("GameWasPlayed"))
        {
            return gameWasPlayed;
        }
        return gameWasPlayed;
    }

    private void SetActive()
    {
        continueButton.SetActive(GameWasPlayed());
    }
    
}
