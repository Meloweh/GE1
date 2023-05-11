using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityRigidbody2D
{
    [TaskCategory("Unity/Rigidbody2D")]
    [TaskDescription("Sets the velocity of the Rigidbody2D. Returns Success.")]
    public class Nearby : Action
    {
        [Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
        public SharedGameObject targetGameObject;
        [Tooltip("Goal GameObject")]
        public GameObject goalGameObject;
        [Tooltip("Radius")]
        public float radius;

        private Rigidbody2D rigidbody2D;
        private Rigidbody2D goalRigid2D;
        private GameObject prevGameObject;

        public override void OnStart()
        {
            var currentGameObject = GetDefaultGameObject(targetGameObject.Value);
            if (currentGameObject != prevGameObject) {
                rigidbody2D = currentGameObject.GetComponent<Rigidbody2D>();
                prevGameObject = currentGameObject;
            }
            if (goalGameObject != null) { //TODO: ensure rigid2D
                Debug.Log(goalGameObject.name);
                goalRigid2D = goalGameObject.GetComponent<Rigidbody2D>();
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
            
            if (goalRigid2D == null) {
                Debug.LogWarning("goalRigid2D is null");
                return TaskStatus.Failure;
            }
            
            return Vector2.Distance(rigidbody2D.position, goalGameObject.transform.position) < radius ? TaskStatus.Success : TaskStatus.Failure;
        }

        public override void OnReset()
        {
            targetGameObject = null;
        }
    }
}