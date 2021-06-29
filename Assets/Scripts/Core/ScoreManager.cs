using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Core
{
    public class ScoreManager : Singleton<ScoreManager>
    {
        [SerializeField] private Text currentScoreDisplay;
        [SerializeField] private Text highScoreDisplay;
        [SerializeField] private GameObject gameOverDialogue;
        private int _currentScore;
        private int _highScore;

        private void Start()
        {
            ResetCurrentScore();
            _highScore = GameManager.Instance.highScore;
            gameOverDialogue.SetActive(false);
        }
        
        private void ResetCurrentScore()
        {
            _currentScore = 0;
            currentScoreDisplay.text = $"Score: {_currentScore}";
        }
        
        public void AddScore(int points)
        {
            _currentScore += points;
            currentScoreDisplay.text = $"Score: {_currentScore}";
        }

        private void SetHighScore()
        {
            if (_currentScore > _highScore)
            {
                _highScore = _currentScore;
            }
            highScoreDisplay.text = $"High Score: {_highScore}";
            
            GameManager.Instance.highScore = _highScore;
        }

        public void DisplayHighScore()
        {
            SetHighScore();
            gameOverDialogue.SetActive(true);
        }
    }
}