using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityRigidbody2D
{
    [TaskCategory("Unity/Rigidbody2D")]
    [TaskDescription("Sets the velocity of the Rigidbody2D. Returns Success.")]
    public class MeleeIfClose : Action
    {
        [Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
        public SharedGameObject targetGameObject;
        private GameObject prevGameObject;
        private EntityHostile entityHostile;
        public LayerMask playerMask;
        public GameObject handSource;

        private Collider2D target;
        private HandSource scriptHandSource;

        public override void OnStart()
        {
            var currentGameObject = GetDefaultGameObject(targetGameObject.Value);
            if (currentGameObject != prevGameObject) {
                entityHostile = currentGameObject.GetComponent<EntityHostile>();
                prevGameObject = currentGameObject;
            }
            if (entityHostile != null) {
                entityHostile.SetMeleeDir();
                entityHostile.SetMelee();
            }
            target = Physics2D.OverlapCircle(transform.position, 2.5f, playerMask);
            if (target) {
                handSource.SetActive(true);
                scriptHandSource = handSource.GetComponent<HandSource>();
                scriptHandSource.Replay();
            }
        }

        public override TaskStatus OnUpdate()
        {
            if (entityHostile == null) {
                Debug.LogWarning("entityHostile is null");
                return TaskStatus.Failure;
            }
            if (target == null) {
                Debug.Log("Target to far");
                return TaskStatus.Failure;
            }
            if (scriptHandSource.IsPlaying()) {
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