using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// プレイヤーの各種ステータス管理と攻撃

public class PlayerController : MonoBehaviour
{
    [SerializeField] TextUIManager textUIManager;

    private PlayerModel model;
    private Transform enemyField;
    private EnemyController enemy;
    private GameManager gameManager;

    public int currentBaseHP, currentBaseMP, currentBaseATK, currentBaseDEF;    // 現在の最大HP,MP,ATK,DEF
    public int currentHP, currentMP, currentATK, currentDEF;                    // 現在のHP, MP, ATK, DEF

    public int currentAddCombo;                                                 // 追加コンボ

    public void Start()
    {
        gameManager = GameObject.FindWithTag("GameController").GetComponent<GameManager>();
        enemyField = GameObject.Find("Enemy1Field").GetComponent<Transform>();
        Init();
    }
    

    // プレイヤーの初期ステータスを設定・表示する
    public void Init()
    {
        model = new PlayerModel();

        currentBaseHP = model.baseHP;
        currentBaseMP = model.baseMP;
        currentBaseATK = model.baseATK;
        currentBaseDEF = model.baseDEF;

        currentHP = currentBaseHP;
        currentMP = currentBaseMP;
        currentATK = currentBaseATK;
        currentDEF = currentBaseDEF;

        textUIManager.ShowPlayerHP(currentBaseHP);
        textUIManager.ShowPlayerBaseHP(currentBaseHP);
        textUIManager.ShowPlayerMP(currentBaseMP);
        textUIManager.ShowPlayerBaseMP(currentBaseMP);
        textUIManager.ShowPlayerATK(currentATK);
        textUIManager.ShowPlayerDEF(currentDEF);
    }

    // 攻撃する(ダメージを与える)
    public IEnumerator Attack(int combo)
    {
        Debug.Log("攻撃");
        for (int i = currentATK; i < currentATK + combo + currentAddCombo; i++)
        {
            // 敵が倒れた後にすぐDestroyしないので少し間隔をあける
            yield return new WaitForSeconds(0.08f);
            enemy = enemyField.Find("Enemy(Clone)").GetComponent<EnemyController>();
            // ダメージを与える
            enemy.DecreaseHP(i);
            // 効果音を出す
            SoundManager.Instance.PlaySE(SESoundData.SE.Attack);
        }

        // コルーチンが終わったらgameManagerのisAttackCompleteをtrueにする
        gameManager.AttackComplete();
        gameManager.SetStateCheckTreasureGet();
        ResetAddCombo();
    }

    // ダメージを受ける
    public IEnumerator DecreaseHP()
    {
        yield return new WaitForSeconds(1.0f);

        enemy = enemyField.Find("Enemy(Clone)").GetComponent<EnemyController>();

        // 効果音を出す
        SoundManager.Instance.PlaySE(SESoundData.SE.Damage);

        for(int i = 0; i < enemy.enemyATKNum; i++)
        {
            yield return new WaitForSeconds(0.07f);

            if (currentDEF > 0)
            {
                currentDEF--;
                textUIManager.ShowPlayerDEF(currentDEF);
            }
            else
            {
                currentHP -= enemy.enemyATK;
                currentHP = System.Math.Max(currentHP, 0);

                // hptextを編集
                textUIManager.ShowPlayerHP(currentHP);
            }
        }
        // すべての攻撃を食らった後にバフをリセットする
        ResetATKDEF();

        // コルーチンが終わったらgameManagerのisAttackCompleteをtrueにする
        gameManager.AttackComplete();

        if(currentHP == 0)
        {
            gameManager.SetStateGameOver();
        }
    }

    // 攻撃力アップスキル使用でHPが減少する
    public void DecreaseHPSkilled(int damage)
    {
        currentHP -= damage;
        currentHP = System.Math.Max(currentHP, 0);

        textUIManager.ShowPlayerHP(currentHP);
    }

    // MPが減少する
    public void DecreaseMP(int damage)
    {
        currentMP -= damage;
        currentMP = System.Math.Max(currentMP, 0);

        // mptextを編集
        textUIManager.ShowPlayerMP(currentMP);
    }

    // HPを回復する
    public void CureHP(int cureHP)
    {
        currentHP += cureHP;
        currentHP = System.Math.Min(currentHP, currentBaseHP);

        // hptextを編集
        textUIManager.ShowPlayerHP(currentHP);
    }

    // MPを回復する
    public void CureMP(int cureMP)
    {
        currentMP += cureMP;
        currentMP = System.Math.Min(currentMP, currentBaseMP);

        // mptextを編集
        textUIManager.ShowPlayerMP(currentMP);
    }

    public void ATKUp(int atkUp)
    {
        currentATK += atkUp;
        textUIManager.ShowPlayerATK(currentATK);
    }

    public void DEFUp(int defUp)
    {
        currentDEF += defUp;
        textUIManager.ShowPlayerDEF(currentDEF);
    }

    // ATKを元に戻す
    public void ResetATKDEF()
    {
        currentATK = currentBaseATK;
        currentDEF = currentBaseDEF;

        textUIManager.ShowPlayerATK(currentATK);
        textUIManager.ShowPlayerDEF(currentDEF);
    }

    public void AddCombo(int value)
    {
        currentAddCombo += value;
        Debug.Log(currentAddCombo);
    }

    public void ResetAddCombo()
    {
        currentAddCombo = 0;
    }

    public void IncreaseBaseHP(int value)
    {
        currentBaseHP += value;
        currentHP += value;

        // hptext&defaulthpを編集
        textUIManager.ShowPlayerHP(currentHP);
        textUIManager.ShowPlayerBaseHP(currentBaseHP);

    }
    public void IncreaseBaseMP(int value)
    {
        currentBaseMP += value;
        currentMP += value;

        // mptext & defaultmpを編集
        textUIManager.ShowPlayerMP(currentMP);
        textUIManager.ShowPlayerBaseMP(currentBaseMP);
    }
    public void IncreaseBaseATK(int value)
    {
        currentATK += value;
        currentBaseATK += value;

        // ATKtextを編集
        textUIManager.ShowPlayerATK(currentATK);
    }
}
