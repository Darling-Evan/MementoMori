using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ArmorSlot : MonoBehaviour {
    [SerializeReference]
    private Item item;

    [SerializeField] private string typeID;
    public int slot;

    private Image icon;
    private InventoryManager manager;

    public Item Item { 
        get { return item; }
        set { if (value.ItemID.Contains(typeID) || value == null) { item = value; }}
    }
    private void Awake() {
        manager = InventoryManager.Instance;
        manager.EquippedArmor[slot] = this;
        icon = gameObject.transform.Find("Icon").GetComponent<Image>();

        if (icon.sprite == null) {
            icon.enabled = false;
        }
    }

    public void Populate() {
        icon.sprite = item.Icon;
        icon.enabled = true;
    }
}