using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class APPlayerController : MonoBehaviour
{
    [SerializeField] private Animator playerVisual;

    [Header("Панель подсказки")]
    [SerializeField] private Image answerUIPanel;
    [SerializeField] private Text answerText;

    [Header("Время показа панели подсказки")]
    [SerializeField] private float answerTextVisibleTime;

    [Header("Move Setting")]
    private Vector2 target;
    [SerializeField] private float speed = 1;
    private bool canMove = false;

    private void Start()
    {
        answerUIPanel.gameObject.SetActive(false);
        answerUIPanel.color = new Color(answerUIPanel.color.r, answerUIPanel.color.g, answerUIPanel.color.b, 0);
    }

    private Coroutine moveCorouine;
    //функция приема точки куда должен идти персонаж
    #region Move
    public void SetNewMoveTarget(Vector2 targetPos, APInteractbleObjController interactiveButton = null)
    {
        target = new Vector2(targetPos.x, transform.position.y);

        if (transform.position.x > target.x && playerVisual.transform.localScale.x > 0)
            playerVisual.transform.localScale = new Vector3(playerVisual.transform.localScale.x * (-1), playerVisual.transform.localScale.y, playerVisual.transform.localScale.z);
        else
           if (transform.position.x < target.x && playerVisual.transform.localScale.x < 0)
                playerVisual.transform.localScale = new Vector3(playerVisual.transform.localScale.x * (-1), playerVisual.transform.localScale.y, playerVisual.transform.localScale.z);

        if (moveCorouine != null)
            StopCoroutine(moveCorouine);

        moveCorouine = StartCoroutine(Move(interactiveButton));
    }

    private IEnumerator Move(APInteractbleObjController interactiveButton = null)
    {
        canMove = true;
        playerVisual.SetBool("Run", true);

        while (canMove && Vector2.Distance(transform.position, target) > 0.05f)
        {
            if (playerVisual.transform.localScale.x > 0)
                transform.position = new Vector2((transform.position.x + speed * Time.deltaTime), transform.position.y);
            else
                transform.position = new Vector2((transform.position.x - speed * Time.deltaTime), transform.position.y);

            yield return new WaitForFixedUpdate();
        }

        canMove = false;
        playerVisual.SetBool("Run", false);

        yield return new WaitForFixedUpdate();
        if (interactiveButton != null) interactiveButton.UseObject();
    }
    #endregion

    //фукнкция приема ответа от класса AnswerController
    #region AsnwerController
    public void AnswerConnector(string _text = "", AudioClip _clip = null)
    {
        if (_text != "" && !answerUIPanel.gameObject.activeInHierarchy) 
        {
            answerText.text = _text;
            StartCoroutine(AnswerSendActivate());
        }

        if (_clip != null && SoundManagerAllControll.Instance) SoundManagerAllControll.Instance.ClipPlay(_clip);
    }

    private IEnumerator AnswerSendActivate()
    {
        answerUIPanel.gameObject.SetActive(true);
        while (answerUIPanel.color.a < 1)
        {
            answerUIPanel.color = new Color(answerUIPanel.color.r, answerUIPanel.color.g, answerUIPanel.color.b, answerUIPanel.color.a + Time.deltaTime);
            yield return new WaitForFixedUpdate();
        }

        yield return new WaitForSeconds(answerTextVisibleTime);

        while (answerUIPanel.color.a > 0)
        {
            answerUIPanel.color = new Color(answerUIPanel.color.r, answerUIPanel.color.g, answerUIPanel.color.b, answerUIPanel.color.a - Time.deltaTime);
            yield return new WaitForFixedUpdate();
        }

        answerUIPanel.gameObject.SetActive(false);
    }
    #endregion

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<DangerObstacle>())
            StartCoroutine(Die());

        if (collision.gameObject.tag != "Bottom")
        { 
            if (moveCorouine != null) StopCoroutine(moveCorouine);
            canMove = false;
            playerVisual.SetBool("Run", false);
        }

    }

    private IEnumerator Die()
    {
        canMove = false;
        playerVisual.SetTrigger("Dead");
        yield return new WaitForEndOfFrame();
    }
}
