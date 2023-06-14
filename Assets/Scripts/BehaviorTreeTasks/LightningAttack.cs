using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityRigidbody2D
{
    [TaskCategory("Unity/Rigidbody2D")]
    [TaskDescription("Sets the velocity of the Rigidbody2D. Returns Success.")]
    public class LightningAttack : Action
    {
        [Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
        public SharedGameObject targetGameObject;
        private GameObject prevGameObject;
        private bool wasAttacking;
        private EntityHostile entityHostile;
        public LayerMask playerMask;
        public GameObject lightningSource;

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
        }

        public override TaskStatus OnUpdate()
        {
            if (entityHostile == null) {
                Debug.LogWarning("entityHostile is null");
                return TaskStatus.Failure;
            }
            
            var target = Physics2D.OverlapCircle(transform.position, 20, playerMask);
            if (target == null) return TaskStatus.Failure;
            lightningSource.SetActive(true);
            LightningSpawner scriptLightningSource = lightningSource.GetComponent<LightningSpawner>();
            scriptLightningSource.Replay();
            if (scriptLightningSource.IsPlaying()) return TaskStatus.Running;
            return TaskStatus.Success;
        }

        public override void OnReset()
        {
            targetGameObject = null;
        }
    }
}