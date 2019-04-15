using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
///关卡编辑器
/// </summary>
public class SystemLevelEditor : MonoBehaviour
{

    /// <summary>
    /// 当前死亡怪物数
    /// </summary>
    private int nowDeachCnt = 0;
    /// <summary>
    /// 关卡间歇时间
    /// </summary>
    public int restSecond = 120;
    /// <summary>
    /// 总关卡数
    /// </summary>
    public int maxLevel = 10;
    /// <summary>
    /// 当前关卡
    /// </summary>
    public int currentLevel = 1;


    /// <summary>
    /// 怪物类型
    /// </summary>
    public GameObject[] monsterType;
    /// <summary>
    /// 每关怪物类型数量
    /// </summary>
    public int[] monsterTypeNumberForEachLevel;
    /// <summary>
    /// 每关怪物数
    /// < summary>
    public int[] monsterCntForEachLevel;
    MonsterAutoGrowEditor theMonsterGrow;
    void Start()
    {
        theMonsterGrow = GetComponent<MonsterAutoGrowEditor>();

        theMonsterGrow.monsterAutoGrow(monsterCntForEachLevel[0], monsterTypeNumberForEachLevel[0]);
        
    }
    private void Update()
    {
        //如果死亡数达到 进入下一关 生成下一关怪 死亡数清零 
        
        if (nowDeachCnt >= monsterCntForEachLevel[currentLevel - 1])
        {
            nowDeachCnt = 0;
            currentLevel++;
            theMonsterGrow.monsterAutoGrow( monsterCntForEachLevel[currentLevel - 1], monsterTypeNumberForEachLevel[currentLevel - 1] );//生成对应数量怪兽
        }
        if (currentLevel == maxLevel) ;//生成boss
    }
    public void deachCnt()
    {
        nowDeachCnt++;
    }
}

