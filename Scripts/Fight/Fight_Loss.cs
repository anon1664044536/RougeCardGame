using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fight_Loss : FightUnit
{
    public override void Init()
    {
        Debug.Log("失败了");
        FightManager.Instance.StopAllCoroutines();

        //显示失败界面
    }

    public override void OnUpdata()
    {

    }
}
