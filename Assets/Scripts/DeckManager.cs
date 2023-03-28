using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// プレイヤーのデッキを管理する
public class DeckManager : MonoBehaviour
{
    // 初期デッキ
    // 赤3(攻撃アップx1, 無x2), 赤青2(無x2), 青3(ガードx1, 無x2), 赤黄2(無x2), 黄3(キュアx1, 無x2),  青黄2(無x2). 計15枚
    public List<int> BaseDeck = new List<int>() { 0, 0, 12, 3, 3, 1, 1, 7, 4, 4, 2, 2, 20, 5, 5};
    List<int> nowDeck = new List<int>() { };    // シャッフルしたデッキ

    // カードのIDを取得
    public int GetCardID()
    {
        return nowDeck[0];
    }

    // デッキのシャッフルと補充
    public void Shuffle()
    {
        nowDeck.Clear();

        int n = BaseDeck.Count;

        for (int i = 0; i < n; i++)
        {
            nowDeck.Add(BaseDeck[i]);
        }

        while (n > 1)
        {
            n--;

            int k = UnityEngine.Random.Range(0, n + 1);

            int temp = nowDeck[k];
            nowDeck[k] = nowDeck[n];
            nowDeck[n] = temp;
        }
    }

    // デッキが空かどうか
    public bool DeckIsEmpty()
    {
        if(nowDeck.Count == 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    // ドローした際にデッキからカードを削除
    public void CardRemove()
    {
        nowDeck.RemoveAt(0);
    }

    // Baseデッキの編集
    public void EditBaseDeck(int deckID, int cardID)
    {
        BaseDeck[deckID] = cardID;
    }
}
