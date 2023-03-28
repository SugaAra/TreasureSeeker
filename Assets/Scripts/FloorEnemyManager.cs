using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorEnemyManager : MonoBehaviour
{
    List<int> EnemyList = new List<int>() { };          // フロアに出現する敵一覧
    List<int> nowEnemyList = new List<int>() { };       // 敵の出現順

    [SerializeField] TextUIManager textUIManager;

    public int floor = 1;

    // 新しいフロアに出る敵を準備する
    public void NewFloor()
    {
        FloorModel model = new FloorModel(floor);
        EnemyList = new List<int>(model.FloorEnemyList);
        Shuffle();

        textUIManager.ShowFloor(floor);
        floor++;
    }

    // 敵のIDを取得する
    public int GetEnemyID()
    {
        return nowEnemyList[0];
    }

    // 敵の出現順をランダムに並べ替える
    public void Shuffle()
    {
        nowEnemyList.Clear();

        int n = EnemyList.Count;

        for (int i = 0; i < n; i++)
        {
            nowEnemyList.Add(EnemyList[i]);
        }

        while (n > 1)
        {
            n--;

            int k = UnityEngine.Random.Range(0, n + 1);

            int temp = nowEnemyList[k];
            nowEnemyList[k] = nowEnemyList[n];
            nowEnemyList[n] = temp;
        }

        nowEnemyList.Add(0);    // 末尾に宝箱を設置する
    }

    // フロア内の敵をすべて倒したかどうか
    public bool EnemyDeckIsEmpty()
    {
        if (nowEnemyList.Count == 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    // 敵を倒したときリストから消す
    public void EnemyRemove()
    {
        nowEnemyList.RemoveAt(0);
    }
}
