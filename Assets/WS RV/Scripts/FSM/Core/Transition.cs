using com.lineact.lit.FSM;
using UnityEngine;


namespace com.lineact.lit.FSM
{
    /// <summary>
    /// This class checks that the Transition is validated and change the State when it is the case.
    /// </summary>
    public abstract class Transition : ScriptableObject
    {
        public BaseState TrueState;
        public BaseState FalseState;

        public void Execute(BaseStateMachine stateMachine)
        {
            if (Decide(stateMachine) && TrueState is not null )
            {
                stateMachine.ChangeState(TrueState);
            }
            else if (FalseState is not null)
            {
                stateMachine.ChangeState(FalseState);
            }
        }

        /// <summary>
        /// function to implement for Deciding going to another State
        /// </summary>
        /// <param name="stateMachine"></param>
        /// <returns></returns>
        public abstract bool Decide(BaseStateMachine stateMachine);
    }
}