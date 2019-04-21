using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/// <summary>
///关卡编辑器
/// </summary>
public class SystemLevelEditor : MonoBehaviour
{
    public GameObject boss;
    //单例模式
    public static SystemLevelEditor instance;
    /// <summary>
    /// 当前死亡怪物数
    /// </summary>
    private int nowDeathCnt = 0;
    /// <summary>
    /// 总关卡数
    /// </summary>
    public int maxLevel = 5;
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
    private void Awake()
    {
        instance = this;
        theMonsterGrow = GetComponent<MonsterAutoGrowEditor>();
    }

    private void Start()
    {
        CountDownTimer.instance.ShowTimer();
    }

    /// <summary>
    /// 开始自动生成敌人
    /// </summary>
    public void MonsterAutoGrow()
    {
        LevelNumTxt.instance.ShowLevelNum(currentLevel);
        theMonsterGrow.monsterAutoGrow(currentLevel, monsterTypeNumberForEachLevel[currentLevel - 1]);
    }
    /// <summary>
    /// 怪物死亡清算
    /// </summary>
    public void DeathCnt()
    {
        nowDeathCnt++;
        //每次有怪物死亡就判断，如果怪物全死亡 一关结束 计时器开始
        if (currentLevel != maxLevel && nowDeathCnt >= monsterCntForEachLevel[currentLevel - 1])
        {
            //熔炉生成零件
            Burner.instance.Creat();
            nowDeathCnt = 0;
            //显示关卡完成后开始计时
            CompletedTxt.instance.Show();
            Invoke("ShowTimer", 2.0f);
            currentLevel++;
        }
        if (currentLevel == maxLevel)
        {
            //告知这次倒计时为boss倒计时
            CountDownTimer.instance.isBoss = true;
            Burner.instance.Creat();
            nowDeathCnt = 0;
            //显示关卡完成后开始计时
            CompletedTxt.instance.Show();
            Invoke("ShowTimer", 2.0f);           
        }
    }
    /// <summary>
    /// 开始计时
    /// </summary>
    private void ShowTimer()
    {
        CountDownTimer.instance.ShowTimer();       
    }

    /// <summary>
    /// 开始boss关卡
    /// </summary>
    public void StartBossLevel()
    {
        //生成BOSS
        GameObject.Instantiate(boss, transform.position + new Vector3(4, 0, 4), Quaternion.identity);
        //播放登场动画
        //播放BOSS关卡Notice
        BossLevelTxt.instance.Show();
        //更换音乐
        AudioManager.instance.PlayBossRoarClip();
        AudioManager.instance.PlayBossBGM();
    }
}

