using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace com.lineact.lit.FSM
{
    [CreateAssetMenu(menuName = "LIT/FSM/Transitions/ReachedWaypointNavMeshTransition")]
    public class ReachedWaypointNavMeshTransition : Transition
    {
        public override bool Decide(BaseStateMachine stateMachine)
        {
            // tell ig agent has reached the goal or has no path.
            var navMeshAgent = stateMachine.GetComponent<NavMeshAgent>();
            if (!navMeshAgent.pathPending)
            {
                if (navMeshAgent.remainingDistance <= navMeshAgent.stoppingDistance)
                {
                    if (!navMeshAgent.hasPath || navMeshAgent.velocity.sqrMagnitude == 0f)
                    {
                        return true;
                    }
                }
            }

            return false;
        }
    }
}