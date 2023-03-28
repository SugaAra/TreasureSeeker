using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 山札を管理
public class NextHandController : MonoBehaviour
{
    [SerializeField] CardController cardPrefab;

    // 次のカードを表示する
    public void NextCard(DeckManager deck)
    {
        if (deck.DeckIsEmpty())
        {
            CardDestroy();
            return;
        }

        int cardID = deck.GetCardID();
        CreateCard(cardID, cardPrefab);
    }

    // カード生成(動かせない)
    public void CreateCard(int cardID, CardController cardprefab)
    {
        CardController card = Instantiate(cardprefab, this.transform);
        card.Init(cardID);
        card.SetCardState(CardController.CardState.Preview);

    }

    // 残ったカード全削除
    public void CardDestroy()
    {
        foreach (Transform NextHand in this.transform)
        {
            GameObject.Destroy(NextHand.gameObject);
        }
    }
}
