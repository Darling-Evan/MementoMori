using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Weapon : Item
{
    [Header("Weapon Properties")]
    [SerializeField] private float attackSpeed;
    [SerializeField] private float attackRange;

    private float damage;
    private bool attackTriggered = false;

    public float Damage { get { return damage; } set { this.damage = value; } }
    public bool AttackTriggered { get { return attackTriggered; } set { this.attackTriggered = value; } }
}
