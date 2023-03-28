using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EditDeckPanel : MonoBehaviour
{
    [SerializeField] CardController cardPrefab;
    [SerializeField] DeckManager deckManager;
    [SerializeField] GameObject treasureSelectPanel;
    [SerializeField] GameObject[] cardPlace;
    [SerializeField] GameObject getCard;

    private CardController[] card = new CardController[15];
    private int changeCardID;

    private void OnEnable()
    {
        DeckPreview();
    }

    private void OnDisable()
    {
        for (int i = 0; i < deckManager.BaseDeck.Count; i++)
        {
            GameObject.Destroy(cardPlace[i].transform.GetChild(0).gameObject);
        }
        GameObject.Destroy(getCard.transform.GetChild(0).gameObject);
    }

    // 押したボタンの場所のカードを変更する
    public void Push_Button(int buttonNum)
    {
        if (buttonNum == 100)    // NoChangeボタンの時
        {
            this.gameObject.SetActive(false);
        }
        else
        {
            deckManager.EditBaseDeck(buttonNum, changeCardID);
            treasureSelectPanel.SetActive(false);
            this.gameObject.SetActive(false);
        }
    }

    // TreasureSelectPanel(強化画面)から選んだカードのcardIDを受け取り表示
    public void ReceiveGetCard(int cardID)
    {
        CardController card = Instantiate(cardPrefab, getCard.transform);
        card.Init(cardID);
        card.SetCardState(CardController.CardState.Preview);
        changeCardID = cardID;
    }

    // デッキ一覧を表示
    public void DeckPreview()
    {
        for(int i = 0; i < deckManager.BaseDeck.Count; i++)
        {
            card[i] = Instantiate(cardPrefab, new Vector3(0, 0, 0), Quaternion.identity, cardPlace[i].transform);
            card[i].transform.localScale = new Vector3(0.7f, 0.7f, 0.7f);
            card[i].Init(deckManager.BaseDeck[i]);
            card[i].SetCardState(CardController.CardState.Preview);
        }
    }
}
