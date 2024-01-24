using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace com.lineact.lit.FSM
{
    [CreateAssetMenu(menuName = "LIT/FSM/Transitions/CallRobotAskedTransition")]
    public class CallRobotAskedTransition : Transition
    {
        public override bool Decide(BaseStateMachine stateMachine)
        {
            var c = stateMachine.gameObject.GetComponent<CallRobotConfig>();
            if (c == null) { return false; }

            return c.CallRobotAsked;
        }
    }
}