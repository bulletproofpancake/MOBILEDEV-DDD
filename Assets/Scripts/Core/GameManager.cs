using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Core
{
    public class GameManager : Singleton<GameManager>
    {
        public bool isGameOver;

        public int highScore;
        
        #region Building Acceleration

        [Header("Building Acceleration")]
        // Sets how long it takes to reach maximum rate
        // Higher is longer
        [SerializeField] private float weight;
        // Starts at 1 because starting from 0 is too slow
        [SerializeField] private float accelerationRate = 1f;
        [Tooltip("Set to a value that is still playable")]
        // Caps the speed of the game so that it is still playable
        [SerializeField] private float maxAccelerationRate;

        /// <summary>
        /// Calls when the game starts so that the acceleration rate does not increase while in the main menu
        /// </summary>
        public void StartGame()
        {
            isGameOver = false;
            accelerationRate = 1f;
            AudioManager.Instance.Stop("bgm");
            AudioManager.Instance.Play("160bgm");
        }
        
        private void Update()
        {
            // returns when GameOver is called so that buildings stop moving
            if (isGameOver) return;
            
            // Time.deltaTime is divided by the weight so that it takes longer to reach the maximum rate
            accelerationRate += Time.deltaTime / weight;
            
            if (accelerationRate >= maxAccelerationRate)
            {
                accelerationRate = maxAccelerationRate;
            }
        }

        /// <summary>
        /// Ensures that the buildings that spawn go faster over time.
        /// </summary>
        public float BuildingAccelerationRate()
        {
            return accelerationRate;
        }

        #endregion
        public void GameOver()
        {
            print("game over");
            StartCoroutine(GameOverRoutine());
        }

        private IEnumerator GameOverRoutine()
        {
            isGameOver = true;
            accelerationRate = 0f;
            ScoreManager.Instance.DisplayHighScore();
            yield return new WaitForSeconds(3f);
            Scenes.Instance.Load("MainMenu");
        }
        
    }
}
