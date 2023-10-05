using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Armor : Item
{
    [Header("Armor Properties")]
    [SerializeField] private float resistance;
    public float Resistance { get { return resistance; } }
}
