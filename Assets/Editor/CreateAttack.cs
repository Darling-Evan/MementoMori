using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using StarterAssets;
using UnityEngine.TextCore.Text;
using System;
using UnityEngine.WSA;
using Codice.Client.BaseCommands.Import;

public class CreateAttack
{
    //This script adds a menu item that shows up when you right click an asset in the project window
    //If you run it on a animation clip(s) it will create a AttackSO and AnimatorOverrideController for each clip that you have selected
    // The new assets will be set up already and can be found at Assets/Custom/Player/Combat/
    // They will be sorted into folders sharing the same name as the animation
    //  This system does not support animations with the same name

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
                //Create Folder
                string folderGUID = AssetDatabase.CreateFolder("Assets/Custom/Player/Combat/Meele Attacks", item.name);
                string folder = AssetDatabase.GUIDToAssetPath(folderGUID) + "/";

                //Duplicate Animation Clip
                string dupePath = folder + item.name + ".fbx";
                AssetDatabase.CopyAsset(AssetDatabase.GetAssetPath(item), dupePath);

                //Create AttackSO
                UseAnimSO attackSO = ScriptableObject.CreateInstance<UseAnimSO>();
                AssetDatabase.CreateAsset(attackSO, folder + item.name + ".asset");
                
                //Create Override Controller
                AnimatorOverrideController overrideController = new AnimatorOverrideController(anim);
                //overrideController["UseAnimation"] = Resources.Load<AnimationClip>(AssetDatabase.GetAssetPath(item));
;               AssetDatabase.CreateAsset(overrideController, folder + item.name + ".overrideController");

                //Populate AttackSO
                attackSO.animOverride = overrideController;
            }
        }
    }
#endif
}