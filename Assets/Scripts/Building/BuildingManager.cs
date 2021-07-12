using UnityEngine;
using Core;
using Rhythm;

namespace Building
{
    public class BuildingManager : MonoBehaviour
    {
        [SerializeField] public ObjectPool buildingPool;
        [SerializeField] private float spawnRateMin,spawnRateMax;

        private void Start()
        {
            AudioManager.Instance.Play("bgm");
            
            print(BeatScroller.Instance.beatTempo);
            InvokeRepeating("Spawn", 0f, BeatScroller.Instance.beatTempo);
            print(BeatScroller.Instance.beatTempo);
        }

        public void Spawn()
        {
            var building = buildingPool.GetPooledObject();
            building.transform.position = transform.position;
            building.SetActive(true);
        }
        
    }
}