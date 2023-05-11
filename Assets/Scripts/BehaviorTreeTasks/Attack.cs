using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityRigidbody2D
{
    [TaskCategory("Unity/Rigidbody2D")]
    [TaskDescription("Sets the velocity of the Rigidbody2D. Returns Success.")]
    public class Attack : Action
    {
        [Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
        public SharedGameObject targetGameObject;
        [Tooltip("Melee blend")]
        public string isMelee;
        [Tooltip("Animator")]
        public Animator animator;
        [Tooltip("Attack clips")]
        public AnimationClip clipAttackLeft, clipAttackRight, clipAttackUp, clipAttackDown;

        private Rigidbody2D rigidbody2D;
        private GameObject prevGameObject;
        private bool wasAttacking;
        
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

        public override void OnStart()
        {
            var currentGameObject = GetDefaultGameObject(targetGameObject.Value);
            if (currentGameObject != prevGameObject) {
                rigidbody2D = currentGameObject.GetComponent<Rigidbody2D>();
                prevGameObject = currentGameObject;
            }

            wasAttacking = false;
            animator.SetTrigger(isMelee);
        }

        public override TaskStatus OnUpdate()
        {
            if (rigidbody2D == null) {
                Debug.LogWarning("Rigidbody2D is null");
                return TaskStatus.Failure;
            }

            bool isAttackAnimation = IsAttackAnimation();
            wasAttacking = wasAttacking || isAttackAnimation;

            if (isAttackAnimation || !wasAttacking) {
                return TaskStatus.Running;
            }

            return TaskStatus.Success;
        }

        public override void OnReset()
        {
            targetGameObject = null;
        }
    }
}