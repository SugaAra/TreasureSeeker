using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 敵のデータを管理
public class EnemyModel
{
    public string enemyName;
    public int enemyID;
    public bool isTreasure;
    public int enemyHP;
    public int enemyATK;
    public int enemyATKNum;
    public int enemyDEF;
    public Sprite enemyIcon;

    public EnemyModel(int EnemyID)
    {
        EnemyDataBase enemyEntity = Resources.Load<EnemyDataBase>("EnemyDataBase");

        enemyName = enemyEntity.datas[EnemyID].enemyName;
        enemyID = enemyEntity.datas[EnemyID].enemyID;
        isTreasure = enemyEntity.datas[EnemyID].isTreasure;
        enemyHP = enemyEntity.datas[EnemyID].enemyHP;
        enemyATK = enemyEntity.datas[EnemyID].enemyATK;
        enemyATKNum = enemyEntity.datas[EnemyID].enemyATKNum;
        enemyDEF = enemyEntity.datas[EnemyID].enemyDEF;
        enemyIcon = enemyEntity.datas[EnemyID].enemyIcon;

    }
}
