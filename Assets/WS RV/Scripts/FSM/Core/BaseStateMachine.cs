using com.lineact.lit.FSM;
using System;
using System.Collections.Generic;
using UnityEngine;
namespace com.lineact.lit.FSM
{
    public class BaseStateMachine : MonoBehaviourCachedComponent
    {
        [SerializeField] private BaseState _initialState;
        public BaseState CurrentState;
        private void Awake()
        {
            CurrentState = _initialState;
        }
        private void Start()
        {
            CurrentState.Enter(this);
        }
        private void Update()
        {
            CurrentState.Execute(this);
        }

        /// <summary>
        /// Change the state of the state machine. Call the Exit of the previous state and call the Enter of the new state
        /// </summary>
        /// <param name="newState"></param>
        public void ChangeState(BaseState newState)
        {
            CurrentState.Exit(this);
            CurrentState = newState;
            CurrentState.Enter(this);
        }
    }
}