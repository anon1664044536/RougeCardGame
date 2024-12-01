using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ConditionManager : MonoBehaviour
{
    public static ConditionManager Instanse = new ConditionManager();

    //反弹次数
    public int mirrorTime;

    //群伤次数
    public int AOETime;

    public void Init()
    {
        mirrorTime = 0;
        AOETime = 0;
    }

    public void Awake()
    {
        Instanse = this;
    }

}
