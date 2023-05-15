using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityHostile : EntityLiving
{
    [SerializeField] private AnimationClip clipAttackLeft, clipAttackRight, clipAttackUp, clipAttackDown;
    [SerializeField] private GameObject meleeColliderLeft, meleeColliderRight, meleeColliderUp, meleeColliderDown;
    
    private bool IsAttackAnimation() {
        AnimatorClipInfo[] clipInfo = GetAnimator().GetCurrentAnimatorClipInfo(0);
        if (clipInfo.Length <= 0) {
            return false;
        }
        if (clipInfo[0].clip.name == clipAttackLeft.name) {
            meleeColliderLeft.SetActive(true);
            return true;
        }
        if (clipInfo[0].clip.name == clipAttackRight.name) {
            meleeColliderRight.SetActive(true);
            return true;
        }
        if (clipInfo[0].clip.name == clipAttackUp.name) {
            meleeColliderUp.SetActive(true);
            return true;
        }
        if (clipInfo[0].clip.name == clipAttackDown.name) {
            meleeColliderDown.SetActive(true);
            return true;
        }
        
        meleeColliderLeft.SetActive(false);
        meleeColliderRight.SetActive(false);
        meleeColliderUp.SetActive(false);
        meleeColliderDown.SetActive(false);
        
        return false;
    }

    private protected void SetMelee() {
        GetAnimator().SetTrigger("isMelee");
    }
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
}
