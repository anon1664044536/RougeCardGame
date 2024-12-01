using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Experimental.GlobalIllumination;

public class LoginUI : UIBase
{
    private void Awake()
    {
        Register("bg/startBtn").onClick = onStartGameBtn;
    }

    private void onStartGameBtn(GameObject obj, PointerEventData pData)
    {
        FightManager.Instance.ChangeType(FightType.Init);

        Close(); 
    }
}
