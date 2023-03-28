using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// デッキ一覧を表示する
public class DeckView : MonoBehaviour
{
    [SerializeField] CardController cardPrefab;

    private DeckManager deckManager;

    // アクティブになった時に実行
    // デッキ内のカード一覧を表示
    private void OnEnable()
    {
        deckManager = GameObject.FindWithTag("DeckManager").GetComponent<DeckManager>();

        for(int i = 0; i < deckManager.BaseDeck.Count; i++)
        {
            CardController card = Instantiate(cardPrefab, new Vector3(0, 0, 0), Quaternion.identity, this.transform);
            card.transform.localScale = new Vector3(0.8f, 0.8f, 0.8f);
            card.Init(deckManager.BaseDeck[i]);
            card.SetCardState(CardController.CardState.Preview);
        }
    }

    // 非アクティブになったときに実行
    // DeckView内に生成されていたカードをすべて削除
    private void OnDisable()
    {
        foreach (Transform deckView in this.transform)
        {
            GameObject.Destroy(deckView.gameObject);
        }
    }
}
