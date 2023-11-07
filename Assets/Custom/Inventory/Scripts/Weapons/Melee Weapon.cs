using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeWeapon : Weapon
{
    [Header("Melee Weapon Properties")]
    //Private
    [SerializeField] private List<AnimationClip> lightAttacks;
    [SerializeField] private List<AnimationClip> heavyAttacks;
    [SerializeField] private List<AnimationClip> specialAttacks;
    //Getters n Setters
}
