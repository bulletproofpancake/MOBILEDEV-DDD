using UnityEngine;
using UnityEngine.UI;

namespace Core
{
    public class GameManager : Singleton<GameManager>
    {
        [Header("Building Movement")]
        [Tooltip("Determines the rate of how slow the building moves. Higher is slower.")]
        [SerializeField] private int buildingRate;
        private float _buildingMovementRate;

        /// <summary>
        /// Global movement rate for buildings so that they move faster as the game goes on.
        /// </summary>
        /// <returns>The movement rate for the building</returns>
        public float BuildingMovementRate()
        {
            // buildingRate and 0.001f are random values chosen through trial and error until the movement feels natural.
            _buildingMovementRate += Time.deltaTime / buildingRate * 0.001f;
            return _buildingMovementRate;
        }

        public void GameOver()
        {
            //TODO: GAME OVER
        }
        
    }
}
