using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Runtime.ConstrainedExecution;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using static UnityEditor.Progress;

[System.Serializable]
public class InventoryManager : MonoBehaviour {

    public RectTransform Content;
    public GameObject SlotPrefab;

    [SerializeField] private Dictionary<string, Item> inventory = new Dictionary<string, Item>();
    [SerializeField] private ArmorSlot[] equippedArmor = new ArmorSlot[4];
    [SerializeField] private EquipmentSlot[] equippedWeapons = new EquipmentSlot[4];

    public static InventoryManager Instance { get; private set; }

    public EquipmentSlot expectedSlot;
    public SlotManager selectedItem;

    //Gets
    public Dictionary<string, Item> Inventory { get { return inventory; } }
    public ArmorSlot[] EquippedArmor { get { return equippedArmor; } }
    public EquipmentSlot[] EquippedWeapons { get { return equippedWeapons; } }

    private void Awake() {
        Instance = this;
    }

    #region AddItems
    public void InventoryAdd(Item item) {
        inventory.Add(item.ItemID ,item);
    }

    public void Equip(int slot, Item item) {
        if (item is Armor) {
            EquipArmor(slot, (Armor)item);
            ResetSlots();
        }
        else if (item is Weapon) {
            EquipWeapon(slot, (Weapon)item);
            ResetSlots();
        }
        RefreshDisp();
    }

    public void EquipArmor(int slot, Armor armor) {
        if (equippedArmor[slot].Item != null) {
            UnequipArmor(slot);
        }
        equippedArmor[slot].Item = armor;
        InventoryRemove(armor);
        equippedArmor[slot].Populate();
    }

    public void EquipWeapon(int slot, Weapon weapon) {
        if (equippedWeapons[slot].Item != null) {
            UnequipWeapon(slot);
        }
        equippedWeapons[slot].Item = weapon;
        InventoryRemove(weapon);
        equippedWeapons[slot].Populate();
    }


    #endregion

    #region RemoveItems
    private void InventoryRemove(Item item) {
        inventory.Remove(item.ItemID);
    }

    private void UnequipArmor(int slot) {
        InventoryAdd(equippedArmor[slot].Item);
        equippedArmor[slot].Item = null;
    }

    private void UnequipWeapon(int slot) {
        InventoryAdd(equippedWeapons[slot].Item);
        equippedWeapons[slot].Item = null;
    }
    private void ResetSlots() {
        expectedSlot = null;
        selectedItem = null;
    }


    #endregion

    #region DisplayItems
    public void DispAll() {
        CleanUp();
        ResetSlots();
        foreach(var item in inventory) {
            GameObject obj = Instantiate(SlotPrefab, Content);
            var slotManager = obj.GetComponent<SlotManager>();
            slotManager.item = item.Value;
            slotManager.Populate();
        }
    }

    public void DispID(string id) {
        CleanUp();
        foreach(string key in inventory.Keys) {
            if (key.Contains(id)) {
                GameObject obj = Instantiate(SlotPrefab, Content);
                var slotManager = obj.GetComponent<SlotManager>();
                slotManager.item = inventory[key];
                slotManager.Populate();
            }
        }
    }

    private void CleanUp() {
        foreach(Transform item in Content) {
               Destroy(item.gameObject);
        }
    }

    private void RefreshDisp() {
        DispAll();
    }


    #endregion
}
