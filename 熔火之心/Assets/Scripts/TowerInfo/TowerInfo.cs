using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 塔的信息类
/// </summary>
public class TowerInfo
{
    public int id;         //编号
    public string name;    //塔名
    public int hp;         //生命值
    public int atk;        //攻击力
    public float atkTime;  //攻击间隔
    public int buildTime;  //建造时间
    public int buildCost;  //建造费用
    public string des;     //塔的特性描述
}
