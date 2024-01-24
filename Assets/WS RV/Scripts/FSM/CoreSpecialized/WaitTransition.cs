using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace com.lineact.lit.FSM
{
    [CreateAssetMenu(menuName = "LIT/FSM/Transitions/WaitTransition")]
    public class WaitTransition : Transition
    {
        [SerializeField] private float waitTime = 3f; // shared with all agent using this Transition
        //float timer = 0;// design error since the timer value is shared between all Agent using it

        public override bool Decide(BaseStateMachine stateMachine)
        {
            var t = stateMachine.GetComponent<WaitActivityConfig>();
            t.Timer += Time.deltaTime;
            if (t.Timer >= waitTime)
            {
                t.Reset();
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}