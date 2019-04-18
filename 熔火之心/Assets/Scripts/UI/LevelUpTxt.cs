using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 升级提示UI
/// </summary>
public class LevelUpTxt : MonoBehaviour
{
    //单例模式
    public static LevelUpTxt instance;

    public float animTime = 0;//动画播放时间
    private Animation anim;
    private Image img;

    private void Awake()
    {
        instance = this;
        img = gameObject.GetComponent<Image>();
        anim = gameObject.GetComponent<Animation>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            Show();
        }
    }

    /// <summary>
    /// 显示并播放动画
    /// </summary>
    public void Show()
    {
        img.enabled = true;
        anim.Play();
        Invoke("Hide", animTime);
    }

    /// <summary>
    /// 动画播放完后隐藏图片
    /// </summary>
    public void Hide()
    {
        img.enabled = false;
    }
}
