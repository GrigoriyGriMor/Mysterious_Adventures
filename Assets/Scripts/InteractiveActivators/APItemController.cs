using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class APItemController : MonoBehaviour
{
    [SerializeField] private SpriteRenderer itemImage;
    [SerializeField] private int ItemID = 0;
    private string itemName = "";

    [Header("Триггер Анимации взаимодействий")]
    [SerializeField] private string triggersForAnim = "";

    [Header("Анимация активного обьекта")]
    [SerializeField] private Animator useObjAnim;
    [SerializeField] private ParticleSystem particle;

    private bool itemWasUsed = false;

    private void Start()
    {
        if (AllItemAsset.Instance)
        {
            itemImage.sprite = AllItemAsset.Instance.GetItemSprite(ItemID);
            itemName = AllItemAsset.Instance.GetItemName(ItemID);
        }
    }

    public string UseObject()
    {
        if (itemWasUsed) return triggersForAnim;

        itemWasUsed = true;
        StartCoroutine(SetItemToInventory());
        return triggersForAnim;
    }

    private IEnumerator SetItemToInventory()
    {
        yield return new WaitForSeconds(0.5f);

        if (APInventoryController.Instance) APInventoryController.Instance.SetNewItemToInventory(ItemID, itemImage.sprite, Camera.main.WorldToScreenPoint(transform.position));
        gameObject.SetActive(false);

        if (useObjAnim != null)
            useObjAnim.SetTrigger("Start");

        if (particle != null)
            particle.Play();
    }

    #region maybe be need later
    public Sprite GetItemSprite()
    {
        return itemImage.sprite;
    }

    public string GetItemName()
    {
        return itemName;
    }
    #endregion
}
