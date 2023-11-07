using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "I Want To Kiss John", menuName = "Create New Attack")]
public class AttackSO : ScriptableObject
{
    public AnimatorOverrideController animOverride;

    public float damage;

}
