using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

// カードの見た目を管理
public class CardView : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI enchantText, detailText;
    [SerializeField] Image enchantIconImage, cardColorImage;

    public void Show(CardModel cardModel)
    {
        enchantText.text = cardModel.enchant;
        detailText.text = cardModel.detail;
        enchantIconImage.sprite = cardModel.enchantIcon;
        cardColorImage.sprite = cardModel.cardColor;
    }
}
