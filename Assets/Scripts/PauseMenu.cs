using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    /// <summary>
    /// Ставим игру на паузу
    /// </summary>
    public void Pause()
    {
        Time.timeScale = 0;
    }

    /// <summary>
    /// Возвращаемся в игру после паузы
    /// </summary>
    public void ReturnToGame()
    {
        Time.timeScale = 1;
    }

    public void BackToMenu()
    {
        SceneManager.LoadScene("MenuScene");
        ReturnToGame();
    }

    public void Restart()
    {
        ReturnToGame();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

}
