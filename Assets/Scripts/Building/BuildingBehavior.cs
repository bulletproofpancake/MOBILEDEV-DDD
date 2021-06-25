using System.Collections;
using System.Collections.Generic;
using Core;
using UnityEngine;

namespace Building
{
    public class BuildingBehavior : MonoBehaviour
    {
        [SerializeField] private ObjectPool floorPool;
        [SerializeField] private int minNumberOfFloors;
        [SerializeField] private int maxNumberOfFloors;

        private List<GameObject> _buildingFloors = new List<GameObject>();
        private List<Floor> _floors = new List<Floor>();
        private Floor _weakFloor;

        private void OnEnable()
        {
            SpawnFloors();
        }

        private void OnDisable()
        {
            ClearBuilding();
        }

        private void SpawnFloors()
        {
            //max amount has +1 because it is not included in Random.Range
            var amountOfFloors = Random.Range(minNumberOfFloors, maxNumberOfFloors + 1);
        
            for (var i = 0; i < amountOfFloors; i++)
            {
                var floor = floorPool.GetPooledObject();
                var position = transform.position;
                floor.transform.position = new Vector3(position.x, position.y + i, position.z);
                _buildingFloors.Add(floor);
                floor.GetComponent<Floor>().building = this;
                floor.SetActive(true);
            }
        
            SelectFloor();
        }

        private void SelectFloor()
        {
            _weakFloor = _floors[Random.Range(0, _floors.Count)];
            _weakFloor.isWeak = true;
            _weakFloor.ChangeColor(Color.yellow);
        }

        private void ClearBuilding()
        {
            foreach (var floor in _buildingFloors)
            {
                floor.SetActive(false);
            }
        }
    
        public void AddFloors(Floor floor)
        {
            _floors.Add(floor);
        }

        public void RemoveFloors(Floor floor)
        {
            _floors.Remove(floor);
        }
    
        /// <summary>
        /// Destroys the floor selected by the player.
        /// </summary>
        /// <param name="floor">
        /// The floor to be destroyed.
        /// </param>
        /// <param name="isWeak">
        /// Checks whether the floor is weakened so that the correct sequence can be made.
        /// </param>
        public void DestroyFloor(Floor floor, bool isWeak)
        {
            //TODO: INSTANTIATE SMOKE/DESTRUCTION EFFECT
        
            if (isWeak)
            {
                floor.gameObject.SetActive(false);
                //TODO: ADD SCORE TO PLAYER
                print("weak floor destroyed");
                StartCoroutine(DestroyBuilding(true));
            }
            else
            {
                //TODO: GAME OVER
                StartCoroutine(DestroyBuilding(false));
                print("game over");
            }
        }

        private IEnumerator DestroyBuilding(bool isCorrect)
        {
            foreach (var floor in _floors)
            {
                floor.ChangeColor(isCorrect ? Color.green : Color.red);
            }
            yield return new WaitForSeconds(1f);
            ClearBuilding();
        }

    }
}
