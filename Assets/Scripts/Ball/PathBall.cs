using System;
using System.Collections.Generic;
using Cinemachine;
using DG.Tweening;
using Path;
using TMPro;
using UnityEngine;

namespace Ball
{
    public class PathBall : MonoBehaviour
    {
        public bool awatingCollision = false;

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

        public int Value => m_value;

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
            SetColor(value);
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

        private void SetColor(int value)
        {
            var meshRenderer = GetComponent<MeshRenderer>();
            var ballColorManager = GetComponent<BallColorManager>();
            
            ballColorManager.SetColor(meshRenderer, value);
        }

        public void SetPosition(float position)
        {
            m_cartController.m_Position = position;
        }

        public void OffsetPosition(float offset)
        {
            m_cartController.m_Position += offset;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (awatingCollision) 
            {
                var pathBall = other.GetComponent<PathBall>();
                if (pathBall && pathBall.Speed > .01f) 
                {
                    awatingCollision = false;
                    RemoveRb();
                    GameObject.FindObjectOfType<PathManager>().SendSpeedFoward(this, other.GetComponent<PathBall>().Speed);
                }
            }

        }

        private void RemoveRb() 
        {
            var rb = GetComponent<Rigidbody>();

            if (rb)
                Destroy(rb);
        }

        private void AddRb() 
        {
            var rb = gameObject.AddComponent<Rigidbody>();

            rb.useGravity = false;
        }

        public void AwaitCollisionToSetSpeed() 
        {
            AddRb();
            awatingCollision = true;
        }
    }
}
