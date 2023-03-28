using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerModel
{
    public int baseHP;          // 基礎HP
    public int baseMP;          // 基礎MP
    public int baseATK;         // 基礎ATK
    public int baseDEF;         // 基礎DEF

    public int hpUpValue;       // HP上昇量
    public int mpUpValue;       // MP上昇量
    public int atkUpValue;      // ATK上昇量

    public int hpUpTimes;       // HP強化回数
    public int mpUpTimes;       // MP強化回数
    public int atkUpTimes;      // ATK強化回数

    public int currentHP;
    public int currentMP;
    public int currentATK;
    public int currentDEF;


    public PlayerModel()
    {
        PlayerEntity playerStatus = Resources.Load<PlayerEntity>("PlayerBaseStatus");

        baseHP = playerStatus.baseHP;
        baseMP = playerStatus.baseMP;
        baseATK = playerStatus.baseATK;
        baseDEF = playerStatus.baseDEF;

        hpUpValue = playerStatus.hpUpValue;
        mpUpValue = playerStatus.mpUpValue;
        atkUpValue = playerStatus.atkUpValue;

        hpUpTimes = playerStatus.hpUpTimes;
        mpUpTimes = playerStatus.mpUpTimes;
        atkUpTimes = playerStatus.atkUpTimes;

        currentHP = baseHP;
        currentMP = baseMP;
        currentATK = baseATK;
        currentDEF = baseDEF;
    }
}
