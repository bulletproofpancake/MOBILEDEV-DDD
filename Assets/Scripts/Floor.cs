using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Floor : MonoBehaviour
{
    public BuildingBehavior building;
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
        isWeak = false;
    }

    private void Update()
    {
        _material.color = isWeak ? Color.yellow : Color.white;
    }

    private void OnMouseDown()
    {
        building.DestroyFloor(this, isWeak);
    }
}
