using com.lineact.lit.FSM;
using System.Threading;
using UnityEngine;

namespace com.lineact.lit.FSM
{
    [CreateAssetMenu(menuName = "LIT/FSM/Activity/PatrolActivity")]
    public class PatrolActivity : Activity
    {
        public float speed = 1; // how fast we should move around while patrolling. The same value for every agent

        public override void Enter(BaseStateMachine stateMachine)
        {
            
        }

        public override void Execute(BaseStateMachine stateMachine)
        {
            // as the stateMachine cache the component this call to GetComponent is optimized
            var PatrolPoints = stateMachine.GetComponent<PatrolPointsConfig>();
            var targetDirection = PatrolPoints.GetTargetPointDirection();
            // basic control turn mir 100 to the final direction
            var t = PatrolPoints.TargetPoint;
            t.y = stateMachine.transform.position.y;
            stateMachine.transform.LookAt(t);
            stateMachine.transform.Translate(targetDirection * speed * Time.deltaTime, Space.World);
        }

        public override void Exit(BaseStateMachine stateMachine)
        {
            // set the next target point that will be done after the wait
            stateMachine.GetComponent<PatrolPointsConfig>().SetNextTargetPoint();
        }
    }
}