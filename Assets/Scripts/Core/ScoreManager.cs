using System;
using System.Collections;
using Building;
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
        private Score _stateCount;

        private void Start()
        {
            ResetCurrentScore();
            _highScore = GameManager.Instance.highScore;
            gameOverDialogue.SetActive(false);
            _stateCount = new Score();
        }
        
        private void ResetCurrentScore()
        {
            _currentScore = 0;
            currentScoreDisplay.text = $"Score: {_currentScore}";
            //ClearStateCount();
        }
        
        public void AddScore(int points)
        {
            _currentScore += points;
            currentScoreDisplay.text = $"Score: {_currentScore}";
        }

        /// <summary>
        /// Adds points to the current score and tracks how many times a state has been hit.
        /// </summary>
        /// <param name="points">amount of points to be added</param>
        /// <param name="state">what was the state of the building when it was hit</param>
        public void AddScore(int points, HitState state)
        {
            _currentScore += points;
            switch (state)
            {
                case HitState.Early:
                    _stateCount.early++;
                    break;
                case HitState.Perfect:
                    _stateCount.perfect++;
                    break;
                case HitState.Miss:
                    _stateCount.miss++;
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(state), state, null);
            }
            currentScoreDisplay.text = $"Score: {_currentScore}";
        }
        
        private void SetHighScore()
        {
            if (_currentScore > _highScore)
            {
                _highScore = _currentScore;
            }
            highScoreDisplay.text = $"High Score: {_highScore}\n" +
                                    $"Early: {_stateCount.early}\n" +
                                    $"Perfect: {_stateCount.perfect}\n" +
                                    $"Miss: {_stateCount.miss}";
            
            GameManager.Instance.highScore = _highScore;
        }

        public void DisplayHighScore()
        {
            SetHighScore();
            gameOverDialogue.SetActive(true);
        }

    }

    [Serializable]
    public class Score
    {
        public int early, perfect, miss;
    }
}