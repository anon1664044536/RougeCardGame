using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class FightCardManager
{
    public static FightCardManager Instance = new FightCardManager();

    public List<string> cardList; //卡堆集合

    public List<string> usedCardList; //弃牌堆

    public List<string> EnableCardList; //战斗中能够使用的卡牌 

    public void Init()
    {
        cardList = new List<string>();  
        usedCardList = new List<string>();
        EnableCardList = new List<string>();

        List<string> tempList = new List<string>();
        tempList.AddRange(Rolemanager.Instanse.cardList);

        while(tempList.Count > 0)
        {
            int tempIndex = Random.Range(0, tempList.Count);
            cardList.Add(tempList[tempIndex]);
            EnableCardList.Add(tempList[tempIndex]);

            //从临时集合删除这张牌
            tempList.RemoveAt(tempIndex);
        }

        Debug.Log(cardList.Count);
    }

    //将弃牌堆的卡牌返回抽牌堆
    public void InitByUsedCard()
    {
        cardList = new List<string>();

        List<string> tempList = new List<string>();
        tempList.AddRange(usedCardList);

        while (tempList.Count > 0)
        {
            int tempIndex = Random.Range(0, tempList.Count);
            cardList.Add(tempList[tempIndex]);

            //从临时集合删除这张牌
            tempList.RemoveAt(tempIndex);
        }

        usedCardList = new List<string>();
    }

    //返回卡牌数量
    public int CardCount()
    {
        return cardList.Count;
    }

    //是否有卡
    public bool Hascard()
    {
        return cardList.Count > 0;
    }

    //抽卡（卡牌模型的抽卡）
    public string DrawCard()
    {
        string id = cardList[cardList.Count - 1];
        cardList.RemoveAt(cardList.Count - 1);
        return id;
    }

    //从抽牌堆里面抽卡
    public void GetCard(int count)
    {
        //如果抽牌堆不够抽的
        if (cardList.Count < count)
        {
            //先把抽牌堆抽完
            int num = cardList.Count;
            UIManager.Instance.GetUI<FightUI>("FightUI").CreateCardItem(cardList.Count);
            int tempCount = count - num;
            Debug.Log("第一次抽的牌的数量为"+ num.ToString());

            //把弃牌堆的牌返回抽牌堆
            InitByUsedCard();

            //再抽剩下的
            UIManager.Instance.GetUI<FightUI>("FightUI").CreateCardItem(tempCount);
            Debug.Log("第二次抽的牌的数量为" + tempCount.ToString());

            //更新弃牌卡堆数量
            UIManager.Instance.GetUI<FightUI>("FightUI").UpdateUsedCardCount();

        }
        else
        {
            UIManager.Instance.GetUI<FightUI>("FightUI").CreateCardItem(count);
        }
    }
}
