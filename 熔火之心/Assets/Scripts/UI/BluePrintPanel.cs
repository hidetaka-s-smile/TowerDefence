using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 蓝图界面
/// </summary>
public class BluePrintPanel : MonoBehaviour
{
    //单例模式
    public static BluePrintPanel instance;

    public Scrollbar scrollbar;//垂直滑动条
    public Image fillImg;//滑动条图片
    public Image bgImg;//滑动条背景图片
    public Transform bluePrintList;//蓝图列表,新的图纸的父物体
    public GameObject bluePrintPrefab;//蓝图预制体
    public int[] towerIdArray;//玩家能解锁的塔的编号数组 

    private Animation anim;
    private int curIndex = 0;//当前即将解锁的塔编号
    private bool isOpen = false;
    private bool isLockFinalTower = false;//是否解锁了最终塔
    private Player player;

    private void Awake()
    {
        instance = this;
        anim = gameObject.GetComponent<Animation>();
        player = GameObject.FindGameObjectWithTag(Tags.player).GetComponent<Player>();
    }

    private void Start()
    {
        //默认一开始就有基础炮塔图纸
        InventNewTower();
    }

    private void Update()
    {
        //当人物满级之后，可以按住L和M解锁最终塔 怜悯
        if(player.Level >= 5)
        {
            if (Input.GetKey(KeyCode.L))
            {
                if (Input.GetKeyDown(KeyCode.M))
                {
                    if (!isLockFinalTower)
                    {
                        isLockFinalTower = true;
                        InventNewTower();
                    }
                }
            }
        }
    }

    /// <summary>
    /// 面板进入游戏界面
    /// </summary>
    public void OnPanelEnter()
    {
        AudioManager.instance.PlayBluePrintClip();
        if (NewTowerNoticePointer.instance.IsShow)
            NewTowerNoticePointer.instance.Hide();
        if (!isOpen && !anim.isPlaying)
        {
            scrollbar.value = 1;
            isOpen = true;
            anim.Play("BluePrintPanelEnter");
        }
    }

    /// <summary>
    /// 面板离开游戏界面
    /// </summary>
    public void OnPanelExit()
    {
        AudioManager.instance.PlayBluePrintClip();
        if (isOpen && !anim.isPlaying)
        {
            isOpen = false;
            anim.Play("BluePrintPanelExit");
        }
    }

    /// <summary>
    /// 发明新的塔图纸，供玩家升级时调用
    /// </summary>
    public void InventNewTower()
    {
        //提醒玩家
        if (curIndex > 0)
        {
            NewTowerNoticePointer.instance.Show();
        }
        TowerInfo newTowerInfo = new TowerInfo();
        if (curIndex < towerIdArray.Length)
        {
            newTowerInfo = TowerInfos.instance.GetTowerInfo(towerIdArray[curIndex]);
            curIndex++;
        }
        // 超过两张图纸 显示滑动条
        if (curIndex > 2)
            ShowScrollbar();
        //安全校验
        if (newTowerInfo != null)
        {
            //实例化新的图纸，加入到图纸列表中
            GameObject go = Instantiate(bluePrintPrefab, bluePrintList);
            BluePrint bluePrint = go.GetComponent<BluePrint>();
            bluePrint.InitBluePrint(newTowerInfo);
        }
    }

    /// <summary>
    /// 超过两张图纸 显示滑动条
    /// </summary>
    private void ShowScrollbar()
    {
        fillImg.enabled = true;
        bgImg.enabled = true;
    }
}
