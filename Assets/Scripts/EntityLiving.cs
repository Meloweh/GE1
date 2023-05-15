using System;
using UnityEngine;
using UnityEngine.UIElements;

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
    private Vector2 direction;
    private bool canDespawn;
    
    private protected bool IsClipPlaying(AnimationClip clip) {
        AnimatorClipInfo[] clipInfo = animator.GetCurrentAnimatorClipInfo(0);
        return clipInfo.Length > 0 && clipInfo[0].clip.name == clip.name;
    }
    
    public float GetCurrentAnimatorTime() {
        AnimatorStateInfo animState = animator.GetCurrentAnimatorStateInfo(0);
        float currentTime = animState.normalizedTime % 1;
        return currentTime;
    }

    private float RemainingPercentageTime(AnimationClip clip) {
        float delta = GetCurrentAnimatorTime();
        float remainingPercentage = 1 - delta / clip.length;
        //Debug.Log(delta + " --- " + limit);
        return remainingPercentage;
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
        canDespawn = false;
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
            float minBacklash = 0.0001f;
            Rigidbody2D rig = GetRigid();
            if (currHurtAnim) {
                float fl = RemainingPercentageTime(clipHurtDown);
                rig.MovePosition(rig.position - direction * Time.fixedDeltaTime * walkSpeed * 0.1f * (fl > minBacklash ? fl : minBacklash));
            }
            prevHurtAnim = currHurtAnim;

            bool currDyingAnim = IsClipPlaying_Dying();
            float remainingDyingPercentage = RemainingPercentageTime(clipDieDown);
            if (prevDyingAnim && remainingDyingPercentage < 0.1f) { //TODO: FIXME: remainingDyingPercentage < 0.1f could possibly skip when game lags
                GetComponent<SpriteRenderer>().enabled = false;
            }
            if (currDyingAnim) {
                
                rig.MovePosition(rig.position - direction * Time.fixedDeltaTime * walkSpeed * 0.1f * 
                    (remainingDyingPercentage > minBacklash ? remainingDyingPercentage : minBacklash));
            }
            prevDyingAnim = currDyingAnim;
        }
    }

    public void SetDirection(Vector2 dir) {
        this.direction = dir;
    }
    private protected Vector2 GetMovement() {
        return movement;
    }
    public Animator GetAnimator() {
        if (animator == null) animator = GetComponent<Animator>();
        return animator;
    }    
    private protected Rigidbody2D GetRigid() {
        if (rigid == null) rigid = GetComponent<Rigidbody2D>();
        return rigid;
    }

    public void SubLife() {
        lifes--;
        Debug.Log("Health: " + lifes);
        if (lifes > 0) {
            GetAnimator().SetTrigger("isHurt");
            Debug.Log("hurt");

        }
        else {
            Debug.Log("dying");
            GetAnimator().SetTrigger("isDying");
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
