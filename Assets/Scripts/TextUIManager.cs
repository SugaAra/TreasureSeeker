using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TextUIManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI nowFloor;

    [SerializeField] TextMeshProUGUI playerHP, defaultPlayerHP;     // playerのHP
    [SerializeField] TextMeshProUGUI playerMP, defaultPlayerMP;     // playerのMP
    [SerializeField] TextMeshProUGUI playerATK;                     // playerのATK
    [SerializeField] TextMeshProUGUI playerDEF;                     // playerのDEF

    [SerializeField] TextMeshProUGUI enemyHP, enemyATK, enemyATKNum, enemyDEF;      // 敵のステータス

    public void ShowFloor(int floor)
    {
        nowFloor.text = "B" + floor.ToString() + "F";
    }

    // 現在のHPを表示
    public void ShowPlayerHP(int hp)
    {
        playerHP.text = hp.ToString();
    }

    // 現在の最大HPを表示
    public void ShowPlayerBaseHP(int basehp)
    {
        defaultPlayerHP.text = basehp.ToString();
    }

    // 現在のMPを表示
    public void ShowPlayerMP(int mp)
    {
        playerMP.text = mp.ToString();
    }

    // 現在の最大HPを表示
    public void ShowPlayerBaseMP(int basemp)
    {
        defaultPlayerMP.text = basemp.ToString();
    }

    // 現在のATKを表示
    public void ShowPlayerATK(int atk)
    {
        playerATK.text = atk.ToString();
    }

    // 現在のDEFを表示
    public void ShowPlayerDEF(int def)
    {
        playerDEF.text = def.ToString();
    }

    // 敵のHPを表示
    public void ShowEnemyHP(int hp)
    {
        enemyHP.text = hp.ToString();
    }
    // 敵のATKを表示
    public void ShowEnemyATK(int atk)
    {
        enemyATK.text = atk.ToString();
    }

    // 敵の攻撃回数を表示
    public void ShowEnemyATKNum(int atkNum)
    {
        enemyATKNum.text = atkNum.ToString();
    }

    // 敵の防御力を表示
    public void ShowEnemyDEF(int def)
    {
        enemyDEF.text = def.ToString();
    }


    // 宝用のテキストを表示
    public void ShowTreasure()
    {
        enemyHP.text = "???";
        enemyATK.text = "???";
        enemyATKNum.text = "?";
        enemyDEF.text = "???";
    }

}