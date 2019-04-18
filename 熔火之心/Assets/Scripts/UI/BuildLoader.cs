using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 建造读条
/// </summary>
public class BuildLoader : MonoBehaviour
{
    //单例模式
    public static BuildLoader instance;

    public Slider slider;
    public Image bg;//背景
    public Image fill;//拉条

    private float loadTime;//加载的时间
    public bool isLoading = false;//是否正在加载

    private void Awake()
    {
        instance = this;
    }

    private void FixedUpdate()
    {
        if (isLoading)
        {
            slider.value += (1.0f / 50.0f) / loadTime;
            //slider.value = Mathf.Lerp(slider.value, 1, Time.deltaTime );
            //读条完毕，隐藏读条
            if(slider.value > 0.9f)
            {
                isLoading = false;
                HideLoader();
            }
        }
    }

    /// <summary>
    /// 读条
    /// </summary>
    /// <param name="time">读条的时间</param>
    public void BuildLoad(float time)
    {
        slider.value = 0;
        ShowLoader();
        isLoading = true;
        loadTime = time;
    }

    /// <summary>
    /// 显示读条
    /// </summary>
    private void ShowLoader()
    {
        bg.enabled = true;
        fill.enabled = true;
    }

    /// <summary>
    /// 隐藏读条
    /// </summary>
    private void HideLoader()
    {
        bg.enabled = false;
        fill.enabled = false;
    }
}
