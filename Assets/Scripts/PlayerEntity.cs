using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
// プレイヤーのデータ
public class PlayerEntity : ScriptableObject
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
}
