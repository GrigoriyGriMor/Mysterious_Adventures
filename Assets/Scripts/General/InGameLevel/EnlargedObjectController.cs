using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnlargedObjectController : MonoBehaviour
{
    [SerializeField] private GameObject panel;
    [SerializeField] private RectTransform enlargedPanel;
    [SerializeField] private Image image;

    [Header("Скорость появления")]
    [SerializeField] private float speedOpen = 1.5f;

    [SerializeField] private Button[] B_ClosePanel = new Button[2];

    private Vector2 startSizePanel;
    private Vector2 objStartPos;
    private Coroutine panelCoroutine;
    private bool panelIsOpen = false;

    private void Start()
    {
        startSizePanel = enlargedPanel.sizeDelta;
        panel.SetActive(false);

        for (int i = 0; i < B_ClosePanel.Length; i++)
            B_ClosePanel[i].onClick.AddListener(() =>
            {
                if (panelCoroutine == null)
                    panelCoroutine = StartCoroutine(ClosePanel());
            });
    }

    public void ActivatePanel(Sprite _sprite, Vector2 startPos)
    {
        if (panelIsOpen || panelCoroutine != null) return;

        panelIsOpen = true;

        panel.SetActive(true);
        image.sprite = _sprite;
        enlargedPanel.sizeDelta = Vector2.zero;
        enlargedPanel.transform.position = startPos;
        objStartPos = startPos;

        panelCoroutine = StartCoroutine(OpenPanel());
    }

    private IEnumerator OpenPanel()
    {
        yield return new WaitForFixedUpdate();

        Vector2 centreScreenPos = new Vector2(Screen.width / 2, Screen.height / 2);

        while (Vector2.Distance(enlargedPanel.transform.position, centreScreenPos) > 5f)
        {
            enlargedPanel.transform.position = Vector2.Lerp(enlargedPanel.transform.position, centreScreenPos, speedOpen);
            enlargedPanel.sizeDelta = Vector2.Lerp(enlargedPanel.sizeDelta, startSizePanel, speedOpen);
            yield return new WaitForFixedUpdate();
        }

        panelCoroutine = null;
    }

    private IEnumerator ClosePanel()
    {
        yield return new WaitForFixedUpdate();

        while (Vector2.Distance(enlargedPanel.transform.position, objStartPos) > 5f)
        {
            enlargedPanel.transform.position = Vector2.Lerp(enlargedPanel.transform.position, objStartPos, speedOpen);
            enlargedPanel.sizeDelta = Vector2.Lerp(enlargedPanel.sizeDelta, Vector2.zero, speedOpen);
            yield return new WaitForFixedUpdate();
        }

        panel.SetActive(false);
        panelIsOpen = false;
        panelCoroutine = null;
    }
}
