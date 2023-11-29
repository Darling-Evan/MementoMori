using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vertx.Attributes;

public class ItemWrapper : MonoBehaviour {
    [SerializeReference, ReferenceDropdown]

    public Item itemInstance;

    private void OnTriggerEnter(Collider other) {
        if (!itemInstance.Deployed && other.CompareTag("Player")) {
            PickUp();
        }

        if (other.CompareTag("Enemy")) {
            if (itemInstance is MeleeWeapon weapon && weapon.AttackTriggered) {
                var enemyHP = other.GetComponent<Entity>().TakeDamage(weapon.Damage);
                Debug.Log("Enemy HP: " + enemyHP);
            }
        }
    }

    public  void PickUp() {
        Debug.Log("Picked up " + itemInstance.ItemName);
        InventoryManager.Instance.InventoryAdd(itemInstance);
        Destroy(gameObject);
    }
}