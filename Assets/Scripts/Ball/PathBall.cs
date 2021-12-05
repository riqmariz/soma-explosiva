using System;
using Cinemachine;
using TMPro;
using UnityEngine;

namespace Ball
{
    public class PathBall : MonoBehaviour
    {
        #region Cart
        [Header("Cart")]
        [SerializeField] private CinemachineDollyCart m_cartController;
        [SerializeField] private float m_speedMultiplier = 1f;
        [SerializeField] private Transform m_sphere;
        [SerializeField] private Transform m_jillisAfterBall;
        [SerializeField] private Transform m_jillisBeforeBall;
        private const float m_speed = 1f;
        #endregion

        #region Value
        [Header("Value")]
        [SerializeField] private int m_value;
        [SerializeField] private TextMeshPro m_valueText;
        #endregion

        #region Properties
        public float Position => m_cartController.m_Position;
        public float Speed => m_speedMultiplier;

        public Tuple<Transform, Transform> BeforeAfterBall => new Tuple<Transform, Transform>(m_jillisBeforeBall,m_jillisAfterBall);

        #endregion

        #region Monobehaviour Methods
        private void Start()
        {
            if (!m_cartController)
                m_cartController = GetComponent<CinemachineDollyCart>();
            
            if(!m_sphere)
                m_sphere = transform;
        }
        #endregion
        
        public void InitBall(CinemachineSmoothPath path, float position, float speed, int value)
        {
            m_cartController.m_Path = path;
            SetPosition(position);
            SetValue(value);
            SetSpeed(speed);
        }

        private void SetValue(int value)
        {
            m_value = value;
            m_valueText.text = m_value.ToString();
        }
        
        public void SetSpeed(float speed)
        {
            m_speedMultiplier = speed;
            m_cartController.m_Speed = m_speedMultiplier * m_speed;
        }

        public void SetPosition(float position)
        {
            m_cartController.m_Position = position;
        }

        public void OffsetPosition(float offset)
        {
            m_cartController.m_Position += offset;
        }
    }
}
