using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class FloorEntity : ScriptableObject
{
    public int nowfloor;
    public List<int> FloorEnemy = new List<int>();
}
