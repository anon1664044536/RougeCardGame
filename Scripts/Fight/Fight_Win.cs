using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fight_Win : FightUnit
{
    public override void Init()
    {
        FightManager.Instance.StopAllCoroutines();
        Debug.Log("游戏胜利");
    }
}
