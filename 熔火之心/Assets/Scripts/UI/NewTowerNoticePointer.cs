using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 新塔图纸诞生提示指针
/// </summary>
public class NewTowerNoticePointer : MonoBehaviour
{
    //单例模式
    public static NewTowerNoticePointer instance;

    private Image img;
    private Animation anim;

    private void Awake()
    {
        instance = this;
        img = gameObject.GetComponent<Image>();
        anim = gameObject.GetComponent<Animation>();
    }

    public void Show()
    {
        img.enabled = true;
        anim.enabled = true;
    }

    public void Hide()
    {
        img.enabled = false;
        anim.enabled = false;
    }
}
