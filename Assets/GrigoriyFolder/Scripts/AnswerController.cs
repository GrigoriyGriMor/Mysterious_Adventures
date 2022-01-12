using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class AnswerController : MonoBehaviour
{
    [SerializeField] private string[] textAnswer;
    [SerializeField] private AudioClip[] audioAnswer;

    private void Start()
    {
        if (textAnswer.Length == 0)
        {
            Array.Resize(ref textAnswer, 1);
            textAnswer[0] = "Что это?";
        }
    }

    public void OnReuestObjInfo(APPlayerController player)
    {
            int textR = UnityEngine.Random.Range(0, textAnswer.Length);
            int audioR = UnityEngine.Random.Range(0, audioAnswer.Length);

            if (audioAnswer.Length != 0 && audioAnswer[audioR] != null)
                player.gameObject.GetComponent<APPlayerController>().AnswerConnector(textAnswer[textR], audioAnswer[audioR]);
            else
                player.gameObject.GetComponent<APPlayerController>().AnswerConnector(textAnswer[textR]);
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<APPlayerController>())
        {
            int textR = UnityEngine.Random.Range(0, textAnswer.Length);
            int audioR = UnityEngine.Random.Range(0, audioAnswer.Length);

            if (audioAnswer.Length != 0 && audioAnswer[audioR] != null)
                collision.gameObject.GetComponent<APPlayerController>().AnswerConnector(textAnswer[textR], audioAnswer[audioR]);
            else
                collision.gameObject.GetComponent<APPlayerController>().AnswerConnector(textAnswer[textR]);
        }
    }

   /* public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<APPlayerController>())
        {
            int textR = UnityEngine.Random.Range(0, textAnswer.Length);
            int audioR = UnityEngine.Random.Range(0, audioAnswer.Length);

            if (audioAnswer.Length != 0 && audioAnswer[audioR] != null)
                collision.GetComponent<APPlayerController>().AnswerConnector(textAnswer[textR], audioAnswer[audioR]);
            else
                collision.GetComponent<APPlayerController>().AnswerConnector(textAnswer[textR]);
        }
    }*/
}
