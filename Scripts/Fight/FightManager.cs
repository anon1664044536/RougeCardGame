using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//战斗枚举
public enum FightType
{
    None,
    Init,
    Player,
    Enemy,
    Win,
    Loss
}

public class FightManager : MonoBehaviour
{
    public static FightManager Instance;
    public FightUnit fightUnit; //战斗单元

    public int MaxHp;
    public int CurHp; //当前血量

    public int MaxPowerCount;
    public int CurPowerCount; //当前能量

    public int DefenseCount;

    public void Init()
    {
        MaxHp = 100;
        CurHp = 100;
        MaxPowerCount = 3; 
        CurPowerCount = 3;
        DefenseCount = 10;
    }

    private void Awake()
    {
        Instance = this;
    }

    //切换战斗类型
    public void ChangeType(FightType type)
    {
        switch(type)
        {
            case FightType.None:
                break;
            case FightType.Init:
                fightUnit = new FightInit(); 
                break;
            case FightType.Player:
                fightUnit = new Fight_PlayerTurn();
                break;
            case FightType.Enemy:
                fightUnit = new Fight_EnemyTurn();
                break;
            case FightType.Win:
                fightUnit = new Fight_Win();
                break;
            case FightType.Loss:
                fightUnit = new Fight_Loss();
                break;
        }
        fightUnit.Init();
    }

    //玩家受伤逻辑
    public void GetPlayerHit(int hit, Enemy thisEnemy = null)
    {
        //扣护盾
        if(DefenseCount >= hit)
        {
            DefenseCount -= hit;
        }
        else
        {
            hit -= DefenseCount;
            DefenseCount = 0;
            if(ConditionManager.Instanse.mirrorTime > 0)
            {
                thisEnemy.Hit(hit);
                hit = 0;
                ConditionManager.Instanse.mirrorTime--;
            }
            CurHp -= hit;
            if(CurHp <= 0)
            {
                CurHp = 0;

                //切换到游戏失败状态
                ChangeType(FightType.Loss);
            }
        }

        //更新界面
        UIManager.Instance.GetUI<FightUI>("FightUI").UpdateHp();
        UIManager.Instance.GetUI<FightUI>("FightUI").UpdateDefense();
    }

    private void Update()
    {
        if (fightUnit != null)
        {
            fightUnit.OnUpdata();
        }
    }
}
