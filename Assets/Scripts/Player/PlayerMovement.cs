using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : EntityHostile {
    
    // Start is called before the first frame update
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
    }

    // Update is called once per frame
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
}
