using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 手札を管理する
public class PlayerHandController : MonoBehaviour
{
    [SerializeField] CardController cardPrefab;

    // 手札をデッキからドロー
    public void DrawCard(DeckManager deck)
    {
        if (deck.DeckIsEmpty())
        {
            return;
        }

        int cardID = deck.GetCardID();
        deck.CardRemove();
        CreateCard(cardID, cardPrefab);
    }

    // カード生成(動かせる)
    public void CreateCard(int cardID, CardController cardprefab)
    {
        CardController card = Instantiate(cardprefab, this.transform);
        card.Init(cardID);
        card.SetCardState(CardController.CardState.Attack);
    }

    // 残ったカードを削除
    public void CardDestroy()
    {
        foreach (Transform playerHand in this.transform)
        {
            GameObject.Destroy(playerHand.gameObject);
        }
    }
}
