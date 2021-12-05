using UnityEngine;

namespace Ball
{
    public class MoveToDirection : MonoBehaviour
    {
        [SerializeField] private float m_speed;
        private Vector2 m_direction;

        public float Speed 
        {
            get { return m_speed; }
            set 
            {
                m_speed = value;
            }
        }
        public Vector2 Direction
        {
            get { return m_direction; }
            set 
            {
                m_direction = value.normalized;
            }
        }

        void Update()
        {
            transform.position += ((Vector3)Direction) * Speed * Time.deltaTime;
        }
    }
}
