using UnityEngine;
using UnityEngine.UI;

namespace Core
{
    public class GameManager : Singleton<GameManager>
    {
        //Starts at 1 because starting from 0 is too slow
        private float _accelerationRate = 1f;
        [Tooltip("Set to a value that is still playable")]
        // Caps the speed of the game so that it is still playable
        [SerializeField] private float maxAccelerationRate;
        
        /// <summary>
        /// Ensures that the buildings that spawn go faster over time.
        /// </summary>
        public float BuildingAccelerationRate()
        {
            // Time.deltaTime is divided by 30f so that it takes longer to reach the maximum rate
            _accelerationRate += Time.deltaTime / 30f;
            
            if (_accelerationRate >= maxAccelerationRate)
            {
                _accelerationRate = maxAccelerationRate;
            }

            return _accelerationRate;
        }

        public void GameOver()
        {
            //TODO: GAME OVER
        }
        
    }
}
