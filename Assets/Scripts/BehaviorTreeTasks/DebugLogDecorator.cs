using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks
{
    [TaskDescription("Evaluates the specified conditional task. If the conditional task returns success then the child task is run and the child status is returned. If the conditional task does not " +
                     "return success then the child task is not run and a failure status is immediately returned.")]
    [TaskIcon("{SkinColor}ConditionalEvaluatorIcon.png")]
    public class DebugLogDecorator : Decorator
    {
        // The index of the child that is currently running or is about to run.
        private int currentChildIndex = 0;
        // The task status of the last child ran.
        private TaskStatus executionStatus = TaskStatus.Inactive;

        public override int CurrentChildIndex()
        {
            Debug.Log("Debug Log Decorator: CurrentChildIndex");

            return currentChildIndex;
        }

        public override bool CanExecute()
        {
            Debug.Log("Debug Log Decorator: CanExecute");

            // We can continue to execuate as long as we have children that haven't been executed and no child has returned failure.
            return currentChildIndex < children.Count && executionStatus != TaskStatus.Failure;
        }

        public override void OnChildExecuted(TaskStatus childStatus)
        {
            Debug.Log("Debug Log Decorator: OnChildExecuted");
            // Increase the child index and update the execution status after a child has finished running.
            currentChildIndex++;
            executionStatus = childStatus;
        }

        public override void OnConditionalAbort(int childIndex)
        {
            Debug.Log("Debug Log Decorator: OnConditionalAbort");

            // Set the current child index to the index that caused the abort
            currentChildIndex = childIndex;
            executionStatus = TaskStatus.Inactive;
        }

        public override void OnEnd()
        {
            Debug.Log("Debug Log Decorator: OnEnd");

            // All of the children have run. Reset the variables back to their starting values.
            executionStatus = TaskStatus.Inactive;
            currentChildIndex = 0;
        }

        public override TaskStatus OnUpdate() {
            Debug.Log("Debug Log Decorator: OnUpdate");
            return base.OnUpdate();
        }

        public override void OnFixedUpdate() {
            Debug.Log("Debug Log Decorator: OnFixedUpdate");

            base.OnFixedUpdate();
        }
    }
}