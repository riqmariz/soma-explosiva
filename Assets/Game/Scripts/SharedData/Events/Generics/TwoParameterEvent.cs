using System;
using UnityEngine;

namespace SharedData.Events.Generics
{
    public abstract class TwoParameterEvent<T1,T2> : ScriptableObject
    {
        #region Fields
        private Action<T1,T2> m_listeners;
        #endregion Fields
        
        #region Methods
        public void Raise(T1 data1, T2 data2)
        {
            m_listeners?.Invoke(data1,data2);
        }
        
        public void AddCallback(Action<T1,T2> callback){ m_listeners += callback; }
        public void RemoveCallback(Action<T1,T2> callback){ m_listeners -= callback; }
        #endregion Methods
    }
}