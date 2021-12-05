using UnityEngine;

namespace Path
{
    public class PathEndPoint : MonoBehaviour
    {
        [SerializeField] private int m_ballsToLose;

        // Update is called once per frame
        void Update()
        {
            if (m_ballsToLose <= 0)
            {
                GameOver();
            }
        }

        private void GameOver()
        {
            
        }
    }
}
