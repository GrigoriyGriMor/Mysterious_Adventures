using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuHandler : MonoBehaviour
{
    [SerializeField] private GameObject continueButton;
    private int currentLevel;

    /// <summary>
    /// �� ������ �� �������� ���� �� ������, �� ������� ����������� �����. � ���� ������ ���� ������, ������������ � PlayerPrefs id ����� ����� ������.
    /// </summary>
    private void Start()
    {
        GetCurrentLevel();
        
    }

    /// <summary>
    /// ������� �� ����
    /// </summary>
    public void ApplicationQuit()
    {
        Application.Quit();
    }

    /// <summary>
    /// ���������� ���� � ������, �� ������� ������������
    /// </summary>
    public void ContinueGame()
    {
        SceneManager.LoadScene(currentLevel);
    }

    /// <summary>
    /// ����� ��������� �������, �� ������� ������������
    /// </summary>
    public void GetCurrentLevel()
    {
        if (PlayerPrefs.HasKey("CurrentLevel"))
        {
            currentLevel = PlayerPrefs.GetInt("CurrentLevel");
        }
    }

    /// <summary>
    /// ��������� ������ �������, ���� ����� ����� ����.
    /// </summary>
    public void FirstLevel()
    {
        SceneManager.LoadScene(1);
    }

    /// <summary>
    /// ��������� � PlayerPrefs, ���� �� ������� ���� ���� �� ���. ��������� ������� ������� ��������� ���������� PlayerPrefs GameWasPlayed
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
