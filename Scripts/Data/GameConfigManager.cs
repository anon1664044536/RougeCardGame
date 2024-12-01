using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameConfigManager
{
    public static GameConfigManager Instance = new GameConfigManager();
    private GameConfigData cardData; //卡牌表
    private GameConfigData enemyData; //敌人表
    private GameConfigData levelData; // 关卡表
    private GameConfigData cardTypeData; //卡牌类型表
    private TextAsset textAsset;
    

    public void Init()
    {
        textAsset = Resources.Load<TextAsset>("Data/card");
        cardData = new GameConfigData(textAsset.text);

        textAsset = Resources.Load<TextAsset>("Data/enemy");
        enemyData = new GameConfigData(textAsset.text);

        textAsset = Resources.Load<TextAsset>("Data/level");
        levelData = new GameConfigData(textAsset.text);

        textAsset = Resources.Load<TextAsset>("Data/cardType");
        cardTypeData = new GameConfigData(textAsset.text);
    }

    public List<Dictionary<string,string>> GetCardLines()
    {
        return cardData.GetLines();
    }

    public List<Dictionary<string, string>> GetEnemyLines()
    {
        return enemyData.GetLines();
    }

    public List<Dictionary<string, string>> GetLevelLines()
    {
        return levelData.GetLines();
    }

    public Dictionary<string,string>GetCardById(string id)
    {
        return cardData.GetOneById(id);
    }

    public Dictionary<string, string> GetEnemyById(string id)
    {
        return enemyData.GetOneById(id);
    }

    public Dictionary<string, string> GetLevelById(string id)
    {
        return levelData.GetOneById(id);
    }

    public Dictionary<string, string> GetCardTypeById(string id)
    {
        return cardTypeData.GetOneById(id);
    }
}
