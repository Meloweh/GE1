using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityRigidbody2D
{
    [TaskCategory("Unity/Rigidbody2D")]
    [TaskDescription("Sets the velocity of the Rigidbody2D. Returns Success.")]
    public class Patrol : Action
    {
        [Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
        public SharedGameObject targetGameObject;
        [Tooltip("The velocity of the Rigidbody2D")]
        public SharedVector2 velocity;

        private Rigidbody2D rigidbody2D;
        private GameObject prevGameObject;

        public Transform[] points;
        private int destPoint = 0;
        private UnityEngine.AI.NavMeshAgent agent;

        public override void OnStart()
        {
            var currentGameObject = GetDefaultGameObject(targetGameObject.Value);
            if (currentGameObject != prevGameObject) {
                rigidbody2D = currentGameObject.GetComponent<Rigidbody2D>();
                prevGameObject = currentGameObject;
            }
            agent = GetComponent<UnityEngine.AI.NavMeshAgent>();

            agent.autoBraking = false;

            GotoNextPoint();
        }

        void GotoNextPoint()
        {
            if (points.Length == 0)
                return;

         
            agent.destination = points[destPoint].position;
            destPoint = (destPoint + 1) % points.Length;
        }

        public override TaskStatus OnUpdate()
        {
            if (rigidbody2D == null) {
                Debug.LogWarning("Rigidbody2D is null");
                return TaskStatus.Failure;
            }

            if (!agent.pathPending && agent.remainingDistance < 0.5f)
                GotoNextPoint();

            return TaskStatus.Success;
        }

        public override void OnReset()
        {
            targetGameObject = null;
            
        }
    }
}
