using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Item {
    [SerializeField] private string itemID = "";
    public string ItemID { get { return itemID; } }

    [SerializeField] private string itemName;
    public string ItemName { get { return itemName; } }

    [SerializeField] private Sprite icon;
    public Sprite Icon { get { return icon; } set { this.icon = value; } }



}




