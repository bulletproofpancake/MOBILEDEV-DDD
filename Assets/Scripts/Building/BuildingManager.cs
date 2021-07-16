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
            AudioManager.Instance.Play("160bgm");
            var bpm = GameManager.Instance.bpm / 60f;
            InvokeRepeating("Spawn", bpm, bpm);
        }

        public void Spawn()
        {
            var building = buildingPool.GetPooledObject();
            building.transform.position = transform.position;
            building.SetActive(true);
        }
        
    }
}