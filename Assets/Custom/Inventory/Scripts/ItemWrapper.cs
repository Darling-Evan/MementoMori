using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vertx.Attributes;

public class ItemWrapper : MonoBehaviour {
    [SerializeReference, ReferenceDropdown]

    public Item itemInstance;

    private void OnTriggerEnter(Collider other) {
        if (!itemInstance.Equipped && other.CompareTag("Player")) {
            InventoryManager.Instance.InventoryAdd(itemInstance);
            Destroy(gameObject);
        }
    }
}