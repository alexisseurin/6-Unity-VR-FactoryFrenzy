using UnityEngine;

namespace com.lineact.lit.FSM
{
    public class BaseState : ScriptableObject
    {
        public virtual void Enter(BaseStateMachine machine) { }
        public virtual void Execute(BaseStateMachine machine) { }
        public virtual void Exit(BaseStateMachine machine) { }
    }
}