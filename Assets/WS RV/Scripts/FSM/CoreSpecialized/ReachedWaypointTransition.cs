using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace com.lineact.lit.FSM
{
    [CreateAssetMenu(menuName = "LIT/FSM/Transitions/ReachedWaypointTransition")]
    public class ReachedWaypointTransition : Transition
    {
        public override bool Decide(BaseStateMachine stateMachine)
        {
            return stateMachine.GetComponent<PatrolPointsConfig>().HasReachedPoint();
        }
    }
}
