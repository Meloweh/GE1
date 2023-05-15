using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityRigidbody2D
{
    [TaskCategory("Unity/Rigidbody2D")]
    [TaskDescription("Sets the velocity of the Rigidbody2D. Returns Success.")]
    public class HurtLock : Action
    {
        [Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
        public SharedGameObject targetGameObject;
        private GameObject prevGameObject;
        private EntityLiving entityLiving;

        public override void OnStart()
        {
            var currentGameObject = GetDefaultGameObject(targetGameObject.Value);
            if (currentGameObject != prevGameObject) {
                prevGameObject = currentGameObject;
                entityLiving = currentGameObject.GetComponent<EntityLiving>();
            }
        }

        public override TaskStatus OnUpdate() {
            if (entityLiving == null) {
                Debug.LogWarning("entityLiving is null");
                return TaskStatus.Failure;
            }

            if (entityLiving.IsHurtLocked()) {
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