using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hand : MonoBehaviour {
    [SerializeField] private AnimationClip hand1;
    private Animator animator;
    private SpriteRenderer renderer;
    private protected bool IsClipPlaying(AnimationClip clip) {
        AnimatorClipInfo[] clipInfo = animator.GetCurrentAnimatorClipInfo(0);
        return clipInfo.Length > 0 && clipInfo[0].clip.name == clip.name;
    }
    // Start is called before the first frame update
    void Start() {
        animator = GetComponent<Animator>();
        renderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        renderer.flipX = IsClipPlaying(hand1);
    }
}
