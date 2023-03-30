using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 強化を選択する
public class TreasureSelect : MonoBehaviour
{
    [SerializeField] GameObject previewLeft;            // 基礎ステータス上昇系のみ出現
    [SerializeField] GameObject previewCenter;          // 効果量が少ないエンチャント付きカードのみ出現
    [SerializeField] GameObject previewRight;           // すべてのエンチャントカード付きから出現
    [SerializeField] CardController cardPrefab;
    [SerializeField] GameObject editDeckPanel;

    private GameManager gameManager;
    //private DeckManager deckManager;
    private CardController cardLeft, cardCenter, cardRight;
    private PlayerController player;
    private EditDeckPanel editDeck;

    private int randLeft, randCenter, randRight;

    // アクティブになった時に実行
    private void OnEnable()
    {
        player = GameObject.FindWithTag("Player").GetComponent<PlayerController>();
        editDeck = editDeckPanel.GetComponent<EditDeckPanel>();
        Init();
    }

    private void Start()
    {
        gameManager = GameObject.FindWithTag("GameController").GetComponent<GameManager>();
    }

    public void Init()
    {
        // ランダムなcardIDを取得
        UnityEngine.Random.InitState(DateTime.Now.Millisecond);
        randLeft = UnityEngine.Random.Range(96, 104);
        UnityEngine.Random.InitState(DateTime.Now.Millisecond);
        randCenter = UnityEngine.Random.Range(6, 36);
        UnityEngine.Random.InitState(DateTime.Now.Second);
        randRight = UnityEngine.Random.Range(6, 96);

        // cardIDに応じたカードを表示
        cardLeft = Instantiate(cardPrefab, new Vector3(0, 0, 0), Quaternion.identity, previewLeft.transform);
        cardLeft.transform.localScale = new Vector3(1.5f, 1.5f, 1.5f);
        cardLeft.Init(randLeft);
        cardLeft.SetCardState(CardController.CardState.Preview);

        cardCenter = Instantiate(cardPrefab, new Vector3(0, 0, 0), Quaternion.identity, previewCenter.transform);
        cardCenter.transform.localScale = new Vector3(1.5f, 1.5f, 1.5f);
        cardCenter.Init(randCenter);
        cardCenter.SetCardState(CardController.CardState.Preview);

        cardRight = Instantiate(cardPrefab, new Vector3(0, 0, 0), Quaternion.identity, previewRight.transform);
        cardRight.transform.localScale = new Vector3(1.5f, 1.5f, 1.5f);
        cardRight.Init(randRight);
        cardRight.SetCardState(CardController.CardState.Preview);
    }

    public void Push_Button(string button)
    {
        switch (button)
        {
            // 左のカードが選択された
            case "left":
                ReflectEffect();
                break;

            // 中央のカードが選択された
            case "center":
                editDeckPanel.SetActive(true);
                editDeck.ReceiveGetCard(cardCenter.model.cardID);
                break;

            // 右のカードが押された
            case "right":
                editDeckPanel.SetActive(true);
                editDeck.ReceiveGetCard(cardRight.model.cardID);
                break;

            default:
                break;
        }
    }

    public void ReflectEffect()
    {
        int enchantID = cardLeft.model.enchantID;
        switch(enchantID)
        {
            // 10なら最大HPUP
            case 10:
                Debug.Log("HP");
                player.IncreaseBaseHP(cardLeft.model.enchantValue);
                break;
            // 11なら最大MPUP
            case 11:
                Debug.Log("MP");
                player.IncreaseBaseMP(cardLeft.model.enchantValue);
                break;
            // 12なら基礎攻撃力UP
            case 12:
                Debug.Log("ATK");
                player.IncreaseBaseATK(cardLeft.model.enchantValue);
                break;
            default:
                break;
        }
    }

    // 非アクティブになったときに実行
    // TreasureSelectPanel内に生成されていたカードをすべて削除
    private void OnDisable()
    {
        GameObject.Destroy(previewLeft.transform.GetChild(0).gameObject);
        GameObject.Destroy(previewCenter.transform.GetChild(0).gameObject);
        GameObject.Destroy(previewRight.transform.GetChild(0).gameObject);

        gameManager.SetStateNewFloor();
    }
}
