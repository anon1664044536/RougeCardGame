using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class JumpCard : CardItem
{
    public override void OnEndDrag(PointerEventData eventData)
    {
        if (TryUse() == true)
        {
            int val = int.Parse(data["Arg0"]);

            Vector3 pos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 2.5f));
            PlayEffect(pos);

            //刷新敌人攻击意图
            EnemyManager.Instance.ReSetAllEnemy();

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

        }
        else
        {
            base.OnEndDrag(eventData);
        }
    }
}
