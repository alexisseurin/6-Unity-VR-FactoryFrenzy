using com.lineact.lit.FSM;
using UnityEngine;

namespace com.lineact.lit.FSM
{
    [CreateAssetMenu(menuName = "LIT/FSM/Activity/WaitActivity")]
    public class WaitActivity : Activity
    {
        public override void Enter(BaseStateMachine stateMachine)
        {
            var t = stateMachine.GetComponent<WaitActivityConfig>();
            t.Reset();
        }

        public override void Execute(BaseStateMachine stateMachine)
        {
        }

        public override void Exit(BaseStateMachine stateMachine)
        {
        }

    }
}