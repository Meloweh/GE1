using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityAnimator
{
    [TaskCategory("Unity/Animator")]
    [TaskDescription("Plays an animator state. Returns Success.")]
    public class Patrol : Action
    {
        [Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
        public SharedGameObject targetGameObject;
        [Tooltip("The name of the state")]
        public SharedString stateName;
        [Tooltip("The layer where the state is")]
        public int layer = -1;
        [Tooltip("The normalized time at which the state will play")]
        public float normalizedTime = float.NegativeInfinity;

        public Transform[] points;
        private int destPoint = 0;
        private UnityEngine.AI.NavMeshAgent agent;

        private Animator animator;
        private GameObject prevGameObject;

        public override void OnStart()
        {
            var currentGameObject = GetDefaultGameObject(targetGameObject.Value);
            if (currentGameObject != prevGameObject) {
                animator = currentGameObject.GetComponent<Animator>();
                prevGameObject = currentGameObject;

                agent = GetComponent<UnityEngine.AI.NavMeshAgent>();

                // Disabling auto-braking allows for continuous movement
                // between points (ie, the agent doesn't slow down as it
                // approaches a destination point).
                agent.autoBraking = false;

                GotoNextPoint();
            }
        }

        public override TaskStatus OnUpdate()
        {
            if (animator == null) {
                Debug.LogWarning("Animator is null");
                return TaskStatus.Failure;
            }

            // Choose the next destination point when the agent gets
            // close to the current one.
            if (!agent.pathPending && agent.remainingDistance < 0.5f)
                GotoNextPoint();
            animator.Play(stateName.Value, layer, normalizedTime);

            return TaskStatus.Success;
        }

        void GotoNextPoint()
        {
            // Returns if no points have been set up
            if (points.Length == 0)
                return;

            // Set the agent to go to the currently selected destination.
            agent.destination = points[destPoint].position;

            // Choose the next point in the array as the destination,
            // cycling to the start if necessary.
            destPoint = (destPoint + 1) % points.Length;
        }

        public override void OnReset()
        {
            targetGameObject = null;
            stateName = "";
            layer = -1;
            normalizedTime = float.NegativeInfinity;
        }
    }
}
