using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BluePrint : MonoBehaviour
{
    public Text hpNum;
    public Text costNum;
    public Text atkNum;
    public Text atkTimeNum;
    public Text buildTimeNum;
    public Text desText;//叙述文本
    public Image icon;//塔图标

    private TowerIcon towerIcon;

    private void Awake()
    {
        towerIcon = icon.gameObject.GetComponent<TowerIcon>();
    }

    /// <summary>
    /// 初始化蓝图信息
    /// </summary>
    /// <param name="id"></param>
    public void InitBluePrint(TowerInfo info)
    {
        //给塔图标传输信息
        towerIcon.Info = info;
        //更改图标
        icon.sprite = Resources.Load<Sprite>(@"TowerIcon\" + info.name);
        //更改塔属性
        hpNum.text = info.hp.ToString();
        atkNum.text = info.atk.ToString();
        atkTimeNum.text = info.atkTime.ToString();
        buildTimeNum.text = info.buildTime.ToString();
        costNum.text = info.buildCost.ToString();
        //更改描述
        desText.text = info.des;
    }
}
