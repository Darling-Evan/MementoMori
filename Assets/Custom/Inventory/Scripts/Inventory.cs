using StarterAssets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[System.Serializable]
public class Inventory : MonoBehaviour
{
    public static Inventory Instance { get; private set; }

    [SerializeField] private Transform anchor;
    [SerializeField] private RectTransform display;
    [SerializeField] private Camera cam;
    [SerializeField] private float rotSensitivty = 15f;
    [SerializeField] private Quaternion startingRotation;

    private float xRot;
    private Player player;
    private InventoryManager manager;
    private PlayerInput playerInput;
    private Animator anim;


    private void Awake() {
        Instance = this;
        player = Player.Instance;
        manager = InventoryManager.Instance;

        playerInput = player.GetComponent<PlayerInput>();
        anim = player.GetComponent<Animator>();
    }

    private void Update() {
        anchor.position = player.transform.position + Vector3.up;

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        if (Input.GetMouseButton(0)) {
            rotatewithPhysicis();
        }
    }

    private void OnEnable() {
        playerInput.enabled = false;
        anim.SetFloat("Speed", 0);
        anim.SetBool("Inventory" , true);

        startingRotation = player.transform.rotation;
        anchor.GetChild(0).localRotation = startingRotation;

        manager.DispAll();
    }

    private void OnDisable() {
        if(player != null) {
            anim.SetBool("Inventory" , false);
            playerInput.enabled = true;
            anim.StopPlayback();
        }

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        if (anchor != null) {
            anchor.GetChild(0).GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
        }
    }

    private void rotatewithPhysicis() {
        anchor.GetChild(0).GetComponent<Rigidbody>().AddTorque(Vector3.up * Input.GetAxis("Mouse X") * rotSensitivty, ForceMode.Acceleration);
    }
}

