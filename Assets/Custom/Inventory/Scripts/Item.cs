using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Item {

    [SerializeField] private List<UseAnimSO> animations;
    public List<UseAnimSO> Animations { get { return animations; } }


    [Header("Inventory Properties")]

    [SerializeField] private string itemID = "";
    public string ItemID { get { return itemID; } }

    [SerializeField] private string itemName;
    public string ItemName { get { return itemName; } }

    [SerializeField] private Sprite icon;
    public Sprite Icon { get { return icon; } set { this.icon = value; } }


        [Header("World Properties")]

    [SerializeField] private GameObject prefab;
    public GameObject Prefab { get { return prefab; } }

    [SerializeField] private bool deployable;
    public bool Deployable { get { return deployable; } set { this.deployable = value; } }



    private ItemWrapper instance;
    public ItemWrapper Instance { get { return instance; } set { this.instance = value; } }


    private bool deployed = false;
    public bool Deployed { get { return deployed; } set { this.deployed = value; } }


    public void Deploy() {
        if(deployable && !deployed) {
            var item = GameObject.Instantiate(prefab, Player.Instance.WeaponParent);
            item.AddComponent<ItemWrapper>().itemInstance = this;

            MeshCollider collider = item.AddComponent<MeshCollider>();
            collider.convex = true;
            collider.isTrigger = true;

            instance = item.GetComponent<ItemWrapper>();
            deployed = true;
        }
    }
}




