using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour {
    public AnimationClip clipAttackLeft, clipAttackRight, clipAttackUp, clipAttackDown;
    
    [SerializeField] private float walkSpeed = 10f;

    private Vector2 movement;
    private Animator animator;
    private Rigidbody2D rigid;

    private bool isAttacking;
    // Start is called before the first frame update
    void Start() {
        rigid = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        isAttacking = false;
    }
    
    private bool IsClipPlaying(AnimationClip clip) {
        AnimatorClipInfo[] clipInfo = animator.GetCurrentAnimatorClipInfo(0);
        return clipInfo.Length > 0 && clipInfo[0].clip.name == clip.name;
    }

    private bool IsAttackAnimation() {
        AnimatorClipInfo[] clipInfo = animator.GetCurrentAnimatorClipInfo(0);
        if (clipInfo.Length <= 0) {
            return false;
        }
        if (clipInfo[0].clip.name == clipAttackLeft.name) {
            return true;
        }
        if (clipInfo[0].clip.name == clipAttackRight.name) {
            return true;
        }
        if (clipInfo[0].clip.name == clipAttackUp.name) {
            return true;
        }
        if (clipInfo[0].clip.name == clipAttackDown.name) {
            return true;
        }

        return false;
    }

    private void OnMovement(InputValue iv) {
        movement = iv.Get<Vector2>();
        if (movement.x != 0 || movement.y != 0) {
            animator.SetFloat("X", movement.x);
            animator.SetFloat("Y", movement.y);
            animator.SetBool("isWalking", true);
        } else {
            animator.SetBool("isWalking", false);
        }
    }

    private void Update() {
        if (Input.GetKeyDown(KeyCode.Z)) {
            animator.SetTrigger("isMelee");
            isAttacking = true;
        }
    }

    // Update is called once per frame
    void FixedUpdate() {
        if (!isAttacking) {
            rigid.MovePosition(rigid.position + movement * Time.fixedDeltaTime * walkSpeed);
        }
        isAttacking = IsAttackAnimation();

        // walk on ice
        //rigid.AddForce(movement * walkSpeed);
    }
}
