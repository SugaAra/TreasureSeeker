using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDataBase : ScriptableObject
{
    public EnemyData[] datas;
}

[System.Serializable]
public class EnemyData
{
    public string enemyName;
    public int enemyID;
    public bool isTreasure;
    public int enemyHP;
    public int enemyATK;
    public int enemyATKNum;
    public int enemyDEF;
    public Sprite enemyIcon;
}
