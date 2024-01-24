using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace com.lineact.lit.FSM
{
    [CreateAssetMenu(menuName = "LIT/FSM/Activity/CallRobotActivity")]
    public class CallRobotActivity : Activity
    {
        public float Speed;
        public override void Enter(BaseStateMachine stateMachine)
        {
            var conf = stateMachine.GetComponent<CallRobotConfig>();
            conf.ResetCall();// if put here the robot can be called while moving
        }

        public override void Execute(BaseStateMachine stateMachine)
        {
            // as the stateMachine cache the component this call to GetComponent is optimized
            var conf = stateMachine.GetComponent<CallRobotConfig>();
            var targetDirection = conf.GetTargetPointDirection();
            // basic control turn mir 100 to the final direction
            var t = conf.TargetPoint;
            t.y = stateMachine.transform.position.y;
            stateMachine.transform.LookAt(t);
            stateMachine.transform.Translate(targetDirection * Speed * Time.deltaTime, Space.World);
        }

        public override void Exit(BaseStateMachine stateMachine)
        {
            
        }
    }
}