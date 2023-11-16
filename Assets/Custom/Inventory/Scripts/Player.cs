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

    private InventoryManager manager;
    private GameObject currentMenu;
    [SerializeField] private Weapon currentWeapon;
    [SerializeField] private Transform weaponParent;

    public Transform WeaponParent { get { return weaponParent; } }
    public Weapon CurrentWeapon { get { return currentWeapon; } }

    private void Awake() {
        manager = InventoryManager.Instance;
        Instance = this;
    }

    private void Update(){
        if(Input.GetKeyDown(KeyCode.I) | Input.GetKeyDown(KeyCode.Tab)) {
            inventory.SetActive(!inventory.activeSelf);
            if (inventory.activeInHierarchy) { currentMenu = inventory; } else { currentMenu = null; }
        }

        if(currentMenu != null) {
            if (Input.GetKeyDown(KeyCode.Escape)) {
                currentMenu.SetActive(false);
                currentMenu = null;
            }
        } 

        if(Input.GetKeyDown(KeyCode.Alpha1)) {
            currentWeapon = InventoryManager.Instance.EquippedWeapons[0].Item as Weapon;
            Debug.Log("Deploy: " + currentWeapon.ItemName);
            currentWeapon.Deploy();
        }
    }

    public void SelectWeapon() {

    }
}

