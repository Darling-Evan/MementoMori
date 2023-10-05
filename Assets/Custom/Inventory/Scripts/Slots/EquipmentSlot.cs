using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EquipmentSlot : MonoBehaviour
{
    [SerializeReference]
    private Item item;

    [SerializeField] private string typeID;
    public int slot;

    private Image icon;
    private Button button;
    private InventoryManager manager;

    public Item Item {
        get { return item; }
        set { if(value == null || value.ItemID.Contains(typeID)) { item = value; } }
    }

    private void Awake() {
        manager = InventoryManager.Instance;
        manager.EquippedWeapons[slot] = this;
        button = gameObject.GetComponent<Button>();
        icon = gameObject.transform.Find("Icon").GetComponent<Image>();

        if(icon.sprite == null) {
            icon.enabled = false;
        }
    }

    public void Populate() {
        icon.sprite = item.Icon;
        icon.enabled = true;
    }

    public void Equip() {
        if (manager.selectedItem != null) {
            manager.Equip(slot, manager.selectedItem.item);
        }
        else {
            manager.expectedSlot = this;
            manager.DispID(typeID);
        }
    }
}
