using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

// ボタンのテキストを管理する
public class ButtonText : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI deckCheckText, attackText, skillText;

    public void ShowCommand()
    {
        ChangeFontSize(36);
        deckCheckText.text = "デッキ\n確認";
        attackText.text = "攻撃!!";
        skillText.text = "スキル";
    }

    public void ShowSkillCommand()
    {
        ChangeFontSize(28);
        deckCheckText.text = "ATK+3\nHP-50";
        attackText.text = "HP20%回復\nMP-25";
        skillText.text = "COMBO+3\nMP-40";
    }

    public void ChangeFontSize(int value)
    {
        deckCheckText.fontSize = value;
        attackText.fontSize = value;
        skillText.fontSize = value;
    }
}
