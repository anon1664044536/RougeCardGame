using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class JeraCard : CardItem
{
    public override void OnEndDrag(PointerEventData eventData)
    {
        if (TryUse() == true)
        {
            int val = int.Parse(data["Arg0"]);
            int curPowerCount = FightManager.Instance.CurPowerCount;

            Debug.Log("使用前的费用为" + FightManager.Instance.CurPowerCount.ToString());
            FightManager.Instance.CurPowerCount = val * curPowerCount;
            Debug.Log("乘的倍数为" + val.ToString() + "当前费用为" + FightManager.Instance.CurPowerCount.ToString());

            UIManager.Instance.GetUI<FightUI>("FightUI").UpdatePower();

            //播放使用后的声音
            AudioManager.Instance.PlayEffect("Effect/healspell");
            //播放特效
            Vector3 pos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 2.5f));
            PlayEffect(pos);
        }
        else
        {
            base.OnEndDrag(eventData);
        }
    }
}
