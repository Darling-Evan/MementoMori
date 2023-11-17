using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : Item
{
    [Header("Weapon Properties")]
    [SerializeField] private float attackSpeed;
    [SerializeField] private float attackRange;

    private float damage;

    public float Damage { get { return damage; } set { this.damage = value; } }
}
