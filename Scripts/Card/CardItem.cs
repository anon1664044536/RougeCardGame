using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.EventSystems;
using UnityEditor;

public class CardItem : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public static CardItem Instance = new CardItem();

    public Dictionary<string, string> data;

    public void Init(Dictionary<string,string> data)
    {
        this.data = data;
    }

    private int index;
    //鼠标进入
    public void OnPointerEnter(PointerEventData eventData)
    {
        transform.DOScale(1.5f, 0.25f);
        index = transform.GetSiblingIndex();
        transform.SetAsLastSibling();

        transform.Find("bg").GetComponent<Image>().material.SetColor("_lineColor", Color.yellow);
        transform.Find("bg").GetComponent<Image>().material.SetFloat("_lineWidth", 10);
    }

    //鼠标离开
    public void OnPointerExit(PointerEventData eventData)
    {
        transform.DOScale(1, 0.25f);
        transform.SetSiblingIndex(index);

        transform.Find("bg").GetComponent<Image>().material.SetColor("_lineColor", Color.black);
        transform.Find("bg").GetComponent<Image>().material.SetFloat("_lineWidth", 1);
    }

    private void Start()
    {
        transform.Find("bg").GetComponent<Image>().sprite = Resources.Load<Sprite>(data["BgIcon"]);
        transform.Find("bg/icon").GetComponent<Image>().sprite = Resources.Load<Sprite>(data["Icon"]);
        transform.Find("bg/msgTxt").GetComponent<Text>().text = string.Format(data["Des"], data["Arg0"]);
        transform.Find("bg/nameTxt").GetComponent<Text>().text = data["Name"];
        transform.Find("bg/useTxt").GetComponent<Text>().text = data["Expend"];
        transform.Find("bg/Text").GetComponent<Text>().text = GameConfigManager.Instance.GetCardTypeById(data["Type"])["Name"];

        //设置bg背景image的外边框材质
        transform.Find("bg").GetComponent<Image>().material = Instantiate(Resources.Load<Material>("Mats/outline"));
    }

    Vector2 initPos; //记录卡牌起始位置
    //开始拖拽
    public virtual void OnBeginDrag(PointerEventData eventData)
    {
        initPos = transform.GetComponent<RectTransform>().anchoredPosition;

        //播放声音
        AudioManager.Instance.PlayEffect("Cards/draw");
    }

    //拖拽中
    public virtual void OnDrag(PointerEventData eventData)
    {
        Vector2 pos;
        if(RectTransformUtility.ScreenPointToLocalPointInRectangle(
            transform.parent.GetComponent<RectTransform>(),
            eventData.position,
            eventData.pressEventCamera,
            out pos
            ))
        {
            transform.GetComponent<RectTransform>().anchoredPosition = pos;
        }
    }

    //结束拖拽
    public virtual void OnEndDrag(PointerEventData eventData)
    {
        transform.GetComponent<RectTransform>().anchoredPosition = initPos;
        transform.SetSiblingIndex(index);
    }

    //尝试使用卡牌
    public virtual bool TryUse()
    {
        int cost = int.Parse(data["Expend"]);

        if (cost > FightManager.Instance.CurPowerCount)
        {
            AudioManager.Instance.PlayEffect("Effect/lose");
            UIManager.Instance.ShowTip("费用不足", Color.red);
            return false;
        }
        else
        {
            FightManager.Instance.CurPowerCount -= cost;

            //更新费用
            UIManager.Instance.GetUI<FightUI>("FightUI").UpdatePower();

            //使用的卡牌删除
            UIManager.Instance.GetUI<FightUI>("FightUI").RemoveUsedCard(this);

            return true;
        }
    }

    //创建卡牌使用效果
    public void PlayEffect(Vector3 pos)
    {
        GameObject effectObj = Instantiate(Resources.Load(data["Effects"])) as GameObject;
        effectObj.transform.position = pos;
        Destroy(effectObj, 2);
    }
}
