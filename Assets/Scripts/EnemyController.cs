using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

// 敵を管理する
public class EnemyController : MonoBehaviour
{
    public EnemyView view;  // 敵の見た目の処理
    public EnemyModel model;    // 敵のデータを処理

    public int enemyHP, enemyATK, enemyATKNum, enemyDEF;        // 敵の持っているステータス

    private GameManager gameManager;
    private TextUIManager textUIManager;
    private PlayerController playerController;

    private void Awake()
    {
        view = GetComponent<EnemyView>();
        gameManager = GameObject.FindWithTag("GameController").GetComponent<GameManager>();
        textUIManager = GameObject.FindWithTag("TextUIManager").GetComponent<TextUIManager>();
        playerController = GameObject.FindWithTag("Player").GetComponent<PlayerController>();
    }

    public void Init(int enemyID)
    {
        if(enemyID == 0)
        {
            textUIManager.ShowTreasure();
            gameManager.isTreasureGet = true;
        }

        model = new EnemyModel(enemyID);
        view.Show(model);

        // ステータスをセット
        enemyHP = model.enemyHP;
        enemyATK = model.enemyATK;
        enemyATKNum = model.enemyATKNum;
        enemyDEF = model.enemyDEF;

        if (!model.isTreasure)
        {
            textUIManager.ShowEnemyHP(enemyHP);
            textUIManager.ShowEnemyATK(enemyATK);
            textUIManager.ShowEnemyATKNum(enemyATKNum);
            textUIManager.ShowEnemyDEF(enemyDEF);
        }
        /*
        else if(model.isTreasure)
        {
            textUIManager.ShowTreasure();
            Debug.Log("ここまでは来てる");
            gameManager.isTreasureGet = true;
        }
        */
    }

    // ダメージを受ける
    public void DecreaseHP(int damage)
    {
        // DEFがあるならダメージ無効
        if (enemyDEF > 0)
        {
            //Debug.Log("ガード");
            enemyDEF--;
            textUIManager.ShowEnemyDEF(enemyDEF);
        }
        else
        {
            //Debug.Log("ダメージ!");
            enemyHP -= damage;
            enemyHP = System.Math.Max(enemyHP, 0);

            if (!model.isTreasure)
            {
                textUIManager.ShowEnemyHP(enemyHP);
            }

            if (enemyHP <= 0)
            {
                //Debug.Log("しんだ");
                // 倒されたら次の敵を出現させ、倒れた敵は消す
                gameManager.NextEnemy();
                Destroy(this.gameObject);
            }
        }
    }
}
