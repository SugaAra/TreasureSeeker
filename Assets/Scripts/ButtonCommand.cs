using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// commandItem, commandAttack, CommandSkillにアタッチ

public class ButtonCommand : MonoBehaviour
{
    private GameManager gameManager;
    private PlayerController player;

    private void Awake()
    {
        gameManager = GameObject.FindWithTag("GameController").GetComponent<GameManager>();
        player = GameObject.FindWithTag("Player").GetComponent<PlayerController>();
    }

    public void Push_Button(string button)
    {
        if (gameManager.CurrentState == GameManager.State.PlayerTurn)
        {
            switch (button)
            {
                // commandDeckCheckが押された
                case "left":
                    SoundManager.Instance.PlaySE(SESoundData.SE.Command);
                    gameManager.SetCommandFlag(true, false, false, false);
                    break;

                // commandAttackが押された
                case "center":
                    SoundManager.Instance.PlaySE(SESoundData.SE.Command);
                    gameManager.SetCommandFlag(false, true, false, false);
                    break;

                // commandSkillが押された
                case "right":
                    SoundManager.Instance.PlaySE(SESoundData.SE.Command);
                    gameManager.SetCommandFlag(false, false, true, false);
                    break;

                default:
                    break;
            }
        }
        // Skill選択画面も作る
        else if (gameManager.CurrentState == GameManager.State.PlayerSkill)
        {
            switch (button)
            {
                case "left":
                    // HPを50消費してATK3アップ
                    if(player.currentHP > 50)
                    {
                        Debug.Log("攻撃アップ");
                        SoundManager.Instance.PlaySE(SESoundData.SE.Boost);
                        player.DecreaseHPSkilled(50);
                        player.ATKUp(3);
                    }
                    else
                    {
                        Debug.Log("攻撃アップできないよ!");
                    }
                    break;
                case "center":
                    // MP25を消費してHPを20%回復
                    if(player.currentMP >= 25 & player.currentHP != player.currentBaseHP)
                    {
                        Debug.Log("回復");
                        SoundManager.Instance.PlaySE(SESoundData.SE.Cure);
                        player.DecreaseMP(25);
                        player.CureHP(player.currentBaseHP / 5);
                    }
                    else
                    {
                        Debug.Log("回復できないよ!");
                    }

                    break;
                case "right":
                    // MP40を消費してコンボ+3
                    if(player.currentMP >= 40)
                    {
                        Debug.Log("コンボアップ");
                        SoundManager.Instance.PlaySE(SESoundData.SE.Boost);
                        player.AddCombo(3);
                        player.DecreaseMP(40);
                    }
                    else
                    {
                        Debug.Log("コンボアップできないよ!");
                    }
                    break;
                case "close":
                    gameManager.SetCommandFlag(false, false, false, true);
                    break;
                default:
                    break;
            }
        }
    }
}
