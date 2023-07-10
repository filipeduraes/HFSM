namespace FSMSystem.Core.BaseTasks
{
    public class StateFinishedCondition : ConditionTask
    {
        private bool conditionFinished;
        
        public override void Initialize(State fromState)
        {
            conditionFinished = false;
            fromState.OnUpdateLoop += FinishCondition;
        }

        public override void Dispose(State fromState)
        {
            fromState.OnUpdateLoop -= FinishCondition;
        }

        protected override bool Check(State state)
        {
            return conditionFinished;
        }
        
        private void FinishCondition()
        {
            conditionFinished = true;
        }
    }
}