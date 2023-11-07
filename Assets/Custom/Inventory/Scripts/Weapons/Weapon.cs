using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : Item
{
    private float damage;
    [SerializeField] private float attackSpeed;
    [SerializeField] private float attackRange;

    public float Damage { get { return damage; } set { this.damage = value; } }
}
