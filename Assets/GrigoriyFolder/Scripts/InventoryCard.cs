using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryCard : MonoBehaviour
{
    [SerializeField] private Image visual;
    private int itemID = -1;

    public void SetData(int ID)
    {
        itemID = ID;
        visual.sprite = AllItemAsset.Instance.GetItemSprite(ID);
    }

    public void DragItem()
    { 
        
    }

}
