using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace com.lineact.lit.FSM
{
    [CreateAssetMenu(menuName = "LIT/FSM/Transitions/CallRobotReachedWaypointTransition")]
    public class CallRobotReachedWaypointTransition : Transition
    {
        public override bool Decide(BaseStateMachine stateMachine)
        {
            return stateMachine.GetComponent<CallRobotConfig>().HasReachedPoint();
        }
    }
}