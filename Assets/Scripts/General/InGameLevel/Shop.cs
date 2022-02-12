using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shop : MonoBehaviour
{
    [SerializeField] private MarketItem[] items = new MarketItem[9];
    [SerializeField] private GameObject BuyPanel;

    public GameObject Choice;
    [SerializeField] private APInventoryController APInventoryController;

    [SerializeField] private string TriggerAnim;

    private Sprite itemSprite;

    private void Start()
    {
        BuyPanelOff();
        Choice.SetActive(false);

        for (int i = 0; i < items.Length; i++)
        {
            if (items[i].ItemPrice == 0)
            {
                items[i].PriceText.text = "Закрыто";
                continue;
            }

            items[i].PriceText.text = items[i].ItemPrice.ToString();
        }
    }

    public int ReturnId(string name)
    {
        for (int i = 0; i < items.Length; i++)
        {
            if (items[i].ButtonImage != null)
            {
                if (items[i].ButtonImage.name == name)
                    return items[i].ItemId;
            }
        }

        return -1;
    }

    public int ReturnPrice(string name)
    {
        for (int i = 0; i < items.Length; i++)
        {
            if (items[i].ButtonImage != null)
            {
                if (items[i].ButtonImage.name == name)
                    return items[i].ItemPrice;
            }
        }

        return -1;
    }

    private int buyItemId;
    private int buyItemPrice;
    private Vector2 buyItemPos;

    public void ChoiceItem(int id, int price, Sprite sprite, Vector2 pos)
    {
        buyItemId = id;
        buyItemPrice = price;
        itemSprite = sprite;
        buyItemPos = pos;

        for (int i = 0; i < items.Length; i++)
        {
            if (items[i].ItemId == id)
            {
                if (items[i].itemBuy == false)
                    Choice.SetActive(true);
            }
        }
    }


    public void ChoiceYes()
    {
        if (DimondLoot.Diamond >= buyItemPrice)
        {
            for (int i = 0; i < items.Length; i++)
            {
                if (items[i].ItemId == buyItemId)
                {
                    items[i].itemBuy = true;
                    items[i].PriceText.text = "Куплено";
                }
            }

            DimondLoot.Diamond -= buyItemPrice;

            Choice.SetActive(false);
            APInventoryController.SetNewItemToInventory(buyItemId, itemSprite, buyItemPos);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.GetComponent<APPlayerController>())
        {
            BuyPanelOff();
            ChoiceNo();
        }
    }

    public void BuyPanelOff()
    {
        BuyPanel.SetActive(false);
    }

    public string BuyPanelOn()
    {
        BuyPanel.SetActive(true);

        return TriggerAnim;
    }

    public void ChoiceNo()
    {
        Choice.SetActive(false);
    }
}

[Serializable]
public class MarketItem 
{
    public int ItemId;
    public int ItemPrice;
    public Text PriceText;
    public Image ButtonImage;
    public bool itemBuy;
}
