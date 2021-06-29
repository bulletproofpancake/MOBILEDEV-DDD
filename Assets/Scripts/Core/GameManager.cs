using UnityEngine;
using UnityEngine.UI;

namespace Core
{
    public class GameManager : Singleton<GameManager>
    {
        [Header("Building Acceleration")]
        // Sets how long it takes to reach maximum rate
        // Higher is longer
        [SerializeField] private float weight;
        // Starts at 1 because starting from 0 is too slow
        [SerializeField] private float _accelerationRate = 1f;
        [Tooltip("Set to a value that is still playable")]
        // Caps the speed of the game so that it is still playable
        [SerializeField] private float maxAccelerationRate;
        
        /// <summary>
        /// Ensures that the buildings that spawn go faster over time.
        /// </summary>
        public float BuildingAccelerationRate()
        {
            // Time.deltaTime is divided by the weight so that it takes longer to reach the maximum rate
            _accelerationRate += Time.deltaTime / weight;
            
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
