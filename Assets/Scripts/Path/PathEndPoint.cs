﻿using System;
using Ball;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Path
{
    public class PathEndPoint : MonoBehaviour
    {
        [SerializeField] private int m_ballsToLose;

        private void Update()
        {
            if (m_ballsToLose <= 0)
            {
                GameOver();
            }
        }

        private void GameOver()
        {
            Debug.Log("Game Over");
            //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        private void OnTriggerEnter(Collider other)
        {
            var ball = other.GetComponent<PathBall>();
            if (!ball) return;
            
            Debug.Log("Ball entered end point");
            m_ballsToLose--;
            Destroy(ball.transform.parent.gameObject);
        }
    }
}