using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// カード全体の管理
public class CardController : MonoBehaviour
{
    public CardView view;
    public CardModel model;
    public CardMovement movement;

    private GameManager gameManager;

    private bool canMove = false;

    public enum CardState
    {
        None,
        Attack,     // 戦闘時
        Preview,    // 表示する
    }

    public CardState CurrentCardState { get; private set; } = CardState.None;

    private void Awake()
    {
        view = GetComponent<CardView>();
        movement = GetComponent<CardMovement>();
        gameManager = GameObject.FindWithTag("GameController").GetComponent<GameManager>();
    }

    private void Update()
    {
        switch(CurrentCardState)
        {
            case CardState.Attack:
                canMove = CanMove();

                if (!canMove)
                {
                    movement.enabled = false;
                }
                else
                {
                    movement.enabled = true;
                }
                break;

            case CardState.Preview:
                movement.enabled = false;
                break;

            case CardState.None:
            default:
                break;
        }
    }

    // カードの状態を変える(戦闘時or表示)
    public void SetCardState(CardState cardstate)
    {
        this.CurrentCardState = cardstate;
    }

    // カードを生成するときに呼ばれる
    public void Init(int cardID)
    {
        model = new CardModel(cardID);  // カードデータ生成
        view.Show(model);   // 表示
    }

    // カードを消す
    public void DestoryCard(CardController card)
    {
        Destroy(card.gameObject);
    }

    // カードが動かないようにする
    public void StopCard()
    {
        movement.enabled = false;
    }

    // カードの色とAttackFieldの色(次に置ける色)を比較して動かせるか判定
    public bool CanMove()
    {
        AttackField dropPlace = gameManager.attackField;
        string cardColor = dropPlace.NextColor();

        switch (cardColor)
        {
            case "Red":
                if (!model.isRed)
                {
                    return false;
                    
                }
                else
                {
                    return true;
                }
            case "Blue":
                if (!model.isBlue)
                {
                    return false;

                }
                else
                {
                    return true;
                }
            case "Yellow":
                if (!model.isYellow)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            case "R&B":
                if (!model.isRed && !model.isBlue)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            case "R&Y":
                if (!model.isRed && !model.isYellow)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            case "B&Y":
                if (!model.isBlue && !model.isYellow)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            case "ALL":
                return true;
            default:
                return false;
        }
    }

}
