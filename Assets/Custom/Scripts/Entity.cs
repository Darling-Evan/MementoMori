using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.Rendering.DebugUI;

public class Entity : MonoBehaviour
{

    [SerializeField] private float maxHealth;
    [SerializeField] private Slider healthBar;

    private float health;

    public float Health { get { return health; } }
    public float MaxHealth { get { return maxHealth; } }

    private void Start() {
        health = maxHealth;
    }

    private void Update() { 
        if(healthBar) {
            healthBar.value = health / maxHealth;
        }
    }

    public float TakeDamage(float damage) {
        if(health - damage <= 0) {
            health = 0;
        }
        else {
            health -= damage;
        }
        return health;
    }
}
