using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class AllItemAsset : MonoBehaviour
{
    private static AllItemAsset instance;
    public static AllItemAsset Instance => instance;

    [Header("Набор итемов + картинки")]
    [SerializeField] private ItemInfo[] allItems = new ItemInfo[0];

    [SerializeField] private Sprite defaultSprite;

    public void Awake()
    {
        instance = this;
    }

    public Sprite GetItemSprite(int ID)
    {
        for (int i = 0; i < allItems.Length; i++)
            if (allItems[i].itemID == ID)
                return allItems[i].itemSprite;

        return defaultSprite;
    }

    public string GetItemName(int ID)
    {
        for (int i = 0; i < allItems.Length; i++)
            if (allItems[i].itemID == ID)
                return allItems[i].itemName;

        return "not found";
    }
}

[Serializable]
public class ItemInfo
{
    public int itemID;
    public Sprite itemSprite;
    public string itemName;
}