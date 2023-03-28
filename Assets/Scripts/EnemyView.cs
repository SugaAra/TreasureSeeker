using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// 敵の見た目を管理
public class EnemyView : MonoBehaviour
{
    [SerializeField] Image enemyIconImage;

    public void Show(EnemyModel enemyModel)
    {
        enemyIconImage.sprite = enemyModel.enemyIcon;
    }
}
