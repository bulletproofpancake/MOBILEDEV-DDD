using UnityEngine;

namespace Building
{
    public class Floor : MonoBehaviour
    {
        public Building building;
        private Material _material;
        public bool isWeak;
    
        private void Awake()
        {
            _material = GetComponent<MeshRenderer>().material;
        }
    
        private void OnEnable()
        {
            if (building == null) return;
            building.AddFloors(this);
        }

        private void OnDisable()
        {
            if (building == null) return;
            building.RemoveFloors(this);
            ChangeColor(Color.white);
            isWeak = false;
        }

        private void OnMouseDown()
        {
            building.DestroyFloor(this, isWeak);
        }

        public void ChangeColor(Color color)
        {
            _material.color = color;
        }
    
    }
}
