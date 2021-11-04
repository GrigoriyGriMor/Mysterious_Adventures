using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    /// <summary>
    /// ������ ���� �� �����
    /// </summary>
    public void Pause()
    {
        Time.timeScale = 0;
    }

    /// <summary>
    /// ������������ � ���� ����� �����
    /// </summary>
    public void ReturnToGame()
    {
        Time.timeScale = 1;
    }

    public void BackToMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

}
