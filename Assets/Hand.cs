using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hand : MonoBehaviour {
    [SerializeField] private AnimationClip hand, hand1;
    private Animator animator;
    private SpriteRenderer renderer;
    private PolygonCollider2D collider;
    private bool IsClipPlaying(AnimationClip clip) {
        AnimatorClipInfo[] clipInfo = animator.GetCurrentAnimatorClipInfo(0);
        return clipInfo.Length > 0 && clipInfo[0].clip.name == clip.name;
    }
    private float GetCurrentAnimatorTime() {
        AnimatorStateInfo animState = animator.GetCurrentAnimatorStateInfo(0);
        float currentTime = animState.normalizedTime % 1;
        return currentTime;
    }
    void Start() {
        animator = GetComponent<Animator>();
        renderer = GetComponent<SpriteRenderer>();
        collider = GetComponent<PolygonCollider2D>();
    }

    public bool IsPlaying() {
        return IsClipPlaying(hand) || IsClipPlaying(hand1);
    }

    void Update() {
        bool active = IsClipPlaying(hand) && GetCurrentAnimatorTime() > 0.58f;
        //Debug.Log("IsClipPlaying(hand): " + IsClipPlaying(hand) + " RemainingPercentageTime(hand): " + (GetCurrentAnimatorTime() > 0.48f) + " = " + active);
        collider.enabled = active;
        renderer.flipX = IsClipPlaying(hand1);
        gameObject.SetActive(IsPlaying());
    }
}
