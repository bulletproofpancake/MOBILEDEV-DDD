using UnityEngine;
using UnityEngine.UI;

namespace Core
{
    public class GameManager : Singleton<GameManager>
    {
        
        [Header("Building Movement")] 
        [Range(0.1f,0.5f)]
        [SerializeField] private float speedOverTime;

        /// <summary>
        /// Ensures that the buildings that spawn go faster over time.
        /// </summary>
        public float BuildingAccelerationRate()
        {
            //Reference:
            //https://answers.unity.com/questions/1170967/how-to-get-speed-to-very-gradually-pick-up-over-ti.html
            var accelerationRate = speedOverTime * Time.deltaTime / 100;
            return accelerationRate;
        }

        public void GameOver()
        {
            //TODO: GAME OVER
        }
        
    }
}
