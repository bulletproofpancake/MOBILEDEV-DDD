using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Floor : MonoBehaviour
{
    private Material _material;
    public bool isWeak;
    
    private void Awake()
    {
        _material = GetComponent<MeshRenderer>().material;
    }
    
    private void OnEnable()
    {
        BuildingBehavior.Instance.AddFloors(this);
    }

    private void OnDisable()
    {
        BuildingBehavior.Instance.RemoveFloors(this);
        isWeak = false;
    }

    private void Update()
    {
        _material.color = isWeak ? Color.yellow : Color.white;
    }

    private void OnMouseDown()
    {
        BuildingBehavior.Instance.DestroyFloor(this, isWeak);
    }
}
