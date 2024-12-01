using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

/*
祖冲之的算术镜
描述：一面由古代数学家祖冲之所创造的镜子，表面镀上了神秘的算术符号。
效果：使用后，能够抵挡一次攻击。
*/
public class MirrorCard : CardItem
{
    public override void OnEndDrag(PointerEventData eventData)
    {
        if (TryUse() == true)
        {
            int val = int.Parse(data["Arg0"]);
            
            ConditionManager.Instanse.mirrorTime = val;

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
