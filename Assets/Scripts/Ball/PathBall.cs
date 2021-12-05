using System;
using Cinemachine;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Ball
{
    public class PathBall : MonoBehaviour
    {
        #region Cart
        [Header("Cart")]
        [SerializeField] private CinemachineDollyCart m_cartController;
        [SerializeField] private float m_speedMultiplier = 1f;
        [SerializeField] private Transform m_sphere;
        private const float Speed = 2f;
        #endregion

        #region Value
        [Header("Value")]
        [SerializeField] private int m_value;
        [SerializeField] private TextMeshPro m_valueText;
        #endregion
        
        #region TEST
        [Header("TEST")]
        [SerializeField] private CinemachineSmoothPath m_path;
        #endregion

        #region Monobehaviour Methods
        private void Start()
        {
            if (!m_cartController)
                m_cartController = GetComponent<CinemachineDollyCart>();
            
            if(!m_sphere)
                m_sphere = transform;
            
            //Test-only
            InitBall(m_path, Random.Range(1, 11));
        }
        #endregion

        public void InitBall(CinemachineSmoothPath path, int value)
        {
            m_cartController.m_Path = path;
            //m_cartController.m_Position = 0f;
            SetValue(value);
            SetSpeed(m_speedMultiplier);
        }

        private void SetValue(int value)
        {
            m_value = value;
            m_valueText.text = m_value.ToString();
        }

        /// <summary>
        /// Set speed of the current path ball instance
        /// </summary>
        /// <param name="speed">Use positive speeds to move the ball forward, zero to stop the ball and negative to move it backwards</param>
        public void SetSpeed(float speed)
        {
            m_speedMultiplier = speed;
            m_cartController.m_Speed = m_speedMultiplier * Speed;
        } 
    }
}
