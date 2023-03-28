using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyEmerge : MonoBehaviour
{
    [SerializeField] EnemyController enemyPrefab;

    // 次の敵?
    public void NextEnemy(FloorEnemyManager enemyDeck)
    {
        if (enemyDeck.EnemyDeckIsEmpty())
        {
            return;
        }

        int enemyID = enemyDeck.GetEnemyID();
        enemyDeck.EnemyRemove();
        CreateCard(enemyID, enemyPrefab);
        if (enemyID == 0)
        {
            TreasureGet();
            return;
        }
    }

    // 宝箱発見時
    public bool TreasureGet()
    {
        return true;
    }

    // カード生成(動かせない)
    public void CreateCard(int enemyID, EnemyController enemyprefab)
    {
        EnemyController enemy = Instantiate(enemyprefab, this.transform);
        enemy.Init(enemyID);
    }

    // 宝のときオブジェクトを消す
    public void EnemyDestroy()
    {
        Destroy(transform.Find("Enemy(Clone)").gameObject);
    }

}
