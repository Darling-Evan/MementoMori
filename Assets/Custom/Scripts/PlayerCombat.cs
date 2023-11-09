using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using Unity.PlasticSCM.Editor.WebApi;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    //most of these properties (with the exception of the two cooldowns) shouldn't need to be changed much if at all. They are just there for testing and to make me happy. Dont worry about them :)
    [SerializeField, Tooltip("Minimum time allowed between combos")] private float comboCooldown = 0.2f;
    [SerializeField, Tooltip("This is the percentage that an attack animation must be played to before the combo can exit into the next state (this shouldn't need to change much)"), Range(0,1)] private float animCompletion = 0.9f;
    [SerializeField, Tooltip("Time delay before combo ends")] private float endDelay = 1f;
    public List<AttackSO> combo;

    private float lastClick;
    private float lastCombo;
    private int comboIndex = 0;

    private Animator anim;
    private AttackSO currentAttack;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButtonDown("Fire1")) {
            Attack();
        }
        ExitAttack();
    }

    private void Attack() {
        if(Time.time - lastCombo > comboCooldown && comboIndex < combo.Count) {
            CancelInvoke("EndCombo");

            currentAttack = combo[comboIndex];
            if(Time.time - lastClick >= currentAttack.animOverride["AttackPH"].length * currentAttack.MinAnimDuration) {
                anim.runtimeAnimatorController = combo[comboIndex].animOverride;
                //Player.Instance.CurrentWeapon.Damage = combo[comboIndex].damage;

                anim.Play("Attack", 0, 0);
                comboIndex++;
                lastClick = Time.time;

                if (comboIndex > combo.Count) {
                    comboIndex = 0;
                }
            }
        }
    }
    
    private void ExitAttack() {
        var stateInfo = anim.GetCurrentAnimatorStateInfo(0);
        if(stateInfo.normalizedTime > animCompletion && stateInfo.IsTag("Attack")) {
            Invoke("EndCombo", endDelay);
        }
    }

    private void EndCombo() {
        comboIndex = 0;
        lastCombo = Time.time;
    }
}
