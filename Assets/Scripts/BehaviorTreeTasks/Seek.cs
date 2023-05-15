using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityRigidbody2D
{
    [TaskCategory("Unity/Rigidbody2D")]
    [TaskDescription("Sets the velocity of the Rigidbody2D. Returns Success.")]
    public class Seek : Action
    {
        [Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
        public SharedGameObject targetGameObject;
        [Tooltip("Movement speed")]
        public float speed;
        [Tooltip("Radius in which goal is reached")]
        public float radius;
        [Tooltip("Radius in which goal is lost")]
        public float outerRadius;
        [Tooltip("Walk animation")]
        public bool shouldAnimate;
        [Tooltip("Animator")]
        public Animator animator;
        [Tooltip("X blend")]
        public string x;
        [Tooltip("Y blend")]
        public string y;
        [Tooltip("Walk blend")]
        public string isWalking;
        [Tooltip("Collider mask")]
        public LayerMask mask;

        private Rigidbody2D rigidbody2D;
        private Rigidbody2D goalRigid2D;
        private GameObject prevGameObject;
        private GameObject goalGameObject;

        public override void OnStart()
        {
            var currentGameObject = GetDefaultGameObject(targetGameObject.Value);
            if (currentGameObject != prevGameObject) {
                rigidbody2D = currentGameObject.GetComponent<Rigidbody2D>();
                prevGameObject = currentGameObject;
            }
            
            var target = Physics2D.OverlapCircle(transform.position, outerRadius, mask);
            if (target != null) {
                goalGameObject = target.gameObject;
            }
            
            if (goalGameObject != null) { //TODO: ensure rigid2D
                goalRigid2D = goalGameObject.GetComponent<Rigidbody2D>();
            }
        }

        private void UpdateAnimation(Vector2 movement) {
            if (!shouldAnimate) return;
            if (movement.x != 0 || movement.y != 0) {
                animator.SetFloat(x, movement.x);
                animator.SetFloat(y, movement.y);
                animator.SetBool(isWalking, true);
            } else {
                animator.SetBool(isWalking, false);
            }
        }

        public override TaskStatus OnUpdate()
        {
            if (rigidbody2D == null) {
                Debug.LogWarning("Rigidbody2D is null");
                return TaskStatus.Failure;
            }
            
            if (goalGameObject == null) {
                Debug.LogWarning("goalGameObject is null");
                return TaskStatus.Failure;
            }

            if (goalGameObject == null) {
                return TaskStatus.Failure;
            }
            
            if (goalRigid2D == null) {
                Debug.LogWarning("goalRigid2D is null");
                return TaskStatus.Failure;
            }

            Vector2 position1 = rigidbody2D.position;
            Vector2 position2 = goalRigid2D.position;
            Vector2 difference = position2 - position1;
            Vector2 direction = difference.normalized;

            rigidbody2D.MovePosition(rigidbody2D.position + direction * Time.fixedDeltaTime * speed);

            if (Vector2.Distance(rigidbody2D.position, goalGameObject.transform.position) < radius) {
                UpdateAnimation(Vector2.zero);
                return TaskStatus.Success;
            }
            UpdateAnimation(direction);
            return TaskStatus.Running;
            
        }

        public override void OnReset()
        {
            targetGameObject = null;
        }
    }
}