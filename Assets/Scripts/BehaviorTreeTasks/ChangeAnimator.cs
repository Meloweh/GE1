using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using System.Threading.Tasks;
using System;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityRigidbody2D
{
    [TaskCategory("Unity/Rigidbody2D")]
    [TaskDescription("Changes the Animator.")]
    public class ChangeAnimator : Action
    {
        public SharedGameObject targetGameObject;
        public RuntimeAnimatorController newController;

        public override TaskStatus OnUpdate()
        {
            var animator = targetGameObject.Value.GetComponent<Animator>();

            if (animator == null)
            {
                return TaskStatus.Failure;
            }

            animator.runtimeAnimatorController = newController;
            return TaskStatus.Success;
        }
    }
}