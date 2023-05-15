using System;
using UnityEngine;

public abstract class EntityLiving : MonoBehaviour
{
    [SerializeField] private short lifes = 3;
    [SerializeField] private AnimationClip clipHurtLeft, clipHurtRight, clipHurtUp, clipHurtDown;
    [SerializeField] private AnimationClip clipDieLeft, clipDieRight, clipDieUp, clipDieDown;
    [SerializeField] private float walkSpeed = 10f;
    
    private Vector2 movement;
    private Animator animator;
    private Rigidbody2D rigid;
    private bool lockedByAnimation;
    private bool prevHurtAnim, prevDyingAnim;
    
    private protected bool IsClipPlaying(AnimationClip clip) {
        AnimatorClipInfo[] clipInfo = animator.GetCurrentAnimatorClipInfo(0);
        return clipInfo.Length > 0 && clipInfo[0].clip.name == clip.name;
    }

    private protected bool IsClipPlaying_Dying() {
        AnimatorClipInfo[] clipInfo = GetAnimator().GetCurrentAnimatorClipInfo(0);
        if (clipInfo.Length <= 0) {
            return false;
        }
        if (clipInfo[0].clip.name == clipDieLeft.name) {
            return true;
        }
        if (clipInfo[0].clip.name == clipDieRight.name) {
            return true;
        }
        if (clipInfo[0].clip.name == clipDieUp.name) {
            return true;
        }
        if (clipInfo[0].clip.name == clipDieDown.name) {
            return true;
        }
        return false;
    }
    
    private protected bool IsClipPlaying_Hurt() {
        AnimatorClipInfo[] clipInfo = GetAnimator().GetCurrentAnimatorClipInfo(0);
        if (clipInfo.Length <= 0) {
            return false;
        }
        if (clipInfo[0].clip.name == clipHurtLeft.name) {
            return true;
        }
        if (clipInfo[0].clip.name == clipHurtRight.name) {
            return true;
        }
        if (clipInfo[0].clip.name == clipHurtUp.name) {
            return true;
        }
        if (clipInfo[0].clip.name == clipHurtDown.name) {
            return true;
        }
        return false;
    }

    // Start is called before the first frame update
    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        lockedByAnimation = false;
        prevHurtAnim = prevDyingAnim = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate() {
        if (lockedByAnimation) {
            bool currHurtAnim = IsClipPlaying_Hurt();
            if (prevHurtAnim && !currHurtAnim) {
                lockedByAnimation = false;
            }
            prevHurtAnim = currHurtAnim;

            bool currDyingAnim = IsClipPlaying_Dying();
            if (prevDyingAnim && !currDyingAnim) {
                Destroy(this);
            }
            prevDyingAnim = currDyingAnim;
        }
    }
    
    private protected Vector2 GetMovement() {
        return movement;
    }
    private protected Animator GetAnimator() {
        return animator;
    }    
    private protected Rigidbody2D GetRigid() {
        return rigid;
    }

    public void SubLife() {
        lifes--;
        if (lifes > 0) {
            animator.SetTrigger("isHurt");
        }
        else {
            animator.SetTrigger("isDying");
        }

        lockedByAnimation = true;
    }

    public bool IsHurtLocked() {
        return lockedByAnimation;
    }

    public bool IsAlive() {
        return lifes > 0;
    }
}
