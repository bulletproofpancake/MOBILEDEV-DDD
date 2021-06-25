using UnityEngine;
using UnityEngine.UI;

namespace Core
{
    public class ScoreManager : Singleton<ScoreManager>
    {
        [SerializeField] private Text display;
        private int _score;

        private void Start()
        {
            _score = 0;
            display.text = $"Score: {_score}";
        }

        public void AddScore(int points)
        {
            _score += points;
            display.text = $"Score: {_score}";
        }
    }
}