using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 快捷栏类型
/// </summary>
enum ShortcutType
{
    None,//空快捷栏
    Tower//建塔快捷栏
}

public class Shortcut : MonoBehaviour
{
    public KeyCode keyCode;//快捷键
    public Image iconImg;//快捷栏图标

    private TowerInfo info = null;//该快捷栏放置的塔的信息
    private ShortcutType type = ShortcutType.None;

    public TowerInfo Info { get => info; }

    private Player player;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag(Tags.player).GetComponent<Player>();
    }

    private void Update()
    {
        //按下快捷键使用技能或道具
        if (Input.GetKeyDown(keyCode) && player.isclear == false)
        {
            Cursor.SetCursor(player.cursor_normal, player.hotpots, player.mode);
            player.ifclear = false;
            if (type == ShortcutType.Tower)
            {
                //调用玩家的建造方法，传入info，生成相应预制体
                if(player.Component - info.buildCost>=0)
                {
                    if (Info != null && !player.isbuild)
                        player.Beforebuild(Info);
                }
                else
                {
                    LackCompnentNotice.instance.Show();
                }
                
            }                
            else print("快捷栏为空");
        }
    }

    /// <summary>
    /// 设置快捷栏信息
    /// </summary>
    public void SetShortcutInfo(TowerInfo towerInfo)
    {
        type = ShortcutType.Tower;
        info = towerInfo;
        iconImg.enabled = true;
        iconImg.sprite = Resources.Load<Sprite>(@"TowerIcon\" + towerInfo.name);
    }

    /// <summary>
    /// 清空快捷栏
    /// </summary>
    public void Clear()
    {
        type = ShortcutType.None;
        info = null;
        iconImg.enabled = false;
    }
}