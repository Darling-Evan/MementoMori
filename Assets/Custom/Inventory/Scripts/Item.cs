using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Item {

    [SerializeField] private GameObject prefab;
    public GameObject Prefab { get { return prefab; } }

    [SerializeField] private string itemID = "";
    public string ItemID { get { return itemID; } }

    [SerializeField] private string itemName;
    public string ItemName { get { return itemName; } }

    [SerializeField] private Sprite icon;
    public Sprite Icon { get { return icon; } set { this.icon = value; } }

    private bool equipped;
    public bool Equipped { get { return equipped; } set { this.equipped = value; } }

    [SerializeField] private bool deployable;
    public bool Deployable { get { return deployable; } set { this.deployable = value; } }


    public void Deploy() {
        if(deployable) {
            var item = GameObject.Instantiate(prefab, Player.Instance.WeaponParent);
            item.AddComponent<ItemWrapper>().itemInstance = this;

        }
    }
}




