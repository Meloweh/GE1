using System;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityRigidbody2D
{
    [TaskCategory("Unity/Rigidbody2D")]
    [TaskDescription("Sets the velocity of the Rigidbody2D. Returns Success.")]
    public class BFSSeek : Action
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
        [Tooltip("Should the wait be randomized?")]
        public SharedBool randomWait = false;
        [Tooltip("The minimum wait time if random wait is enabled")]
        public SharedFloat randomWaitMin = 1;
        [Tooltip("The maximum wait time if random wait is enabled")]
        public SharedFloat randomWaitMax = 1;

        // The time to wait
        private float waitDuration;
        // The time that the task started to wait.
        private float startTime;
        // Remember the time that the task is paused so the time paused doesn't contribute to the wait time.
        private float pauseTime;

        private Rigidbody2D rigidbody2D;
        private Rigidbody2D goalRigid2D;
        private GameObject prevGameObject;
        private GameObject goalGameObject;
        private EntityLiving entityLiving;
        private BFSAlgorithm bfs;
        private Vector2 next;
        private bool isPathingStarting;
        private Vector2 prev = Vector2.zero;
        private SpriteRenderer spriteRenderer;
        private float timerDuration = 1f;
        private float elapsedTime = 0f;

        public override void OnStart()
        {
            var currentGameObject = GetDefaultGameObject(targetGameObject.Value);
            if (currentGameObject != prevGameObject) {
                rigidbody2D = currentGameObject.GetComponent<Rigidbody2D>();
                prevGameObject = currentGameObject;
                entityLiving = GetComponent<EntityLiving>();
                prev = Center(Vector2Int.RoundToInt(rigidbody2D.position));
                spriteRenderer = GetComponent<SpriteRenderer>();
            }
            
            startTime = Time.time;
            if (randomWait.Value) {
                waitDuration = UnityEngine.Random.Range(randomWaitMin.Value, randomWaitMax.Value);
            }
            
            var target = Physics2D.OverlapCircle(transform.position, outerRadius, mask);
            if (target != null) {
                goalGameObject = target.gameObject;
                //Debug.LogWarning("Player object: " + target.name + " " + target.transform.position.ToString());
            }
            
            if (goalGameObject != null) { //TODO: ensure rigid2D
                goalRigid2D = goalGameObject.GetComponent<Rigidbody2D>();
            }

            bfs = new BFSAlgorithm();
            isPathingStarting = true;
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

        private Vector2 GetDirectionTo(Vector2 target) {
            Vector2 position1 = rigidbody2D.position;
            Vector2 position2 = target;
            Vector2 difference = position2 - position1;
            Vector2 direction = difference.normalized;
            return direction;
        }

        private Vector2 Center(Vector2Int v) {
            return new Vector2(v.x + 0.5f, v.y + 0.5f);
        }
        
        public override TaskStatus OnUpdate()
        {
            // The task is done waiting if the time waitDuration has elapsed since the task was started.
            if (startTime + waitDuration < Time.time) {
                UpdateAnimation(Vector2.zero);
                return TaskStatus.Success;
            }
            
            if (rigidbody2D == null) {
                Debug.LogWarning("Rigidbody2D is null");
                return TaskStatus.Failure;
            }

            if (prev == Vector2.zero) {
                prev = rigidbody2D.position;
            }
            
            if (goalGameObject == null) {
                Debug.LogWarning("goalGameObject is null");
                return TaskStatus.Failure;
            }

            if (goalRigid2D == null) {
                Debug.LogWarning("goalRigid2D is null");
                return TaskStatus.Failure;
            }

            if (entityLiving == null) {
                Debug.LogWarning("entityLiving is null");
                return TaskStatus.Failure;
            }

            if (spriteRenderer == null) {
                Debug.LogWarning("spriteRenderer is null");
                return TaskStatus.Failure;
            }
            
            Vector2 currentPos = rigidbody2D.position;

            float distToTarget = Vector2.Distance(rigidbody2D.position, goalRigid2D.position);
            if (distToTarget > outerRadius) {
                UpdateAnimation(Vector2.zero);
                return TaskStatus.Failure;
            }
            if (distToTarget <= 1.5f) {
                UpdateAnimation(Vector2.zero);
                return TaskStatus.Success;
            }
            
            Vector2Int currentCell = Vector2Int.RoundToInt(currentPos);
            Vector2Int targetCell = Vector2Int.RoundToInt(goalRigid2D.position);

            if (isPathingStarting) {
                next = prev;
                isPathingStarting = false;
            }

            if (!bfs.CanWalk(currentCell)) {
                elapsedTime += Time.deltaTime;
                if (elapsedTime >= timerDuration) {
                    Vector2Int dir = bfs.ShuffleDirections()[0];
                    next = Center(currentCell + dir);
                    elapsedTime = 0f;
                }
            }
            
            bool isAtCenter = Vector2.Distance(currentPos, next) < 0.1f;

            if (isAtCenter && bfs.CanWalk(currentCell)) {
                if (!bfs.HasNext()) {
                    bfs.ShuffleDirections();
                    bfs.Calculate(currentCell, targetCell);
                    //Debug.LogWarning("no path");
                }
                else {
                    //Debug.LogWarning("path");
                }

                prev = next;
                next = Center(bfs.Poll());

            }

            Vector2 direction = GetDirectionTo(next);
            entityLiving.SetDirection(direction);
            rigidbody2D.MovePosition(currentPos + direction * Time.fixedDeltaTime * speed);
            UpdateAnimation(direction);
            return TaskStatus.Running;
        }

        public override void OnReset() {
            targetGameObject = null;
        }
    }
}