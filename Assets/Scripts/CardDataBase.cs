using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardDataBase : ScriptableObject
{
    public CardData[] datas;
}

[System.Serializable]
public class CardData
{
    public string name;
    public int cardID;
    public int enchantID;
    public int enchantValue;    // 効果量
    public bool isRed;
    public bool isBlue;
    public bool isYellow;
    public string enchant;
    public string detail;
    public Sprite enchantIcon;
    public Sprite cardColor;
}
