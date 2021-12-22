using System;
using UnityEngine;

namespace SharedData.Events.Generics
{
    public abstract class OneParameterEvent<T1> : ScriptableObject
    {
        #region Fields
        private Action<T1> m_listeners;
        #endregion Fields
        
        #region Methods
        public void Raise(T1 data)
        {
            m_listeners?.Invoke(data);
        }
        
        public void AddCallback(Action<T1> callback){ m_listeners += callback; }
        public void RemoveCallback(Action<T1> callback){ m_listeners -= callback; }
        #endregion Methods
    }
}