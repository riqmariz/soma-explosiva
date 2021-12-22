using System;
using UnityEngine;

namespace SharedData.Events.Generics
{
    public abstract class OneParameterEventWithReturn<T1, TResult> : ScriptableObject 
    {
        #region Fields
        private Func<T1, TResult> m_listeners;
        private Action<TResult> m_eventResponse;
        #endregion Fields
        
        #region Methods
        public TResult Raise(T1 data)
        {
            var result = m_listeners.Invoke(data);
            m_eventResponse?.Invoke(result);
            return result;
        }
        
        // TODO: This += will use only the last registered event!! However, this shouldn't be a problem for this project
        //       since only LevelManager should add a callback for it
        public void AddCallback(Func<T1, TResult> callback){ m_listeners += callback; }
        public void RemoveCallback(Func<T1, TResult> callback){ m_listeners -= callback; }
        public void AddResponse(Action<TResult> action) { m_eventResponse += action; }
        public void RemoveResponse(Action<TResult> action) { m_eventResponse -= action; }
        #endregion Methods
    }
}