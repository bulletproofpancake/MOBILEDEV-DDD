using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BuildingBehavior : Singleton<BuildingBehavior>
{
    [SerializeField] private GameObject floorPrefab;
    [SerializeField] private int minNumberOfFloors;
    [SerializeField] private int maxNumberOfFloors;

    private List<GameObject> _buildingFloors = new List<GameObject>();
    [SerializeField] private List<Floor> _floors = new List<Floor>();
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
            //TODO: SWITCH TO AN OBJECT POOL
            var floor = Instantiate(floorPrefab, transform);
            var position = transform.position;
            floor.transform.position = new Vector3(position.x, position.y + i, position.z);
            _buildingFloors.Add(floor);
        }
        
        SelectFloor();
    }

    private void SelectFloor()
    {
        _weakFloor = _floors[Random.Range(0, _floors.Count)];
        _weakFloor.isWeak = true;
        
        print(_weakFloor);
    }

    private void ClearBuilding()
    {
        foreach (var floor in _buildingFloors)
        {
            Destroy(floor);
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
        }
        else
        {
            //TODO: GAME OVER
            print("game over");
        }
    }

}
