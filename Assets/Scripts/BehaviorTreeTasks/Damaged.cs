using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityRigidbody2D
{
    [TaskCategory("Unity/Rigidbody2D")]
    [TaskDescription("Sets the velocity of the Rigidbody2D. Returns Success.")]
    public class Damaged : Action
    {
        [Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
        public SharedGameObject targetGameObject;
        [Tooltip("Collider mask")]
        public LayerMask hurtMask;

        private Rigidbody2D rigidbody2D;
        private GameObject prevGameObject;
        private Collider2D ownCollider;
        private EntityLiving ownLiving;

        public override void OnStart()
        {
            var currentGameObject = GetDefaultGameObject(targetGameObject.Value);
            if (currentGameObject != prevGameObject) {
                rigidbody2D = currentGameObject.GetComponent<Rigidbody2D>();
                prevGameObject = currentGameObject;
                ownCollider = currentGameObject.GetComponent<Collider2D>();
                ownLiving = currentGameObject.GetComponent<EntityLiving>();
            }
        }

        public override TaskStatus OnUpdate()
        {
            if (rigidbody2D == null) {
                Debug.LogWarning("Rigidbody2D is null");
                return TaskStatus.Failure;
            }
            if (ownCollider == null) {
                Debug.LogWarning("Collider2D is null");
                return TaskStatus.Failure;
            }
            if (ownLiving == null) {
                Debug.LogWarning("ownLiving is null");
                return TaskStatus.Failure;
            }
            
            //var target = Physics2D.IsTouchingLayers(ownCollider, hurtMask);
            var target = Physics2D.OverlapCircle(transform.position, 0.1f, hurtMask);

            if (target == null) {
                return TaskStatus.Running;
            }
            Debug.Log("HIT");
            var targetGameComponent = target.gameObject.transform.parent;
            if (targetGameComponent == null) {
                Debug.Log("targetGameComponent is null");
                return TaskStatus.Failure;
            }
            Debug.LogWarning(targetGameComponent.name);
            EntityLiving living = targetGameComponent.GetComponent<EntityLiving>();
            if (living == null) {
                Debug.Log("living is null");
                return TaskStatus.Failure;
            }
            Vector2 direction = living.GetDirection();
            ownLiving.DoHurt(direction);
            return TaskStatus.Success;
        }

        public override void OnReset()
        {
            targetGameObject = null;
        }
    }
}