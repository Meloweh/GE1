using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks.Unity.UnityRigidbody2D
{
    [TaskCategory("Unity/Rigidbody2D")]
    [TaskDescription("Patrol Task.")]
    public class NearbyPatrol : Action
    {
        [Tooltip("The GameObject that the task operates on. If null the task GameObject is used.")]
        public SharedGameObject targetGameObject;
        [Tooltip("Radius")]
        public float radius;
        [Tooltip("Collider mask")]
        public LayerMask NPC1_Path;

        private Rigidbody2D rigidbody2D;
        private GameObject prevGameObject;
        private GameObject nextGameObject;
        private GameObject[] targets;

       

        public override void OnStart()
        {
           
            var currentGameObject = GetDefaultGameObject(targetGameObject.Value);
            if (currentGameObject != prevGameObject)
            {
                rigidbody2D = currentGameObject.GetComponent<Rigidbody2D>();
                prevGameObject = currentGameObject;
            }

          
        }


        public override TaskStatus OnUpdate()
        {
            if (rigidbody2D == null)
            {
                Debug.LogWarning("Rigidbody2D is null");
                return TaskStatus.Failure;
            }

            var targets = Physics2D.OverlapCircleAll(transform.position, radius, NPC1_Path);

            var gameObjectDistances =  new List<DistanceContainer>();
            foreach (var target in targets)
            {
                float distance = Vector2.Distance(rigidbody2D.position, target.transform.position);
                var distanceContainer = new DistanceContainer();
                distanceContainer.Distance = distance;
                distanceContainer.OwnGameObject = target.gameObject;
                gameObjectDistances.Add(distanceContainer);

            }
            gameObjectDistances.Sort((a, b) => a.Distance.CompareTo(b.Distance));

            GameObjectDistance candidate = null;

            foreach(var target in targets)
            {
                var god = target.GetComponent<GameObjectDistance>();

                if(god.State == PathStates.PREV)
                {
                    god.State = PathStates.NONE;
                }
                else if(god.State == PathStates.CURRENT)
                {
                    god.State = PathStates.PREV;
                }
                else if (god.State == PathStates.NEXT)
                {
                    god.State = PathStates.CURRENT;
                }
                else if(candidate == null)
                {
                    candidate = god;
                    
                }
                else if(candidate != null)
                {
                    System.Random random = new System.Random();
                    bool randomBool = random.Next(2) == 0;

                    if(randomBool)
                    {
                        god.State = PathStates.NEXT;
                        candidate.State = PathStates.PREV;
                    }
                    else
                    {
                        god.State = PathStates.PREV;
                        candidate.State = PathStates.NEXT;
                    }
                }
                
            }

            

            return targets.Length > 0 ? TaskStatus.Success : TaskStatus.Failure;
        }

        public override void OnReset()
        {
            targetGameObject = null;
        }
    }
}