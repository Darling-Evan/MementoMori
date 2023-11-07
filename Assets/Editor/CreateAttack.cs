using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using StarterAssets;
using UnityEngine.TextCore.Text;
using System;

public class CreateAttack
{
    //This script adds a menu item that shows up when you right click an asset in the project window
    //If you run it on a animation clip(s) it will create a AttackSO and AnimatorOverrideController for each clip that you have selected
    // The new assets will be set up already and can be found at Assets/Custom/Player/Combat/
    // They will not be sorted into folders so you will have to do that yourself

    //This script can only be run in the editor.

#if UNITY_EDITOR
    [MenuItem("Assets/CreateAttack")]
    private static void CreateAttackAssets() {
        var anim = AssetDatabase.LoadAssetAtPath<RuntimeAnimatorController>("Assets/StarterAssets/ThirdPersonController/Character/Animations/StarterAssetsThirdPerson.controller");
        if(anim == null) {
            Debug.LogError("Could not find animator controller -- Check path and try again");
            return;
        }

        foreach (var item in Selection.objects) {
            if(item is AnimationClip) {
                AttackSO attackSO = ScriptableObject.CreateInstance<AttackSO>();
                AssetDatabase.CreateAsset(attackSO, "Assets/Custom/Player/Combat/" + item.name + ".asset");

                AnimatorOverrideController overrideController = new AnimatorOverrideController(anim);
                overrideController["AttackPH"] = item as AnimationClip;
                AssetDatabase.CreateAsset(overrideController, "Assets/Custom/Player/Combat/" + item.name + ".overrideController");

                attackSO.animOverride = overrideController;
            }
        }
    }
#endif
}