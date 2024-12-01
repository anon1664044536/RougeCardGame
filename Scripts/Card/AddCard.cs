using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class AddCard : CardItem
{
    public override void OnEndDrag(PointerEventData eventData)
    {
        if (TryUse() == true)
        {
            int val = int.Parse(data["Arg0"]);

            /*
                //如果抽牌堆不够抽的
                if (FightCardManager.Instance.CardCount() < val)
                {
                    //先把抽牌堆抽完
                    UIManager.Instance.GetUI<FightUI>("FightUI").CreateCardItem(FightCardManager.Instance.CardCount());

                    //把弃牌堆的牌返回抽牌堆
                    FightCardManager.Instance.InitByUsedCard();

                    //再抽剩下的
                    UIManager.Instance.GetUI<FightUI>("FightUI").CreateCardItem(val - FightCardManager.Instance.CardCount());

                    //更新弃牌卡堆数量
                    UIManager.Instance.GetUI<FightUI>("FightUI").UpdateUsedCardCount();

                }
                else
                {
                    UIManager.Instance.GetUI<FightUI>("FightUI").CreateCardItem(val);
                }
                */
            //抽卡
            FightCardManager.Instance.GetCard(val);

            //更新卡牌位置
            UIManager.Instance.GetUI<FightUI>("FightUI").UpdateCardItemPos();
            Vector3 pos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 2.5f));
            PlayEffect(pos);

            //更新卡牌数量
            UIManager.Instance.GetUI<FightUI>("FightUI").UpdateCardCount();
        }
        else
        {
            base.OnEndDrag(eventData);
        }
    }
}
