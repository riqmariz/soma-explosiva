using System.Collections.Generic;
using Ball;
using Cinemachine;
using UnityEngine;

namespace Path
{
    public class PathManager : MonoBehaviour
    {
        [SerializeField] private GameObject m_ballPrefab;
        [SerializeField] private int m_ballsToSpawn;
        [SerializeField] private CinemachineSmoothPath m_path;
        [SerializeField] private List<PathBall> m_currentSpawnedBalls;

        public List<PathBall> Balls => m_currentSpawnedBalls;
        
        private void Start()
        {
            if (m_currentSpawnedBalls == null)
                m_currentSpawnedBalls = new List<PathBall>();

            if (!m_path)
                m_path = FindObjectOfType<CinemachineSmoothPath>();
            
            SpawnBall();
        }

        private void Update()
        {
            if (m_ballsToSpawn > 0)
            {
                var lastBall = m_currentSpawnedBalls[m_currentSpawnedBalls.Count - 1];
                if (ShouldSpawnNextBall(lastBall))
                {
                    SpawnBall(lastBall);
                    m_ballsToSpawn--;
                }
            }
        }

        private bool ShouldSpawnNextBall(PathBall currentBall)
        {
            return currentBall.Position >= 0.75f;
        }

        private void SpawnBall(PathBall lastBall)
        {
            var ball = Instantiate(m_ballPrefab,transform).GetComponentInChildren<PathBall>();
            var speed = lastBall.Speed;
            var position = lastBall.Position - 0.75f;
            var value = Random.Range(1, 11);
            ball.InitBall(m_path, position, speed, value);
            m_currentSpawnedBalls.Add(ball);
        }

        private void SpawnBall()
        {
            var ball = Instantiate(m_ballPrefab, transform).GetComponentInChildren<PathBall>();
            var value = Random.Range(1, 11);
            ball.InitBall(m_path, 0f, 1f, value);
            m_currentSpawnedBalls.Add(ball);
        }

        public void SpawnBallAt(int hitIndex,int index, int value) 
        {
            var lastBall = m_currentSpawnedBalls[hitIndex];

            var ball = Instantiate(m_ballPrefab, transform).GetComponentInChildren<PathBall>();
            var speed = lastBall.Speed;

            var position = lastBall.Position;
            ball.InitBall(m_path, position, speed, value);
            m_currentSpawnedBalls.Insert(index,ball);
            
            for (int i = hitIndex; i >= 0; i--) 
            {
                m_currentSpawnedBalls[i].OffsetPosition(+0.75f);
            }
        }
    }
}
