using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 塔的信息库类
/// </summary>
public class TowerInfos : MonoBehaviour
{
    //单例模式
    public static TowerInfos instance;

    public TextAsset textAsset;//储存塔信息

    private Dictionary<int, TowerInfo> towerInfoDict = new Dictionary<int, TowerInfo>();

    private void Awake()
    {
        Debug.Log(textAsset.text);
        Debug.LogWarning("无用警告");
        InitInfosFromText();
    }

    /// <summary>
    /// 从文档读取塔的信息初始化字典
    /// </summary>
    private void InitInfosFromText()
    {
        string text = textAsset.text;
        Debug.Log(text);
        string[] towerInfosArray = text.Split('\n');//储存不同塔的信息
        foreach (var towerInfoArray in towerInfosArray)
        {
            string[] propArray = towerInfoArray.Split('，');//储存塔信息的每个属性
            TowerInfo towerInfo = new TowerInfo();
            //下标0为ID，1为名字，2为生命值，3为攻击力，4为攻击间隔，5为建造时间，6为建造费用
            towerInfo.id = int.Parse(propArray[0]);
            towerInfo.name = propArray[1];
            towerInfo.hp = int.Parse(propArray[2]);
            towerInfo.atk = int.Parse(propArray[3]);
            towerInfo.atkTime = float.Parse(propArray[4]);
            towerInfo.buildTime = int.Parse(propArray[5]);
            towerInfo.buildCost = int.Parse(propArray[6]);
            //储存完毕，加入字典
            towerInfoDict.Add(towerInfo.id, towerInfo);
            print("已经导入一种名为" + towerInfo.name + "的塔");
        }
        print("当前共有" + towerInfoDict.Count + "种塔");
    }

    /// <summary>
    /// 通过ID获取塔的信息
    /// </summary>
    /// <param name="id"></param>
    private TowerInfo GetTowerInfo(int id)
    {
        TowerInfo towerInfo = null;
        towerInfoDict.TryGetValue(id, out towerInfo);
        return towerInfo;
    }
}
