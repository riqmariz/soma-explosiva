using System.Collections.Generic;
using Ball;
using Cinemachine;
using UnityEngine;

namespace Path
{
    public class PathManager : MonoBehaviour
    {
        #region Path
        [Header("Path")]
        [SerializeField] private CinemachineSmoothPath m_path;
        [SerializeField] private List<PathBall> m_currentSpawnedBalls;
        #endregion
        
        #region Spawn
        [Header("Spawn")]
        [SerializeField] private GameObject m_ballPrefab;
        [SerializeField] private int m_ballsToSpawn;
        [SerializeField] private List<int> m_possibleValues = new List<int>();

        [Header("Spawn Probabilities")] 
        [SerializeField] private int m_twoBallsComboPercentage = 70;
        [SerializeField] private int m_threeBallsComboPercentage = 60;
        [SerializeField] private int m_fourBallsComboPercentage = 50;
        private int currentComboCount = 0;
        #endregion

        #region Properties
        public List<PathBall> Balls => m_currentSpawnedBalls;
        public List<int> PossibleValues => m_possibleValues;
        #endregion


        private void Start()
        {
            if (m_currentSpawnedBalls == null)
                m_currentSpawnedBalls = new List<PathBall>();

            if (!m_path)
                m_path = FindObjectOfType<CinemachineSmoothPath>();
            
            if (m_possibleValues == null)
                m_possibleValues = new List<int>();
            
            SpawnBall();
            
            GameManager.GetInstance().Resume();
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
            else 
            {
                if (m_currentSpawnedBalls.Count <= 0) 
                {
                    PopupManager.GetInstance().ShowPopup<GameCompletePopup>();
                    this.enabled = false;
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

            var random = Random.Range(1, 101);
            var probability = 0;
            var value = 0;
            var possibleValues = new List<int>();

            foreach (var possibleValue in m_possibleValues)
            {
                possibleValues.Add(possibleValue);
            }

            
            if (currentComboCount == 0)
            {
                probability = m_twoBallsComboPercentage;
            }
            else if (currentComboCount == 1)
            {
                probability = m_threeBallsComboPercentage;
            }
            else if (currentComboCount >= 2)
            {
                probability = m_fourBallsComboPercentage;
            }

            if (random <= probability)
            {
                value = lastBall.Value;
                currentComboCount++;
            }
            else
            {
                possibleValues.Remove(lastBall.Value);
                var index = Random.Range(0, possibleValues.Count);
                value = possibleValues[index];
                currentComboCount = 0;
            }

            ball.InitBall(m_path, position, speed, value);
            m_currentSpawnedBalls.Add(ball);
        }

        private void SpawnBall()
        {
            var ball = Instantiate(m_ballPrefab, transform).GetComponentInChildren<PathBall>();
            
            var random = Random.Range(0, m_possibleValues.Count);
            ball.InitBall(m_path, 0f, 1f, m_possibleValues[random]);
            m_currentSpawnedBalls.Add(ball);
        }

        public void SpawnBallAt(int hitIndex,int index, int value) 
        {
            Debug.Log("Spawn: \nHitIndex: " + hitIndex + "\nindex: " + index + "\nvalue: " + value);
            if (hitIndex < 0)
                return;
            var lastBall = m_currentSpawnedBalls[hitIndex];

            var ball = Instantiate(m_ballPrefab, transform).GetComponentInChildren<PathBall>();
            var speed = lastBall.Speed;

            var position = lastBall.Position;
            ball.InitBall(m_path, position, speed, value);
            m_currentSpawnedBalls.Insert(index,ball);
            
            for (int i = hitIndex; i >= 0; i--) 
            {
                //if (m_currentSpawnedBalls[i].awatingCollision)
                //    break;

                m_currentSpawnedBalls[i].OffsetPosition(+0.75f);
            }
            
            IdentifyMatches(index, ball.Value);
        }

        private void IdentifyMatchesOnQueueCollision(PathBall b1, PathBall b2) 
        {
            if (b1.Value == b2.Value)
                IdentifyMatches(m_currentSpawnedBalls.IndexOf(b1),b1.Value);
        }

        private void IdentifyMatches(int currentBallIndex, int targetValue)
        {
            var matches = new List<PathBall>();
            
            for (int i = currentBallIndex; i >= 0; i--)
            {
                if (m_currentSpawnedBalls[i].Value == targetValue)
                {
                    matches.Add(m_currentSpawnedBalls[i]);
                }
                else
                {
                    break;
                }
            }
            
            for (int i = currentBallIndex + 1; i < m_currentSpawnedBalls.Count; i++)
            {
                if (m_currentSpawnedBalls[i].Value == targetValue)
                {
                    matches.Add(m_currentSpawnedBalls[i]);
                }
                else
                {
                    break;
                }
            }
            
            DestroyMatches(matches);
        }

        private void DestroyMatches(List<PathBall> matchesToDestroy)
        {
            float speed = m_currentSpawnedBalls[m_currentSpawnedBalls.Count - 1].Speed;

            if (matchesToDestroy.Count >= 3)
            {
                int matchIndex = -1;
                for (int i = 0; i < matchesToDestroy.Count ; i++)
                {
                    var ball = matchesToDestroy[i];
                    if (i == matchesToDestroy.Count - 1) 
                    {
                        matchIndex = m_currentSpawnedBalls.IndexOf(ball);
                    }
                    

                    m_currentSpawnedBalls.Remove(ball);
                    Destroy(ball.transform.parent.gameObject);

                }
                

                if (matchIndex >= 1)
                {
                    Debug.Log(matchIndex - 1);
                    m_currentSpawnedBalls[matchIndex - 1].AwaitCollisionToSetSpeed();

                    for (int i = matchIndex - 1; i >= 0; i--) 
                    {
                        m_currentSpawnedBalls[i].SetSpeed(0);
                    }

                    if (m_currentSpawnedBalls.Count > matchIndex 
                        && m_currentSpawnedBalls[matchIndex - 1].Value == m_currentSpawnedBalls[matchIndex].Value) 
                    {
                        int awaiters = 0;
                        for (int i = matchIndex - 1; i >= 0; i--)
                        {
                            if (m_currentSpawnedBalls[i].awatingCollision) 
                            {
                                if (awaiters == 0)
                                    awaiters++;
                                else
                                    break;
                            }
                            m_currentSpawnedBalls[i].SetSpeed(-6);
                        }
                    }

                    if (m_currentSpawnedBalls[m_currentSpawnedBalls.Count - 1].Speed <= .01f) 
                    {
                        for (int i = m_currentSpawnedBalls.Count - 1; i >= 0; i--) 
                        {
                            if (m_currentSpawnedBalls[i].awatingCollision && i != m_currentSpawnedBalls.Count-1)
                                break;
                            m_currentSpawnedBalls[i].SetSpeed(speed);
                        }
                    }
                }
            }
        }

        public void SendSpeedFoward(PathBall ball, float speed) 
        {
            int index = m_currentSpawnedBalls.IndexOf(ball);

            for (int i = index; i >= 0; i--) 
            {
                if (m_currentSpawnedBalls[i].awatingCollision)
                    break;

                m_currentSpawnedBalls[i].SetSpeed(speed);
            }
            if(index < m_currentSpawnedBalls.Count)
                IdentifyMatchesOnQueueCollision(ball,m_currentSpawnedBalls[m_currentSpawnedBalls.IndexOf(ball)+1]);
        }
    }
}
