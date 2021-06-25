using UnityEngine;
using Core;
namespace Building
{
    public class BuildingManager : MonoBehaviour
    {
        [SerializeField] public ObjectPool buildingPool;
        [SerializeField] private float spawnRateMin,spawnRateMax;

        private void Start()
        {
            InvokeRepeating("Spawn", 0f, Random.Range(spawnRateMin, spawnRateMax));
        }

        public void Spawn()
        {
            var building = buildingPool.GetPooledObject();
            building.transform.position = transform.position;
            building.SetActive(true);
        }
        
    }
}