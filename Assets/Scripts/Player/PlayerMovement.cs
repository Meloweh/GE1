using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : EntityHostile {
    [SerializeField] private Sprite face;
    [SerializeField] private DialogManager dialogManager;
    private Dialog dialog;

    private void OnMovement(InputValue iv) {
        if (IsHurtLocked()) {
            return;
        }
        movement = iv.Get<Vector2>();
        UpdateMovementAnimation();
        if (movement.x != 0 || movement.y != 0) {
            SetDirection(movement.normalized * 10);
        }
            
    }

    private void Update() {
        if (IsHurtLocked()) {
            return;
        }
        if (Input.GetKeyDown(KeyCode.Z)) {
            GetAnimator().SetTrigger("isMelee");
            isAttacking = true;
        }

        /*if (Input.GetKeyUp(KeyCode.U) && !DialogManager.instance.IsBusy()) {
            DoSampleDialog();
        }*/
    }

    private protected new void FixedUpdate() {
        base.FixedUpdate();
        if (IsHurtLocked()) {
            return;
        }
        
        if (!isAttacking) {
            DoMovement(false);
        }
        isAttacking = IsAttackAnimation();

        // walk on ice
        //rigid.AddForce(movement * walkSpeed);
    }

    private void DoSampleDialog() {
        dialog = new Dialog(face);
        dialog.Add("Hello dear <color=red>friend</color>! How are you?");
        dialog.Add("This is a <color=blue>color</color> test.");
        dialog.Add("<color=blue>Enjoy!</color>");
        dialogManager.StartDialog(dialog);

    }
}
