using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CountDownTimer : MonoBehaviour
{
    //单例模式
    public static CountDownTimer instance;

    private Text timerText;
    private int remainTime = 30;//剩余时间
    private float timer = 0;//计时器
    private bool isBegin = false;//是否开始计时
    private Animation anim;

    private void Awake()
    {
        instance = this;
        timerText = gameObject.GetComponent<Text>();
        anim = gameObject.GetComponent<Animation>();
    }

    private void Update()
    {
        if (isBegin)
        {
            timer += Time.deltaTime;
            if (timer >= 1)
            {
                timer = 0;
                remainTime -= 1;
                timerText.text = remainTime.ToString() + 's';
            }
            if (remainTime <= 0)
            {
                isBegin = false;
                HideTimer();
            }
        }
    }



    /// <summary>
    /// 显示倒计时
    /// </summary>
    public void ShowTimer()
    {       
        //初始化计时器
        remainTime = 30;
        timer = 0;
        //显示计时器
        timerText.enabled = true;
        anim.Play();       
        //开始计时
        isBegin = true;
    }

    /// <summary>
    /// 隐藏倒计时
    /// </summary>
    private void HideTimer()
    {
        anim.Stop();
        timerText.enabled = false;
        //调用关卡管理器的开始生成敌人
        SystemLevelEditor.instance.MonsterAutoGrow();
    }
}
