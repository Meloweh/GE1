using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : EntityHostile {
    [SerializeField] private Sprite face;
    private Dialog dialog;
    void Start() {

    }

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

        if (Input.GetKeyUp(KeyCode.U)) {
            DoSampleDialog();
        }
    }

    private protected new void FixedUpdate() {
        base.FixedUpdate();
        if (IsHurtLocked()) {
            return;
        }
        
        if (!isAttacking) {
            GetRigid().MovePosition(GetRigid().position + movement * Time.fixedDeltaTime * GetWalkSpeed());
        }
        isAttacking = IsAttackAnimation();

        // walk on ice
        //rigid.AddForce(movement * walkSpeed);
    }

    private void DoSampleDialog() {
        dialog = new Dialog();
        dialog.speakerFace = face;
        dialog.sentences.Add("Hello dear <color=red>friend</color>! How are you?");
        dialog.sentences.Add("This is a <color=blue>color</color> test.");
        dialog.sentences.Add("<color=blue>Enjoy!</color>");
        DialogManager.instance.StartDialog(dialog);

    }
}
