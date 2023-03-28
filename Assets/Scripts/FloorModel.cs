using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorModel
{
    public int nowfloor;
    public List<int> FloorEnemyList = new List<int>();

    public FloorModel(int floor)
    {
        FloorEntity floorEntity = Resources.Load<FloorEntity>("FloorEntityList/Floor" + floor);

        nowfloor = floorEntity.nowfloor;
        FloorEnemyList = new List<int> (floorEntity.FloorEnemy);
    }
}
