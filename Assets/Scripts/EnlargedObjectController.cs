using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnlargedObjectController : MonoBehaviour
{
    [SerializeField] private GameObject panel;
    [SerializeField] private RectTransform enlargedPanel;

    private Vector2 startSizePanel;

    private void Start()
    {
        startSizePanel = enlargedPanel.sizeDelta;
    }

    public void ActivatePanel(Sprite _sprite, Vector2 startPos)
    {
        panel.SetActive(true);
        enlargedPanel.GetComponent<Image>().sprite = _sprite;
        enlargedPanel.sizeDelta = Vector2.zero;
        enlargedPanel.transform.position = startPos;

        StartCoroutine(Activate());
    }

    private IEnumerator Activate()
    {
        yield return new WaitForFixedUpdate();

        Vector2 centreScreenPos = new Vector2(Screen.width / 2, Screen.height / 2);

        while (Vector2.Distance(enlargedPanel.transform.position, centreScreenPos) > 5f)
        {
            enlargedPanel.transform.position = Vector2.Lerp(enlargedPanel.transform.position, centreScreenPos, 0.05f);
            enlargedPanel.sizeDelta = Vector2.Lerp(enlargedPanel.sizeDelta, startSizePanel, 0.05f);
            yield return new WaitForFixedUpdate();
        }
    }
}
