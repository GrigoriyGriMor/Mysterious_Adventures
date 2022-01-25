//НА ВСЕ ЭЛЕМЕНТЫ ГОРИЗОНТАЛЬНОГО ПОЛА, НЕОБХОДИМО СТАВИТЬ ТЕГ "Bottom", интаче персонаж будет останавливаться заходя на каждый новый блок

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
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

    private MoveType.moveMode moveType = MoveType.moveMode._onlyHorizontal;

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
    public void SetNewMoveTarget(Vector2 targetPos, MonoBehaviour interactiveButton = null)
    {
        if (!GameStateController.Instance.gameIsPlayed) return;

        switch (moveType)
        {
            case MoveType.moveMode._onlyHorizontal:
                target = new Vector2(targetPos.x, transform.position.y);
                break;
            case MoveType.moveMode._onlyVertical:
                target = new Vector2(transform.position.x, targetPos.y);
                break;
            case MoveType.moveMode._allVectors:
                target = new Vector2(targetPos.x, targetPos.y);
                break;
        }

        if (transform.position.x > target.x && playerVisual.transform.localScale.x > 0)
            playerVisual.transform.localScale = new Vector3(playerVisual.transform.localScale.x * (-1), playerVisual.transform.localScale.y, playerVisual.transform.localScale.z);
        else
           if (transform.position.x < target.x && playerVisual.transform.localScale.x < 0)
                playerVisual.transform.localScale = new Vector3(playerVisual.transform.localScale.x * (-1), playerVisual.transform.localScale.y, playerVisual.transform.localScale.z);

        if (moveCorouine != null)
            StopCoroutine(moveCorouine);

        moveCorouine = StartCoroutine(Move(interactiveButton));
    }

    private IEnumerator Move(MonoBehaviour interactiveButton = null)
    {
        canMove = true;

        switch (moveType)
        {
            case MoveType.moveMode._onlyHorizontal://просто бег влево/вправо
                playerVisual.SetBool("Run", true);

                while (canMove && Mathf.Abs(transform.position.x - target.x) > 0.05f)
                {
                    if (playerVisual.transform.localScale.x > 0)
                        transform.position = new Vector2((transform.position.x + speed * Time.deltaTime), transform.position.y);
                    else
                        transform.position = new Vector2((transform.position.x - speed * Time.deltaTime), transform.position.y);

                    yield return new WaitForFixedUpdate();
                }
                playerVisual.SetBool("Run", false);
                break;

            case MoveType.moveMode._onlyVertical://Подъем вверх/вниз
                playerVisual.SetBool("MoveVertical", true);

                while (canMove && Mathf.Abs(transform.position.y - target.y) > 0.05f)
                {
                    if (transform.position.y > target.y)
                        transform.position = new Vector2(transform.position.x, (transform.position.y - speed * Time.deltaTime));
                    else
                        transform.position = new Vector2(transform.position.x, (transform.position.y + speed * Time.deltaTime));

                    Debug.LogError(Mathf.Abs(transform.position.y - target.y));
                    yield return new WaitForFixedUpdate();
                }

                playerVisual.SetBool("MoveVertical", false);
                break;

            case MoveType.moveMode._allVectors://Подъем вверх/вниз/влево/вправо
                playerVisual.SetBool("MoveVertical", true);

                while (canMove && Vector2.Distance(transform.position, target) > 0.05f)
                {
                        transform.position = new Vector2(
                            (transform.position.x > target.x) ? (transform.position.x - speed * Time.deltaTime) : (transform.position.x + speed * Time.deltaTime), 
                            (transform.position.y > target.y) ? (transform.position.y - speed * Time.deltaTime) : (transform.position.y + speed * Time.deltaTime));

                    yield return new WaitForFixedUpdate();
                }

                playerVisual.SetBool("MoveVertical", false);
                break;
        }

        canMove = false;

        yield return new WaitForFixedUpdate();
        if (interactiveButton != null)
        {
            if (interactiveButton is APInteractbleObjController _classIntObj)
                playerVisual.SetTrigger(_classIntObj.UseObject());
            else
            {
                if (interactiveButton is APItemController _classItem)
                    playerVisual.SetTrigger(_classItem.UseObject());
                else
                    if (interactiveButton is EnlargedObject _classEnlargedObj)
                        playerVisual.SetTrigger(_classEnlargedObj.UseObject());
            }
        }
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

    public void SetNewMoveType(MoveType.moveMode _moveType = MoveType.moveMode._onlyHorizontal)
    {
        moveType = _moveType;

        switch (_moveType)
        {
            case MoveType.moveMode._onlyHorizontal:
                _rb.simulated = true;
                playerVisual.SetTrigger("HorizontalIdle");
                break;
            case MoveType.moveMode._onlyVertical:
                _rb.simulated = false;
                playerVisual.SetTrigger("VerticalIdle");
                break;
            case MoveType.moveMode._allVectors:
                _rb.simulated = false;
                playerVisual.SetTrigger("AllVectorIdle");
                break;
        }
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (!GameStateController.Instance.gameIsPlayed) return;

        if (collision.GetComponent<DangerObstacle>())
            StartCoroutine(Die());

        if (collision.GetComponent<APInteractbleObjController>() && !collision.GetComponent<APInteractbleObjController>().needUse)
            collision.GetComponent<APInteractbleObjController>().UseObject();

        if (collision.GetComponent<LadderActivator>())
                SetNewMoveType(MoveType.moveMode._allVectors);
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.GetComponent<LadderActivator>())
        {
            if (moveType == MoveType.moveMode._allVectors)
                SetNewMoveType(MoveType.moveMode._onlyHorizontal);
        }
    }

    private IEnumerator Die()
    {
        canMove = false;
        playerVisual.SetTrigger("Dead");

        yield return new WaitForSeconds(1);
        GameStateController.Instance.GameEnd(false);
    }
}

[Serializable]
public class MoveType
{
    public enum moveMode { _onlyVertical, _onlyHorizontal, _allVectors };

    private moveMode currentMode = moveMode._onlyHorizontal;
}