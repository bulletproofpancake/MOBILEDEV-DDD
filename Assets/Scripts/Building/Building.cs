using System;
using System.Collections;
using System.Collections.Generic;
using Core;
using UnityEngine;
using UnityEngine.EventSystems;
using Random = UnityEngine.Random;

namespace Building
{
    public class Building : MonoBehaviour
    {
        private enum HitState
        {
            Early,
            Perfect,
            Miss
        }

        [Serializable]
        private class Score
        {
            public int early, perfect, miss;
        }
        
        [Header("Floor Spawning")]
        [SerializeField] private ObjectPool floorPool;
        [SerializeField] private int minNumberOfFloors;
        [SerializeField] private int maxNumberOfFloors;

        [Header("Effects")]
        [SerializeField] private GameObject destructionEffect;
        [SerializeField] private GameObject brokenEffect;

        [Header("Scoring")]
        [SerializeField] private Score scoreValue;

        private List<GameObject> _buildingFloors = new List<GameObject>();
        private List<Floor> _floors = new List<Floor>();
        private Floor _weakFloor;
        private Collider _collider;
        private HitState _hitState;
        private float _movementSpeed;

        private void Start()
        {
            _collider = GetComponent<Collider>();
        }

        private void OnEnable()
        {
            if (_collider != null)
                _collider.enabled = true;
            SpawnFloors();
            _hitState = HitState.Early;
        }

        private void OnDisable()
        {
            ClearBuilding();
        }

        private void Update()
        {
            Move();
        }

        private void Move()
        {
            _movementSpeed = GameManager.Instance.bpm / 60f;
            transform.Translate(Vector3.left * (_movementSpeed * Time.deltaTime));
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
            StartCoroutine(DestroyBuilding(floor, isWeak));
            if (isWeak)
            {
                floor.gameObject.SetActive(false);
                switch (_hitState)
                {
                    case HitState.Early:
                        ScoreManager.Instance.AddScore(scoreValue.early);
                        break;
                    case HitState.Perfect:
                        ScoreManager.Instance.AddScore(scoreValue.perfect);
                        break;
                    case HitState.Miss:
                        ScoreManager.Instance.AddScore(scoreValue.miss);
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
                ClearBuilding();
            }
            else
            {
                GameManager.Instance.GameOver();
                print("game over");
            }
        }
        
        /// <summary>
        /// Instantiates the explosion/dust particle system and despawns the building.
        /// </summary>
        /// <param name="floor">The floor where the particle effect will spawn</param>
        /// <param name="isCorrect">Determines what type of particle effect will be spawned</param>
        /// <returns></returns>
        private IEnumerator DestroyBuilding(Floor floor, bool isCorrect)
        {
            #region Expanded Version
                // GameObject fx;
                // if (isCorrect)
                // {
                //     fx = Instantiate(destructionEffect, transform);
                // }
                // else
                // {
                //     fx = Instantiate(brokenEffect, transform);
                // }
            #endregion
            //Spawns the corresponding effect depending on the answer of the player
            var fx = isCorrect ? Instantiate(destructionEffect, transform) : Instantiate(brokenEffect, transform);
            fx.transform.position = floor.transform.position;
            var duration = fx.GetComponent<ParticleSystem>().main.duration;
            
            //Disables the building's colliders so that the despawner does not deactivate the building
            //While the effect is still playing
            _collider.enabled = false;
            
            yield return new WaitForSeconds(duration);
            Destroy(fx);
            gameObject.SetActive(false);
        }

        private void OnTriggerEnter(Collider other)
        {

            if (other.CompareTag("Activator"))
            {
                _weakFloor.ChangeColor(Color.green);
                _hitState = HitState.Perfect;
            }
            
            if (other.CompareTag("Despawner"))
            {
                GameManager.Instance.GameOver();
                gameObject.SetActive(false);
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("Activator"))
            {
                _weakFloor.ChangeColor(Color.red);
                _hitState = HitState.Miss;
            }
        }
        
        // private IEnumerator DestroyBuilding(bool isCorrect, GameObject effect)
        // {
        //     foreach (var floor in _floors)
        //     {
        //         floor.ChangeColor(isCorrect ? Color.green : Color.red);
        //     }
        //     yield return new WaitForSeconds(1f);
        //     Destroy(effect);
        //     ClearBuilding();
        // }

    }
}
