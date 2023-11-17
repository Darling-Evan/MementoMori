using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;


public class UseAnimSO : ScriptableObject
{
    [Header("Animation Properties")]
    public AnimatorOverrideController animOverride;
    [SerializeField, Range(0,1)] private float minAnimDuration = 0.95f;

    [Header("Attack Properties")]
    [SerializeField] private float damage;

    public float MinAnimDuration { get => minAnimDuration; }
    public float Damage { get => damage; }
}
