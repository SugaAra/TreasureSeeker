using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] public AttackField attackField;            // カードをドロップする場所
    [SerializeField] ButtonText buttontext;                     // ボタンに表示するテキスト
    [SerializeField] DeckManager deck;                          // 戦闘で使うデッキ
    [SerializeField] NextHandController nextHand;               // 次のカード表示場
    [SerializeField] PlayerHandController playerHand1, playerHand2, playerHand3;                        // 手札置き場
    [SerializeField] GameObject menuPanel, commandItem, commandAttack, commandSkill, commandClose;      // メニューの種類表示, コマンドボタン
    [SerializeField] GameObject deckCheckPanel;                 // デッキ一覧を確認できるポップアップ
    [SerializeField] PlayerController playerController;         // プレイヤーのあれこれ
    [SerializeField] FloorEnemyManager enemylist;               // 敵のリスト
    [SerializeField] EnemyEmerge enemyEmerge;                   // 敵を出すところ
    [SerializeField] EnchantManager enhanceManager;             // エンチャントを管理する
    [SerializeField] GameObject treasureSelect;                 // 宝(強化カード)選択
    [SerializeField] Slider timeSlider;                         // 制限時間

    // 各種フラグ(この辺は要検討)
    public bool isItem = false;
    public bool isAttack = false;
    public bool isSkill = false;
    public bool isClose = false;

    public bool isTreasureGet = false;      // 宝を手に入れたかどうか
    private bool isNewFloor;                // 新フロアかどうか
    private bool isAttackComplete;          // 攻撃が終わったか

    private float elapsedTime = 0;
    private float attackTime = 0;

    private int nowFloor;         // 現在のフロア
    private int goalFloor = 15;     // ゴールの階層

    // 戦闘時の状態
    public enum State
    {
        None,
        NewFloor,           // フロアの情報を読み込む
        PlayerTurn,         // プレイヤーのターン、コマンド選択画面
        DeckCheck,          // デッキ確認状態
        PlayerSkill,        // スキル選択画面
        PlayerAttack,       // 戦闘画面
        AttackResult,       // 戦闘後処理
        AttackStay,         // 攻撃が終わるのを待つ
        CheckTreasureGet,   // 宝物かどうかを判定する
        EnemyTurn,          // 敵の攻撃
        GameClearCheck,     // ゲームクリアかどうか判定する
        TreasureSelect,     // 宝物(強化)選択
        GameOver,           // ゲームオーバー
        GameClear,          // ゲームクリア
    }

    public State CurrentState { get; private set; } = State.None;

    void Start()
    {
        SetCommandFlag(false, false, false, false);
        isAttackComplete = true;
        isNewFloor = true;
        nowFloor = 0;
        CurrentState = State.NewFloor;
        SoundManager.Instance.PlayBGM(BGMSoundData.BGM.Dungeon);
    }

    private void Update()
    {
        switch(CurrentState)
        {
            case State.NewFloor:        // フロアの情報を読み込む
                if (isNewFloor)
                {
                    //Debug.Log("新フロア!");
                    isNewFloor = false;
                    nowFloor++;
                    enemylist.NewFloor();       // 新しいフロアに出る敵のリストを取得
                    NextEnemy();                // 敵を出現させる
                    CurrentState = State.PlayerTurn;
                }
                break;

            case State.PlayerTurn:      // コマンド選択画面(プレイヤーのターン)
                if (!isAttackComplete)
                {
                    break;
                }
                else
                {
                    // プレイヤーのHPが0になっていたらでゲームオーバー
                    if (playerController.currentHP == 0)
                    {
                        CurrentState = State.GameOver;
                    }

                    // コマンド選択画面
                    enhanceManager.ResetEnhance();

                    attackField.ResetCombo();
                    attackField.ClearColor();

                    buttontext.ShowCommand();
                    CommandSetActive(false, true, true, true, false);
                    HandDestroy();

                    // 押されたボタンに応じて状態を変化させる
                    if (isItem)
                    {
                        CurrentState = State.DeckCheck;
                    }
                    else if (isAttack)
                    {
                        CurrentState = State.PlayerAttack;
                    }
                    else if (isSkill)
                    {
                        CurrentState = State.PlayerSkill;
                    }
                }
                break;

            case State.DeckCheck:      // デッキ確認画面
                deckCheckPanel.SetActive(true);

                CommandSetActive(false, true, true, true, false);

                // 戻るボタンでplayerTurn
                if (isClose)
                {
                    SetCommandFlag(false, false, false, false);
                    CurrentState = State.PlayerTurn;
                }
                break;

            case State.PlayerSkill:         // スキル選択画面

                buttontext.ShowSkillCommand();
                CommandSetActive(true, true, true, true, true);

                // 戻るボタン押下でplayerTurn
                if (isClose)
                {
                    SetCommandFlag(false, false, false, false);
                    CurrentState = State.PlayerTurn;
                }
                break;

            case State.PlayerAttack:        // 攻撃画面

                CommandSetActive(false, false, false, false, false);

                if (isAttack)
                {
                    // デッキのシャッフルとattackFieldのリセット→攻撃開始
                    isAttack = false;
                    deck.Shuffle();
                    attackField.SetFieldColor();
                    StartAttack();
                }

                // 8秒間攻撃可能,その後attackresultへ
                attackTime += Time.deltaTime;
                timeSlider.value = attackTime;

                if (attackTime > 8)
                {
                    attackTime = 0;
                    CurrentState = State.AttackResult;
                }

                break;

            // 攻撃終了後の処理
            case State.AttackResult:
                CommandSetActive(false, false, false, false, false);

                // 場のリセット
                HandDestroy();              // 残ったカードを消す               
                attackField.ClearColor();   // attackFieldの色も消す

                // コンボ数を受け取り敵に攻撃する
                int combo = attackField.GetCombo();

                // コンボ数分のMP回復
                playerController.CureMP(combo);

                // エンチャント発動
                enhanceManager.UseEnhance();

                // コンボ数分攻撃を行う
                StartCoroutine(playerController.Attack(combo));

                if (combo == 0)
                {
                    CurrentState = State.CheckTreasureGet;
                }
                else
                {
                    CurrentState = State.AttackStay;
                }
                break;

            case State.AttackStay:
                // StartCoroutine(player.Controller.Attack(combo)が終われば
                // CurrentStateがCheckTreasureGetになる
                break;
            // 敵か宝箱かの判定
            case State.CheckTreasureGet:
                if(isTreasureGet)
                {
                    CurrentState = State.GameClearCheck;
                }
                else
                {
                    CurrentState = State.EnemyTurn;
                }
                break;
            // 敵の攻撃ターン
            case State.EnemyTurn:

                isAttackComplete = false;

                // 相手の攻撃を受けてプレイヤーの体力が減る
                StartCoroutine(playerController.DecreaseHP());

                // プレイヤーのターンへ戻る
                CurrentState = State.PlayerTurn;
                break;

            // ゲームをクリアしたかどうか
            case State.GameClearCheck:
                if (!isAttackComplete)
                {
                    // playerController.DecreaseHP()が終わらないうちは何もしない
                    Debug.Log("treasurestay");
                    break;
                }
                else
                {
                    Debug.Log("お宝選択!");

                    isTreasureGet = false;

                    elapsedTime += Time.deltaTime;
                    if (elapsedTime > 2)
                    {
                        // 最後のフロアだった時
                        if (nowFloor == goalFloor)
                        {
                            elapsedTime = 0;
                            CurrentState = State.GameClear;     // ゲームクリア
                        }
                        else
                        {
                            elapsedTime = 0;
                            // 宝選択画面を開き、その裏でバフと敵を消す
                            isNewFloor = true;
                            treasureSelect.SetActive(true);
                            playerController.ResetATKDEF();
                            enemyEmerge.EnemyDestroy();
                            CurrentState = State.TreasureSelect;        // 宝選択画面へ
                        }
                    }
                }
                break;

            case State.TreasureSelect:
                // 宝(強化)選択状態
                // 宝が選び終わったらNextFloor()が呼ばれ、state.NewFloorになる
                break;
            case State.GameOver:
                elapsedTime += Time.deltaTime;
                // 間をおいてゲームオーバー画面へ
                if (elapsedTime > 2)
                {
                    elapsedTime = 0;
                    SceneManager.LoadScene("GameOver", LoadSceneMode.Single);
                }
                break;

            case State.GameClear:
                // 間をおいてゲームクリア画面へ
                elapsedTime += Time.deltaTime;
                if (elapsedTime > 2)
                {
                    elapsedTime = 0;
                    SceneManager.LoadScene("GameClear", LoadSceneMode.Single);
                }
                break;
            case State.None:
            default:
                break;
        }
    }

    // 攻撃開始(カードをセットする)
    void StartAttack()
    {
        DrawCard(playerHand1);
        DrawCard(playerHand2);
        DrawCard(playerHand3);

        NextCard();
    }

    // カードを引く
    public void DrawCard(PlayerHandController hand)
    {
        hand.DrawCard(deck);
    }

    // 次のカードを表示
    public void NextCard()
    {
        nextHand.NextCard(deck);
    }

    // 次の敵を出現させる
    public void NextEnemy()
    {
        enemyEmerge.NextEnemy(enemylist);
    }

    // 攻撃の処理が終わったらCurrentStateをCheckTreasureGetにする
    public void SetStateCheckTreasureGet()
    {
        CurrentState = State.CheckTreasureGet;
    }

    // 宝を選択し終わったらCurrentStateをNewFloorにする
    public void SetStateNewFloor()
    {
        CurrentState = State.NewFloor;
    }

    public void SetStateGameOver()
    {
        CurrentState = State.GameOver;
    }

    // 攻撃(playerController.Attack(combo))が終了したか
    public void AttackComplete()
    {
        isAttackComplete = true;
    }

    // 戦闘コマンドを一括有効化
    private void CommandSetActive(bool menu, bool pH1, bool pH2, bool pH3, bool close)
    {
        menuPanel.SetActive(menu);
        commandItem.SetActive(pH1);
        commandAttack.SetActive(pH2);
        commandSkill.SetActive(pH3);
        commandClose.SetActive(close);
    }

    // 押されたボタンに応じてフラグをTRUEにする
    public void SetCommandFlag(bool flagItem, bool flagAttack, bool flagSkill, bool flagClose)
    {
        isItem = flagItem;
        isAttack = flagAttack;
        isSkill = flagSkill;
        isClose = flagClose;
    }

    // 残ったカードをまとめてを消す
    private void HandDestroy()
    {
        playerHand1.CardDestroy();      // 手札1に残ったカード削除
        playerHand2.CardDestroy();      // 手札2に
        playerHand3.CardDestroy();      // 手札3に
        nextHand.CardDestroy();        // 山札に
    }
}
