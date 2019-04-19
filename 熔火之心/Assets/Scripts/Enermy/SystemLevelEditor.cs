using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/// <summary>
///关卡编辑器
/// </summary>
public class SystemLevelEditor : MonoBehaviour
{
    private bool monsterGrowFlag = true; 
    private Text timerText;
    private int remainTime = 3;//剩余时间
    private float timer = 0;//计时器
    public bool isBegin = true; //是否开始计时
    /// <summary>
        /// 初始化计时器 开关开启
        /// </summary>
    public void ShowTimer()
        {
            timerText.enabled = true;
            //初始化计时器
            remainTime = 3;
            timer = 0;
            //开始计时
            isBegin = true;
        }
    /// <summary>
        /// 隐藏倒计时
        /// </summary>
    private void HideTimer()
        {
            timerText.enabled = false;
            //调用关卡管理器的开始生成敌人
            print("敌人开始出来了");
        }
    /// <summary>
    /// 当前死亡怪物数
    /// </summary>
    private int nowDeachCnt = 0;
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
    private void Awake()
    {
        theMonsterGrow = GetComponent<MonsterAutoGrowEditor>();
        timerText = gameObject.GetComponent<Text>();
    }
    void Start()
    {
        ShowTimer();
    }
    private void Update()
    {
        //计时 如果计时开关开启 开始计时  如果计时完毕 开关关闭
        if (isBegin)
        {
            timer += Time.deltaTime;
            if (timer >= 1)
            {
                timer = 0;
                remainTime -= 1;
                timerText.text = string.Format("{0:d2}", remainTime % 60);
            }
            if (remainTime <= 0)
            {

                isBegin = false;
                HideTimer();
            }
        }
        //如果怪物全死亡  计时器开始

        if (!isBegin&&monsterGrowFlag)
        {
            theMonsterGrow.monsterAutoGrow(monsterCntForEachLevel[currentLevel - 1], monsterTypeNumberForEachLevel[currentLevel - 1]);
            monsterGrowFlag = false;
        }
        if (nowDeachCnt >= monsterCntForEachLevel[currentLevel - 1])
        {
            monsterGrowFlag = true; 
            nowDeachCnt = 0;
            ShowTimer();
            currentLevel++;
        }
        //if (currentLevel == maxLevel) ;//生成boss
    }
    public void deachCnt()
    {
        nowDeachCnt++;
    }
}

