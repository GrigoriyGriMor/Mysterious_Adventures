using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SavePlace : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!GameStateController.Instance.gameIsPlayed) return;

        if (collision.GetComponent<APPlayerController>())
            GameStateController.Instance.SaveInitialization();
    }
}
