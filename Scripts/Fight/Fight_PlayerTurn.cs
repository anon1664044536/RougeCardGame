using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fight_PlayerTurn : FightUnit
{
    public override void Init()
    {
        Debug.Log("PlayerTime");
        UIManager.Instance.ShowTip("玩家回合", Color.green, delegate ()
        {

            //回复费用
            FightManager.Instance.CurPowerCount = FightManager.Instance.MaxPowerCount;
            UIManager.Instance.GetUI<FightUI>("FightUI").UpdatePower();

            //抽牌
            Debug.Log("抽牌");
            FightCardManager.Instance.GetCard(4);

            //更新卡牌位置
            UIManager.Instance.GetUI<FightUI>("FightUI").UpdateCardItemPos();

            //更新卡牌数
            UIManager.Instance.GetUI<FightUI>("FightUI").UpdateCardCount();
        });
    }

    public override void OnUpdata()
    {
        
    }
}
