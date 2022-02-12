using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuyItem : MonoBehaviour
{
    [SerializeField] private Shop Shop;
    [SerializeField] private AllItemAsset ItemAssets;

    private int price;
    private int id;
    private Sprite itemSprite;

    private void Start()
    {
        price = Shop.ReturnPrice(gameObject.name);
        id = Shop.ReturnId(gameObject.name);

        if (id != -1)
        {
            itemSprite = ItemAssets.GetItemSprite(id);
            gameObject.GetComponent<Image>().sprite = itemSprite;
        }
    }

    public void ShopItems()
    {
        if (id != -1 && price != -1)
        {
            if (Shop.Choice.gameObject.activeSelf == false)
                Shop.ChoiceItem(id, price, itemSprite, transform.position);
        }
    }
}
