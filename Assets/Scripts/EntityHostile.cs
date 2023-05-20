using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityHostile : EntityLiving
{
    [SerializeField] private AnimationClip clipAttackLeft, clipAttackRight, clipAttackUp, clipAttackDown;
    [SerializeField] private GameObject meleeColliderLeft, meleeColliderRight, meleeColliderUp, meleeColliderDown;

    private protected bool isAttacking;
    public bool IsAttackAnimation() {
        AnimatorClipInfo[] clipInfo = GetAnimator().GetCurrentAnimatorClipInfo(0);
        if (clipInfo.Length <= 0) {
            return false;
        }
        if (clipInfo[0].clip.name == clipAttackLeft.name) {
            Debug.LogWarning("left");
            meleeColliderLeft.SetActive(true);
            return true;
        }
        if (clipInfo[0].clip.name == clipAttackRight.name) {
            Debug.LogWarning("right");
            meleeColliderRight.SetActive(true);
            return true;
        }
        if (clipInfo[0].clip.name == clipAttackUp.name) {
            Debug.LogWarning("up");
            meleeColliderUp.SetActive(true);
            return true;
        }
        if (clipInfo[0].clip.name == clipAttackDown.name) {
            Debug.LogWarning("down");
            meleeColliderDown.SetActive(true);
            return true;
        }
        
        meleeColliderLeft.SetActive(false);
        meleeColliderRight.SetActive(false);
        meleeColliderUp.SetActive(false);
        meleeColliderDown.SetActive(false);
        
        return false;
    }

    public void SetMelee() {
        GetAnimator().SetTrigger("isMelee");
    }
    
    // Start is called before the first frame update
    void Start() {
        isAttacking = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    private protected new void FixedUpdate() {
        base.FixedUpdate();
    }
    
    private float Sigmoid(float val) {
        return val > 0.5f ? 1 : val < -0.5f ? -1 : 0;
    }

    private Vector2 Sigmoid(Vector2 val) {
        return new Vector2(Sigmoid(val.x), Sigmoid(val.y));
    }

    public void SetMeleeDir() {
        var vec = Sigmoid(GetDirection().normalized);
        GetAnimator().SetFloat("X", vec.x);
        GetAnimator().SetFloat("Y", vec.y);
    }
}
