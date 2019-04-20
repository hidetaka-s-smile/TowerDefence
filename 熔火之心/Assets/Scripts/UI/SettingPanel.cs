using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingPanel : MonoBehaviour
{
    private Animation anim;
    private bool isOpen = false;

    private void Awake()
    {
        anim = gameObject.GetComponent<Animation>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!isOpen)
            {
                OnOpenBtn();
            }
            else
            {
                OnCloseBtn();
            }    
        }
    }

    /// <summary>
    /// 打开界面
    /// </summary>
    public void OnOpenBtn()
    {
        isOpen = true;
        AudioManager.instance.PlaySettingClip();
        anim.Play("SettingPanelEnter");
        //GameManager.instance.Pause();
        
    }

    /// <summary>
    /// 关闭界面
    /// </summary>
    public void OnCloseBtn()
    {
        isOpen = false;
        AudioManager.instance.PlaySettingClip();
        anim.Play("SettingPanelExit");
        //GameManager.instance.Run();
    }
}
