using com.lineact.lit.FSM;
using UnityEngine;

namespace com.lineact.lit.FSM
{
    public abstract class Activity : ScriptableObject
    {
        public abstract void Enter(BaseStateMachine stateMachine);
        public abstract void Execute(BaseStateMachine stateMachine);
        public abstract void Exit(BaseStateMachine stateMachine);
    }
}