using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using System.Threading.Tasks;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityRigidbody2D
{
    [TaskCategory("Unity/Rigidbody2D")]
    [TaskDescription("Checks the live Number")]


    public class CheckLives : Conditional
    {
        public SharedGameObject targetGameObject;
        private int lifes;

        public override TaskStatus OnUpdate()
        {

            if (targetGameObject.Value == null)
            {
                Debug.LogError("Target GameObject is null");
                return TaskStatus.Failure;
            }


            var script = targetGameObject.Value.GetComponent<Vampire>();
            if (script == null)
            {
                Debug.LogError("EntityLiving");
                return TaskStatus.Failure;
            }

            //lifes = script.lifes;
            

            if (lifes <= 2)
            {
                return TaskStatus.Success;
            }
            else
            {
                return TaskStatus.Failure;
            }
        }
    }
}