using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnchantManager : MonoBehaviour
{
    List<int> nowEnchant = new List<int>() { };    // 場に出されたカード

    private PlayerController playerController;
    private CardModel cardModel;

    public void Start()
    {
        playerController = GameObject.FindWithTag("Player").GetComponent<PlayerController>();
    }

    // エンハンスをカウントする
    public void AddEnhance(int cardID)
    {
        nowEnchant.Add(cardID);
    }

    // cardIDに対応する効果を発動する
    public void UseEnhance()
    {
        for(int i = 0; i < nowEnchant.Count; i++)
        {
            cardModel = new CardModel(nowEnchant[i]);

            switch(cardModel.enchantID)
            {
                // 1ならガード(DEFアップ)
                case 1:
                    Debug.Log("ガード");
                    playerController.DEFUp(cardModel.enchantValue);
                    break;

                // 2なら攻撃アップ
                case 2:
                    Debug.Log("ATK");
                    playerController.ATKUp(cardModel.enchantValue);
                    break;

                // 3ならHP回復
                case 3:
                    Debug.Log("HP");
                    playerController.CureHP(cardModel.enchantValue);
                    break;
                // 4ならMP回復
                case 4:
                    Debug.Log("MP");
                    playerController.CureMP(cardModel.enchantValue);
                    break;
                case 5:
                    Debug.Log("COMBO");
                    playerController.AddCombo(cardModel.enchantValue);
                    break;
                // 0なら何もしない
                case 0:
                default:
                    break;
            }
        }
    }

    // エンチャントをリセットする
    public void ResetEnhance()
    {
        nowEnchant.Clear();
    }
}
