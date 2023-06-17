using System;
using UnityEngine;
using UnityEngine.UIElements;

public abstract class EntityLiving : Entity
{
    [SerializeField] private short lifes = 3;
    [SerializeField] private AnimationClip clipHurtLeft, clipHurtRight, clipHurtUp, clipHurtDown;
    [SerializeField] private AnimationClip clipDieLeft, clipDieRight, clipDieUp, clipDieDown;
    [SerializeField] private float walkSpeed = 10f;
    [SerializeField] private LayerMask alphaMask;
    
    private protected Vector2 movement;
    private Animator animator;
    private Rigidbody2D rigid;
    private bool lockedByAnimation;
    private bool prevHurtAnim, prevDyingAnim;
    private float minBacklash = 0.0001f;
    private Vector2 prevPos;
    private BoxCollider2D col;
    
    /*public virtual void HandleAlphaCollision() {
        
        if (GetCollider().IsTouchingLayers(alphaMask)) {
            rigid.position = prevPos;
        }

        prevPos = GetRigid().position + Vector2.zero;
    }*/
    
    public virtual void HandleAlphaCollision() {
        /*Vector2 direction = (GetRigid().position - prevPos).normalized;
        float distance = Vector2.Distance(GetRigid().position, prevPos);

        RaycastHit2D hit = Physics2D.Raycast(prevPos, direction, distance, alphaMask);
        if (hit.collider != null) {
            // If a collision would occur, stop the movement
            GetRigid().position = hit.point;
        }

        prevPos = GetRigid().position;*/
    }

    private protected LayerMask GetAlphaMask() {
        return alphaMask;
    }

    public void DoMovement(bool useDir) {
        Vector2 orientation = useDir ? GetDirection() : movement;
        Vector2 intendedPosition = GetRigid().position + orientation * Time.fixedDeltaTime * GetWalkSpeed();
        Vector2 direction = orientation.normalized;
        float distance = orientation.magnitude * Time.fixedDeltaTime * GetWalkSpeed();
        RaycastHit2D hit = Physics2D.Raycast(GetRigid().position, direction, distance, GetAlphaMask());
            
        if (hit.collider != null) {
            Vector2 adjustedMovement = GetRigid().position - hit.point;
            GetRigid().MovePosition(GetRigid().position + adjustedMovement);
        }else {
            GetRigid().MovePosition(intendedPosition);
        }

        SetPrevPos(GetRigid().position);
    }

    private protected Vector2 GetPrevPos() {
        return prevPos;
    }

    private protected void SetPrevPos(Vector2 next) {
        prevPos = next;
    }
    
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
    public virtual void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        lockedByAnimation = false;
        prevHurtAnim = prevDyingAnim = false;
        prevPos = rigid.position + Vector2.zero;
        col = GetComponent<BoxCollider2D>();
    }

    public BoxCollider2D GetCollider() {
        if (col == null) {
            col = GetComponent<BoxCollider2D>();
        }

        return col;
    }
    
    // Update is called once per frame
    void Update()
    {
        
    }

    private protected void FixedUpdate() {
        if (lockedByAnimation) {
            bool currHurtAnim = IsClipPlaying_Hurt();
            if(currHurtAnim && gameObject.name != "Lich") {
                for (int i = 0; i < transform.childCount; i++) {
                    var go = transform.GetChild(i).gameObject;
                    if (go.name == "Collider" || go.name == "ColliderLeft" || go.name == "ColliderRight" || go.name == "ColliderUp" || go.name == "ColliderDown") {
                        go.SetActive(false);
                    }
                }
            }
            if (prevHurtAnim && !currHurtAnim) {
                lockedByAnimation = false;
                movement = Vector2.zero;
            }
            
            Rigidbody2D rig = GetRigid();
            if (currHurtAnim) {
                float fl = RemainingPercentageTime(clipHurtDown);
                rig.MovePosition(rig.position - direction * Time.fixedDeltaTime * walkSpeed * 0.1f *
                    (fl > minBacklash ? fl : minBacklash));
            }
            prevHurtAnim = currHurtAnim;

            bool currDyingAnim = IsClipPlaying_Dying();
            float remainingDyingPercentage = RemainingPercentageTime(clipDieDown);
            if (currDyingAnim) {
                rig.MovePosition(rig.position - direction * Time.fixedDeltaTime * walkSpeed * 0.1f * 
                    (remainingDyingPercentage > minBacklash ? remainingDyingPercentage : minBacklash));
            }
            prevDyingAnim = currDyingAnim;
        }
        HandleAlphaCollision();
    }

    public void DestroySelf() {
        Destroy(gameObject);
    }
    public Animator GetAnimator() {
        if (animator == null) animator = GetComponent<Animator>();
        return animator;
    }    
    private protected Rigidbody2D GetRigid() {
        if (rigid == null) rigid = GetComponent<Rigidbody2D>();
        return rigid;
    }

    private protected float GetWalkSpeed() {
        return walkSpeed;
    }

    protected virtual void OnDie() {
        GetAnimator().SetTrigger("isDying");
        Invoke(nameof(DestroySelf), clipDieDown.length);
    }

    public virtual void SubLife() {
        lifes--;
        if (lifes > 0) {
            GetAnimator().SetTrigger("isHurt");

        }
        else {
            OnDie();
            
        }

        lockedByAnimation = true;
    }
    
    public void SubLifeThen(Action method) {
        lifes--;
        if (lifes > 0) {
            GetAnimator().SetTrigger("isHurt");

        }
        else {
            method.Invoke();
        }

        lockedByAnimation = true;
    }

    public bool IsHurtLocked() {
        return lockedByAnimation;
    }

    public bool IsAlive() {
        return lifes > 0;
    }

    private protected void UpdateMovementAnimation() {
        if (movement.x != 0 || movement.y != 0) {
            GetAnimator().SetFloat("X", movement.x);
            GetAnimator().SetFloat("Y", movement.y);
            GetAnimator().SetBool("isWalking", true);
        } else {
            GetAnimator().SetBool("isWalking", false);
        }
    }

    public void DoHurt(Vector2 dir) {
        Vector2 knockback = dir.normalized * -1;
        lockedByAnimation = true;
        direction = knockback;
        movement = direction;
        GetAnimator().SetFloat("X", knockback.x);
        GetAnimator().SetFloat("Y", knockback.y);
        //GetAnimator().SetBool("isWalking", true);
        SubLife();
    }
}
