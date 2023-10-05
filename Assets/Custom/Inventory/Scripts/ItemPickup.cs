using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vertx.Attributes;

public class ItemPickup : MonoBehaviour {
    [SerializeReference, ReferenceDropdown]
    public Item itemInstance;

    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Player")) {
            InventoryManager.Instance.InventoryAdd(itemInstance);
            Destroy(gameObject);
        }
    }
}