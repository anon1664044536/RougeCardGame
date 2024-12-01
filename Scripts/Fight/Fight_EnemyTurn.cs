using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fight_EnemyTurn : FightUnit
{
    public override void Init()
    {
        //删除所有卡牌
        UIManager.Instance.GetUI<FightUI>("FightUI").RemoveAllCards();
        //显示敌人回合提示
        UIManager.Instance.ShowTip("敌人回合", Color.red, delegate ()
        {
            FightManager.Instance.StartCoroutine(EnemyManager.Instance.DoAllEnemyAction());
        });
    }

    public override void OnUpdata()
    {
        
    }
}
