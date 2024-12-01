using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FightInit : FightUnit
{
    public override void Init()
    {
        //初始化战斗数值
        FightManager.Instance.Init();

        //初始化玩家状态
        ConditionManager.Instanse.Init();

        //切换bgm
        AudioManager.Instance.PlayBGM("battle");

        //显示战斗画面
        UIManager.Instance.ShowUI<FightUI>("FightUI");

        //初始化战斗卡牌
        FightCardManager.Instance.Init();

        //敌人生成
        EnemyManager.Instance.LoadRes("10003");

        //切换到玩家回合
        FightManager.Instance.ChangeType(FightType.Player);
    }

    public override void OnUpdata()
    {
        base.OnUpdata();
    }
}
