using StarterAssets;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(Victim))]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(ThirdPersonController))]
//Theres some more but Im pretty sure they and handled by other scripts / we arent going to need to make a fresh player anyways (besides changing the model)

[System.Serializable]
public class Player : MonoBehaviour {

    public static Player Instance { get; private set; }

    [SerializeField] private GameObject inventory;
    [SerializeField] private Item currentWeapon;
    [SerializeField] private Transform weaponParent;

    public Transform WeaponParent { get { return weaponParent; } }
    public Item CurrentWeapon { get { return currentWeapon; } }

    private InventoryManager manager;
    private GameObject currentMenu;





    private void Awake() {
        manager = InventoryManager.Instance;
        Instance = this;
    }

    private void Update() {
        if (Input.GetKeyDown(KeyCode.I) | Input.GetKeyDown(KeyCode.Tab)) {
            inventory.SetActive(!inventory.activeSelf);
            if (inventory.activeInHierarchy) { currentMenu = inventory; } else { currentMenu = null; }
        }

        if (currentMenu != null) {
            if (Input.GetKeyDown(KeyCode.Escape)) {
                currentMenu.SetActive(false);
                currentMenu = null;
            }
        }

        Equip(CheckHotbar());
    }
    
    private void Equip(int i) {

        if(i != 0 && InventoryManager.Instance.HotBar[i-1] != null) {
            if (InventoryManager.Instance.HotBar[i - 1].Item != null) {

                if (currentWeapon != InventoryManager.Instance.HotBar[i - 1].Item && currentWeapon.Deployed == true) {
                    currentWeapon.Deployed = false;
                    Destroy(currentWeapon.Instance.gameObject);
                }
                currentWeapon = InventoryManager.Instance.HotBar[i - 1].Item as Weapon;

                Debug.Log("Deploy: " + currentWeapon.ItemName);
                currentWeapon.Deploy();
            }
        }
    }


    private int CheckHotbar() {
        if (Input.GetKeyDown(KeyCode.Alpha1)) {
            return 1;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2)) {
            return 2;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3)) {
            return 3;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4)) {
            return 4;
        }
        else return 0;
    }
}

