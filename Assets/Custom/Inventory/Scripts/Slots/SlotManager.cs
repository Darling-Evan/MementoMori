using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using UnityEngine;
using UnityEngine.UI;

public class SlotManager : MonoBehaviour
{
    [SerializeReference]
    public Item item;

    private InventoryManager manager;
    private Image icon;

    private void Awake() {
        manager = InventoryManager.Instance;
        icon = gameObject.transform.Find("Item").GetComponent<Image>();
    }

    public void Populate() {
        //gameObject.transform.Find("Item").GetComponent<Image>().sprite = item.Icon;
        icon.sprite = item.Icon;
    }

    
    public void Equip() {
        if(item is Armor) {
            switch (item) {
                case Helmet:
                    manager.Equip(0, (Armor)item);
                    break;
                case Gauntlets:
                    manager.Equip(1, (Armor)item);
                    break;
                case ChestPlate:
                    manager.Equip(2, (Armor)item);
                    break;
                case Leggings:
                    manager.Equip(3, (Armor)item);
                    break;
            }
        }
        else { 
            if (manager.expectedSlot != null) {
            manager.Equip(manager.expectedSlot.slotNumber, item);
            }
            else {
                manager.selectedItem = this;
            }
        }
    }
}
