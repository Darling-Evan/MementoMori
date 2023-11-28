using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EquipmentSlot : MonoBehaviour
{
    [SerializeReference]
    private Item item;

    [SerializeField] private GameObject slot;

    [SerializeField] private string typeID;
    public int slotNumber;

    private Image icon;
    private Image hbIcon;

    private Button button;
    private InventoryManager manager;

    public Item Item {
        get { return item; }
        set { if(value == null || value.ItemID.Contains(typeID)) { item = value; } }
    }

    private void Awake() {
        manager = InventoryManager.Instance;
        manager.HotBar[slotNumber] = this;
        button = gameObject.GetComponent<Button>();

        icon = gameObject.transform.Find("Icon").GetComponent<Image>();
        hbIcon = slot.transform.Find("Icon").GetComponent<Image>();

        if(icon.sprite == null) {
            icon.enabled = false;
            hbIcon.enabled = false;
        }
    }

    public void Populate() {
        icon.sprite = item.Icon;
        hbIcon.sprite = item.Icon;

        icon.enabled = true;
        hbIcon.enabled = true;
    }

    public void Equip() {
        if (manager.selectedItem != null) {
            manager.Equip(slotNumber, manager.selectedItem.item);
        }
        else {
            manager.expectedSlot = this;
            manager.DispID(typeID);
        }
    }
}
