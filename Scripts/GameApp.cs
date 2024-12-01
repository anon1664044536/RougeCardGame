using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameApp : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        //初始化配置表
        GameConfigManager.Instance.Init();

        //初始化音频管理器
        AudioManager.Instance.Init();

        //初始化用户信息
        Rolemanager.Instanse.Init();

        //显示loginUI
        UIManager.Instance.ShowUI<LoginUI>("LoginUI");

        //播放BGM
        AudioManager.Instance.PlayBGM("bgm1");

        //显示战斗界面
        //FightManager.Instance.ChangeType(FightType.Init);
    
        string name = GameConfigManager.Instance.GetCardById("1001")["Name"];
        print(name);
    }
}
