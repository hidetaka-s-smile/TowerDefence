using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingPanel : MonoBehaviour
{
    private Animation anim;

    private void Awake()
    {
        anim = gameObject.GetComponent<Animation>();
    }

    /// <summary>
    /// 打开界面
    /// </summary>
    public void OnOpenBtn()
    {
        anim.Play("SettingPanelEnter");
        //GameManager.instance.Pause();
    }

    /// <summary>
    /// 关闭界面
    /// </summary>
    public void OnCloseBtn()
    {
        anim.Play("SettingPanelExit");
        //GameManager.instance.Run();
    }
}
