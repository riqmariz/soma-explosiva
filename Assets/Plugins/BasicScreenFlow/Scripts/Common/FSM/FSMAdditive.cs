using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Common.FSM
{
    public class FSMAdditive : FSM
    {
        private readonly Dictionary<string, FSMState> _additiveStateMap;
        
        public FSMAdditive(string name) : base(name)
        {
            _additiveStateMap = new Dictionary<string, FSMState>();
        }

        public FSMState AddAdditiveState(string name)
        {
            if (_additiveStateMap.ContainsKey(name))
            {
                Debug.LogWarning("The FSM already constains: "+ name);
                return null;
            }
            
            FSMState newState = new FSMState(name, this);
            _additiveStateMap[name] = newState;

            return newState;
        }

        public FSMState AddAdditiveState(FSMState newState)
        {
            var name = newState.ToString();
            if (_additiveStateMap.ContainsKey(name))
            {
                Debug.LogWarning("The FSM already constains: "+ name);
                return null;
            }
            
            _additiveStateMap[name] = newState;
            EnterAdditiveState(newState);
            
            return newState;
        }

        public FSMState RemoveAdditiveState(string name)
        {
            if (!_additiveStateMap.ContainsKey(name))
            {
                Debug.LogWarning("The FSM does not contain the state");
                return null;
            }

            FSMState removedState = _additiveStateMap[name];
            _additiveStateMap.Remove(name);
            return removedState;
        }
        
        public FSMState RemoveAdditiveState(FSMState oldState)
        {
            var name = oldState.ToString();
            if (!_additiveStateMap.ContainsKey(name))
            {
                Debug.LogWarning("The FSM does not contain the state");
                return null;
            }

            FSMState removedState = _additiveStateMap[name];
            ExitAdditiveState(removedState);
            _additiveStateMap.Remove(name);

            return removedState;
        }

        public void EnterAdditiveState(FSMState state)
        {
            ProcessStateAction(state, delegate(FSMAction action) { 
            action.OnEnter();
             });
        }

        public void ExitAdditiveState(FSMState state)
        {
            ProcessStateAction(state, delegate(FSMAction action) {
                action.OnExit();
              });
        }
        

        public override void Update()
        {
            if (_additiveStateMap.Count > 0)
            {
                var keys = _additiveStateMap.Keys.ToList();
                
                foreach (var key in keys)
                {
                    ProcessStateAction(_additiveStateMap[key], delegate(FSMAction action)
                    {
                        action.OnUpdate();
                    });
                }
            }
            
            base.Update();

        }

        public override void FixedUpdate()
        {
            if (_additiveStateMap.Count > 0)
            {
                var keys = _additiveStateMap.Keys.ToList();
                
                foreach (var key in keys)
                {
                    ProcessStateAction(_additiveStateMap[key], delegate(FSMAction action)
                    {
                        action.OnFixedUpdate();
                    });
                }
            }
            
            base.FixedUpdate();
        }
    }
}

