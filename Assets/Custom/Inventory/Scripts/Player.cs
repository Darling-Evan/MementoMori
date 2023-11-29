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
    public static bool inventoryIsShowing = false;
    [SerializeField] private Item currentItem;
    [SerializeField] private Transform weaponParent;

    public Transform WeaponParent { get { return weaponParent; } }
    public Item CurrentItem { get { return currentItem; } set { currentItem = value; } }

    private InventoryManager manager;
    private Use useAnimator;
    private GameObject currentMenu;





    private void Awake() {
        manager = InventoryManager.Instance;
        Instance = this;

        useAnimator = GetComponent<Use>();
    }

    private void Update() {

        if (MapShow.mapIsShowing == false && CardShow.CardsAreShowing == false && PauseMenu.GameIsPaused == false)
        {
            if (Input.GetKeyDown(KeyCode.I) | Input.GetKeyDown(KeyCode.Tab))
            {
                HotBar.Instance.gameObject.SetActive(false);
                inventoryIsShowing = true;
                inventory.SetActive(!inventory.activeSelf);
                if (inventory.activeInHierarchy) 
                { 
                    currentMenu = inventory;
                } 
                else 
                {
                    HotBar.Instance.gameObject.SetActive(true);
                    currentMenu = null;
                    inventoryIsShowing = false;
                }
            }

            if (currentMenu != null)
            {
                if (Input.GetKeyDown(KeyCode.Escape))
                {
                    HotBar.Instance.gameObject.SetActive(true);
                    currentMenu.SetActive(false);
                    currentMenu = null;
                    inventoryIsShowing = false;
                }
            }
        }

        Equip(CheckHotbar());
    }
    
    private void Equip(int i) {

        if(i != 0 && InventoryManager.Instance.HotBar[i-1] != null) {
            if (InventoryManager.Instance.HotBar[i - 1].Item != null) {

                if (CurrentItem != InventoryManager.Instance.HotBar[i - 1].Item && CurrentItem.Deployed == true) {
                    CurrentItem.Deployed = false;
                    Destroy(CurrentItem.Instance.gameObject);
                }
                CurrentItem = InventoryManager.Instance.HotBar[i - 1].Item;
                useAnimator.Combo = currentItem.Animations;

                Debug.Log("Deploy: " + CurrentItem.ItemName);
                CurrentItem.Deploy();
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

