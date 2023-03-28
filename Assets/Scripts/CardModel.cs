using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

// カードのデータを管理
public class CardModel
{
    public string name;
    public int cardID;
    public int enchantID;
    public int enchantValue;
    public bool isRed;
    public bool isBlue;
    public bool isYellow;
    public string enchant;
    public string detail;
    public Sprite enchantIcon;
    public Sprite cardColor;

    public CardModel(int CardID)
    {
        CardDataBase cardEntity = Resources.Load<CardDataBase>("CardDataBase");

        name = cardEntity.datas[CardID].name;
        cardID = cardEntity.datas[CardID].cardID;
        enchantID = cardEntity.datas[CardID].enchantID;
        enchantValue = cardEntity.datas[CardID].enchantValue;
        isRed = cardEntity.datas[CardID].isRed;
        isBlue = cardEntity.datas[CardID].isBlue;
        isYellow = cardEntity.datas[CardID].isYellow;
        enchant = cardEntity.datas[CardID].enchant;
        detail = cardEntity.datas[CardID].detail;
        enchantIcon = cardEntity.datas[CardID].enchantIcon;
        cardColor = cardEntity.datas[CardID].cardColor;
    }

}
