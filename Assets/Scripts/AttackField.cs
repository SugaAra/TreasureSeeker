using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

// カードをドロップする場所
public class AttackField : MonoBehaviour, IDropHandler
{
    public Image image;
    private Sprite sprite;
    private int combo;
    private GameManager gameManager;
    private EnchantManager enhanceManager;

    [Flags]
    public enum FieldColor
    {
        Red = 1 << 0,
        Blue = 1 << 1,
        Yellow = 1 << 2,
    }

    public FieldColor color { get; private set; }

    private void Awake()
    {
        gameManager = GameObject.FindWithTag("GameController").GetComponent<GameManager>();
        enhanceManager = GameObject.FindWithTag("EnhanceManager").GetComponent<EnchantManager>();
    }

    // 全色置けるようにする
    public void SetFieldColor()
    {
        color = FieldColor.Red | FieldColor.Blue | FieldColor.Yellow;
    }

    public void OnDrop(PointerEventData eventData)  // ドロップされた時の処理
    {
        CardController card = eventData.pointerDrag.GetComponent<CardController>();
        PlayerHandController hand = card.movement.cardParent.GetComponent<PlayerHandController>();

        if (card != null)
        {
            // ドロップ後にカードを配る & 次のカード表示
            gameManager.DrawCard(hand);
            gameManager.NextCard();
            
            card.movement.cardParent = this.transform;   // カードの親要素を自分にする

            // AttackFieldにカードを置いたら色が反映される
            if (card.model.isRed && card.model.isBlue)
            {
                sprite = Resources.Load<Sprite>("CardImage/CardRB");
                image = this.GetComponent<Image>();
                image.sprite = sprite;

                SetFieldColor();
                color &= ~FieldColor.Yellow;
            }
            else if (card.model.isRed && card.model.isYellow)
            {
                sprite = Resources.Load<Sprite>("CardImage/CardRY");
                image = this.GetComponent<Image>();
                image.sprite = sprite;

                SetFieldColor();
                color &= ~FieldColor.Blue;
            }
            else if (card.model.isBlue && card.model.isYellow)
            {
                sprite = Resources.Load<Sprite>("CardImage/CardBY");
                image = this.GetComponent<Image>();
                image.sprite = sprite;

                SetFieldColor();
                color &= ~FieldColor.Red;
            }
            else if (card.model.isRed)
            {
                sprite = Resources.Load<Sprite>("CardImage/CardRed");
                image = this.GetComponent<Image>();
                image.sprite = sprite;

                SetFieldColor();
                color &= ~FieldColor.Blue;
                color &= ~FieldColor.Yellow;
            }
            else if (card.model.isBlue)
            {
                sprite = Resources.Load<Sprite>("CardImage/CardBlue");
                image = this.GetComponent<Image>();
                image.sprite = sprite;

                SetFieldColor();
                color &= ~FieldColor.Red;
                color &= ~FieldColor.Yellow;
            }
            else if (card.model.isYellow)
            {
                sprite = Resources.Load<Sprite>("CardImage/CardYellow");
                image = this.GetComponent<Image>();
                image.sprite = sprite;

                SetFieldColor();
                color &= ~FieldColor.Red;
                color &= ~FieldColor.Blue;
            }

            // コンボ加算
            combo++;
            // 出したカードを記憶しておく
            enhanceManager.AddEnhance(card.model.cardID);
            // 効果音を出す
            SoundManager.Instance.PlaySE(SESoundData.SE.Card);
            // カードを消去する
            card.DestoryCard(card);
        }
    }


    public string NextColor()
    {
        if (color.HasFlag(FieldColor.Red | FieldColor.Blue | FieldColor.Yellow))
        {
            return "ALL";
        }
        else if (color.HasFlag(FieldColor.Red | FieldColor.Blue))
        {
            return "R&B";
        }
        else if (color.HasFlag(FieldColor.Red | FieldColor.Yellow))
        {
            return "R&Y";
        }
        else if (color.HasFlag(FieldColor.Blue | FieldColor.Yellow))
        {
            return "B&Y";
        }
        else if (color.HasFlag(FieldColor.Red))
        {
            return "Red";
        }
        else if (color.HasFlag(FieldColor.Blue))
        {
            return "Blue";
        }
        else // if(color.HasFlag(FieldColor.Yellow))
        {
            return "Yellow";
        }

    }

    // コンボの初期化
    public void ResetCombo()
    {
        combo = 0;
    }

    // comboを渡す
    public int GetCombo()
    {
        return combo;
    }

    // AttackFieldの色をリセット
    public void ClearColor()
    {
        // グレーの画像を差し込む
        sprite = Resources.Load<Sprite>("CardImage/CardGray");
        image = this.GetComponent<Image>();
        image.sprite = sprite;
    }
}
