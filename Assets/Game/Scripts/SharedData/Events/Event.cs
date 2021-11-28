using System;
using UnityEngine;

namespace SharedData.Events
{
    [CreateAssetMenu(fileName = "Event", menuName = "Shared/Events/No Parameter Event", order = 0)]
    public class Event : ScriptableObject
    {
        #region Fields
        private Action m_listeners;
        #endregion Fields
        
        #region Methods
        public void Raise()
        {
            m_listeners?.Invoke();
        }
        
        public void AddCallback(Action callback){ m_listeners += callback; }
        public void RemoveCallback(Action callback){ m_listeners -= callback; }
        #endregion Methods
    }
}