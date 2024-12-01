using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class FightUI : UIBase
{
    private Text cardCountTxt; //卡牌数量
    private Text usedCardCountTxt; //弃牌堆数量
    private Text powerTxt;
    private Text hpTxt;
    private Image hpImg;
    private Text fyTxt; //防御数值
    private List<CardItem> cardItemList; //卡牌物体的集合

    private void Start()
    {
        UpdateHp();
        UpdatePower();
        UpdateDefense();
        UpdateCardCount();
        UpdateUsedCardCount();
    }

    private void Awake()
    {
        cardItemList = new List<CardItem>();
        cardCountTxt = transform.Find("hasCard/icon/Text").GetComponent<Text>();
        usedCardCountTxt = transform.Find("noCard/icon/Text").GetComponent<Text>();
        powerTxt = transform.Find("mana/Text").GetComponent<Text>();
        hpTxt = transform.Find("hp/hpTxt").GetComponent<Text>();
        hpImg = transform.Find("hp/fill").GetComponent<Image>();
        fyTxt = transform.Find("hp/fangyu/Text").GetComponent<Text>();
        transform.Find("turnBtn").GetComponent<Button>().onClick.AddListener(onChangeTurnBtn);
    }

    //玩家回合结束，切换到敌人回合
    private void onChangeTurnBtn()
    {
        //只有玩家回合才能切换
        if(FightManager.Instance.fightUnit is Fight_PlayerTurn)
        {
            FightManager.Instance.ChangeType(FightType.Enemy);
        }
    }

    public void UpdateHp()
    {
        hpTxt.text = FightManager.Instance.CurHp.ToString() + "/" + FightManager.Instance.MaxHp.ToString();
        hpImg.fillAmount = (float)FightManager.Instance.CurHp / (float)FightManager.Instance.MaxHp;
    }

    public void UpdatePower()
    {
        powerTxt.text = FightManager.Instance.CurPowerCount.ToString() + "/" + FightManager.Instance.MaxPowerCount.ToString();
    }

    public void UpdateDefense()
    {
        fyTxt.text = FightManager.Instance.DefenseCount.ToString();
    }

    public void UpdateCardCount()
    {
        cardCountTxt.text = FightCardManager.Instance.cardList.Count.ToString();
    }

    public void UpdateUsedCardCount()
    {
        usedCardCountTxt.text = FightCardManager.Instance.usedCardList.Count.ToString();
    }

    //创建卡牌物体
    public void CreateCardItem(int count)
    {
        if (count > FightCardManager.Instance.cardList.Count)
        {
            count = FightCardManager.Instance.cardList.Count;
        }
        for (int i = 0; i < count; i++)
        {
            GameObject obj = Instantiate(Resources.Load("UI/CardItem"), transform) as GameObject;
            obj.GetComponent<RectTransform>().anchoredPosition = new Vector2(-1000, -700);
            //var item = obj.AddComponent<CardItem>();

            string cardId = FightCardManager.Instance.DrawCard();
            Dictionary<string, string> data = GameConfigManager.Instance.GetCardById(cardId);
            CardItem item = obj.AddComponent(System.Type.GetType(data["Script"])) as CardItem;
            item.Init(data);
            cardItemList.Add(item);
        }
    }

    //更新卡牌位置
    public void UpdateCardItemPos()
    {
        float offset = 800.0f / cardItemList.Count;
        Vector2 startPos = new Vector2(-cardItemList.Count / 2.0f * offset + offset * 0.5f, -700);
        for(int i =0;i<cardItemList.Count;i++)
        {
            cardItemList[i].GetComponent<RectTransform>().DOAnchorPos(startPos, 0.5f);
            startPos.x += offset;
        }
    }

    //删除卡牌物体
    public void RemoveCard(CardItem item)
    {
        AudioManager.Instance.PlayEffect("Cards/cardShove");

        item.enabled = false; //禁用卡牌逻辑

        //添加到弃牌堆
        FightCardManager.Instance.usedCardList.Add(item.data["Id"]);

        //更新使用后的卡牌数量
        usedCardCountTxt.text = FightCardManager.Instance.usedCardList.Count.ToString();

        //从集合中删除
        cardItemList.Remove(item);

        //刷新卡牌位置
        UpdateCardItemPos();

        //卡牌移到弃牌堆效果
        item.GetComponent<RectTransform>().DOAnchorPos(new Vector2(1000, -700), 0.25f);
        item.transform.DOScale(0, 0.25f);
        Destroy(item.gameObject, 1);
    }

    //删除使用过的卡牌
    public void RemoveUsedCard(CardItem item)
    {
        AudioManager.Instance.PlayEffect("Cards/cardShove");

        item.enabled = false; //禁用卡牌逻辑

        //判断卡牌是否为消耗牌
        Dictionary<string, string> data = GameConfigManager.Instance.GetCardTypeById(item.data["Type"]);
        if (int.Parse(data["Index"]) != 3)
        {
            //添加到弃牌堆
            FightCardManager.Instance.usedCardList.Add(item.data["Id"]);
        }

        //更新使用后的卡牌数量
        usedCardCountTxt.text = FightCardManager.Instance.usedCardList.Count.ToString();

        //从集合中删除
        cardItemList.Remove(item);

        //刷新卡牌位置
        UpdateCardItemPos();

        //卡牌移到弃牌堆效果
        item.GetComponent<RectTransform>().DOAnchorPos(new Vector2(1000, -700), 0.25f);
        item.transform.DOScale(0, 0.25f);
        Destroy(item.gameObject, 1);
    }

    //删除所有卡牌
    public void RemoveAllCards()
    {
        for (int i = cardItemList.Count - 1; i >= 0; i--)
        {
            RemoveCard(cardItemList[i]);
        }
    }


}
