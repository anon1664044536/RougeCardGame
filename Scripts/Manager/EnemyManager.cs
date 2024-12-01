using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager
{
    public static EnemyManager Instance = new EnemyManager();

    private List<Enemy> enemyList;//储存战斗中的敌人

    //关卡id
    public void LoadRes(string id)
    {
        enemyList = new List<Enemy>();

        //读取关卡表
        Dictionary<string,string> levelData = GameConfigManager.Instance.GetLevelById(id);

        string[] enemyIds = levelData["EnemyIds"].Split('=');
        string[] enemyPos = levelData["Pos"].Split('='); //敌人位置信息

        for (int i = 0; i < enemyIds.Length; i++)
        {
            string enemyId = enemyIds[i];
            string[] posArr = enemyPos[i].Split(",");

            //敌人位置
            float x = float.Parse(posArr[0]);
            float y = float.Parse(posArr[1]);
            float z = float.Parse(posArr[2]);

            Dictionary<string,string> enemyData = GameConfigManager.Instance.GetEnemyById(enemyId);

            //从资源加载对于敌人模型
            GameObject obj = Object.Instantiate(Resources.Load(enemyData["Model"])) as GameObject;

            Enemy enemy = obj.AddComponent<Enemy>();//添加敌人脚本

            enemy.Init(enemyData);//储存敌人信息

            //储存到集合
            enemyList.Add(enemy);

            obj.transform.position = new Vector3(x, y, z);
        }
    }

    //移除敌人
    public void DeleteEnemy(Enemy enemy)
    {
        enemyList.Remove(enemy);

        //是否击杀所有怪物
        if(enemyList.Count == 0)
        {
            FightManager.Instance.ChangeType(FightType.Win);
        }

    }

    //执行活着的敌人的行为
    public IEnumerator DoAllEnemyAction()
    {
        for(int i = 0;i < enemyList.Count; i++)
        {
            yield return FightManager.Instance.StartCoroutine(enemyList[i].DoAction());
        }

        //行动完成后，更新所有敌人行为
        for(int i = 0;i< enemyList.Count; i++)
        {
            enemyList[i].SetRandomAction();
        }

        //切换至玩家回合
        FightManager.Instance.ChangeType(FightType.Player);
    }

    //攻击所有敌人
    public void HitAllEnemy(int val)
    {
        List<Enemy> curEnemyList = new List<Enemy>();

        for(int i =0;i<enemyList.Count;i++)
        {
            curEnemyList.Add(enemyList[i]);
        }

        for(int i = 0;i < curEnemyList.Count;i++)
        {
            curEnemyList[i].Hit(val);
        }

        curEnemyList.Clear();
    }

    //所有敌人的攻击特效
    public void PlayEffectOnAllEneny(CardItem cardItem)
    {
        for (int i = 0; i < enemyList.Count; i++)
        {
            cardItem.PlayEffect(enemyList[i].transform.position);
        }     
    }

    //选中所有敌人
    public void SelectAllEnemy()
    {
        for (int i = 0; i < enemyList.Count; i++)
        {
            enemyList[i].Onselect();
        }
    }

    //未选中所有敌人
    public void UnSelectAllEnemy()
    {
        for (int i = 0; i < enemyList.Count; i++)
        {
            enemyList[i].OnUnselect();
        }
    }

    //刷新所有敌人的攻击意图
    public void ReSetAllEnemy()
    {
        for (int i = 0; i < enemyList.Count; i++)
        {
            enemyList[i].SetRandomAction();
        }
    }

}
