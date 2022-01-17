using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class APItemController : MonoBehaviour
{
    private SpriteRenderer itemImage;
    [SerializeField] private int ItemID = 0;
    private string itemName = "";

    private void Start()
    {
        if (AllItemAsset.Instance)
        {
            itemImage.sprite = AllItemAsset.Instance.GetItemSprite(ItemID);
            itemName = AllItemAsset.Instance.GetItemName(ItemID);
        }
    }

}
