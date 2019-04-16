using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BluePrintPanel : MonoBehaviour
{
    public Scrollbar scrollbar;//垂直滑动条

    private Animation anim;
    private bool isOpen = false;

    private void Awake()
    {
        anim = gameObject.GetComponent<Animation>();        
    }

    /// <summary>
    /// 面板进入游戏界面
    /// </summary>
    public void OnPanelEnter()
    {      
        if(!isOpen && !anim.isPlaying)
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
        if (isOpen && !anim.isPlaying)
        {
            isOpen = false;
            anim.Play("BluePrintPanelExit");
        }
    }
}
