using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BuildingBehavior : Singleton<BuildingBehavior>
{
    [SerializeField] private List<Floor> floors = new List<Floor>();
    [SerializeField] private Floor weakFloor;

    private void OnEnable()
    {
        SelectFloor();
    }

    private void OnDisable()
    {
        ResetFloors();
    }

    private void SelectFloor()
    {
        ResetFloors();
        
        weakFloor = floors[Random.Range(0, floors.Count)];
        weakFloor.isWeak = true;
    }

    private void ResetFloors()
    {
        #region Expanded Version
            // foreach (var floor in floors)
            // {
            //     if (floor.isWeak)
            //     {
            //         floor.isWeak = false;
            //     }
            // }
        #endregion

        foreach (var floor in floors.Where(floor => floor.isWeak))
        {
            floor.isWeak = false;
        }
    }

    public void AddFloors(Floor floor)
    {
        floors.Add(floor);
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
