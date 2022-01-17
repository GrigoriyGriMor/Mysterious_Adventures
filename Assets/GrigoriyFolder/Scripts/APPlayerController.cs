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
    [Header("Регулятор размерности подсказки")]
    [SerializeField] private int oneLetterParcer = 30;
    [SerializeField] private int maxLetterInOneLine = 20;
    private Vector2 panelStartSize;

    [Header("Время показа панели подсказки")]
    [SerializeField] private float answerTextVisibleTime;

    [Header("Move Setting")]
    private Vector2 target;
    [SerializeField] private float speed = 1;
    private bool canMove = false;

    private Rigidbody2D _rb;

    [SerializeField] private AudioClip backgroundClip;

    private void Start()
    {
        _rb = gameObject.GetComponent<Rigidbody2D>();
        panelStartSize = answerUIPanel.rectTransform.sizeDelta;

        answerUIPanel.gameObject.SetActive(false);
        answerUIPanel.color = new Color(answerUIPanel.color.r, answerUIPanel.color.g, answerUIPanel.color.b, 0);

        if (backgroundClip != null && SoundManagerAllControll.Instance) SoundManagerAllControll.Instance.BackgroundClipPlay(backgroundClip);
    }

    private Coroutine moveCorouine;
    //функция приема точки куда должен идти персонаж
    #region Move
    public void SetNewMoveTarget(Vector2 targetPos, APInteractbleObjController interactiveButton = null)
    {
        if (!GameStateController.Instance.gameIsPlayed) return;

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

        while (canMove && Mathf.Abs(transform.position.x - target.x) > 0.05f)
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
        if (interactiveButton != null)
            playerVisual.SetTrigger(interactiveButton.UseObject());
    }
    #endregion

    //фукнкция приема ответа от класса AnswerController
    #region AsnwerController
    public void AnswerConnector(string _text = "", AudioClip _clip = null)
    {
        if (_text != "" && !answerUIPanel.gameObject.activeInHierarchy) 
        {
            float sizeX = Mathf.Clamp(_text.Length, 5, maxLetterInOneLine) * oneLetterParcer;
            float sizeY = panelStartSize.y + ((Mathf.FloorToInt(_text.Length / maxLetterInOneLine) + 1) * oneLetterParcer);

            answerUIPanel.rectTransform.sizeDelta = new Vector2(sizeX, sizeY);
            answerText.rectTransform.sizeDelta = new Vector2(sizeX - oneLetterParcer, sizeY);

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

    private void Update()
    {
        if (!GameStateController.Instance.gameIsPlayed)
        {
            if (moveCorouine != null) StopCoroutine(moveCorouine);
            return;
        } 

        playerVisual.SetFloat("VelocityY", _rb.velocity.y);
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (!GameStateController.Instance.gameIsPlayed) return;

        if (collision.gameObject.GetComponent<DangerObstacle>())
            StartCoroutine(Die());

        if (collision.gameObject.tag != "Bottom")
        { 
            if (moveCorouine != null) StopCoroutine(moveCorouine);
            canMove = false;
            playerVisual.SetBool("Run", false);
        }
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (!GameStateController.Instance.gameIsPlayed) return;

        if (collision.GetComponent<DangerObstacle>())
            StartCoroutine(Die());

        if (collision.GetComponent<APInteractbleObjController>() && !collision.GetComponent<APInteractbleObjController>().needUse)
            collision.GetComponent<APInteractbleObjController>().UseObject();
    }

    private IEnumerator Die()
    {
        canMove = false;
        playerVisual.SetTrigger("Dead");

        yield return new WaitForSeconds(1);
        GameStateController.Instance.GameEnd(false);
    }
}
