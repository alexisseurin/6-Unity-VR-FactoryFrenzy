using com.lineact.lit.FSM;
using System.Threading;
using UnityEngine;
using UnityEngine.AI;

namespace com.lineact.lit.FSM
{
    [CreateAssetMenu(menuName = "LIT/FSM/Activity/PatrolNavMeshActivity")]
    public class PatrolNavMeshActivity : Activity
    {
        public override void Enter(BaseStateMachine stateMachine)
        {
            var PatrolPoints = stateMachine.GetComponent<PatrolPointsConfig>();
            var navMeshAgent = stateMachine.GetComponent<NavMeshAgent>();
            navMeshAgent.destination = PatrolPoints.TargetPoint;
        }

        public override void Execute(BaseStateMachine stateMachine)
        {
            
        }

        public override void Exit(BaseStateMachine stateMachine)
        {
            var navMeshAgent = stateMachine.GetComponent<NavMeshAgent>();
            navMeshAgent.ResetPath();

            // set the next target point that will be done after the wait
            stateMachine.GetComponent<PatrolPointsConfig>().SetNextTargetPoint();
        }
    }
}