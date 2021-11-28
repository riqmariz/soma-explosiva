using UnityEngine;

namespace Data
{
    public class Timer
    {
        #region Fields
        private bool m_isStopped;
        private float m_startTime;
        #endregion Fields

        #region Constructor
        public Timer()
        {
            m_startTime = Time.time;
        }
        #endregion Constructor

        
        #region Methods
        public float ElapsedTime()
        {
            return Time.time - m_startTime;
        }

        public void Reset()
        {
            m_startTime = Time.time;
        }
        #endregion Methods
    }
}