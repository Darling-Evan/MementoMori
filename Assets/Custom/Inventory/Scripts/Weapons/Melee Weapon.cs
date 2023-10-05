using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeWeapon : Weapon
{
    [Header("Melee Weapon Properties")]
    //Private
    [SerializeField] private float damage;
    [SerializeField] private float attackSpeed;
    [SerializeField] private float attackRange;
    [SerializeField] private List<AnimationClip> lightAttacks;
    [SerializeField] private List<AnimationClip> heavyAttacks;
    [SerializeField] private List<AnimationClip> specialAttacks;
    //Getters n Setters
    public float Damage { get { return damage; } }
}
