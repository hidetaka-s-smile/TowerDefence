using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
///控制动画播放
/// </summary>

public class EnemyAnimation : MonoBehaviour
{
    /// <summary>
    /// 跑步动画
    /// </summary>
    public string runName = "Run";

    /// <summary>
    /// 攻击动画
    /// </summary>
    public string atkName = "Attack01";

    /// <summary>
    /// 死亡动画
    /// </summary>
    public string deathName = "Dead";

    /// <summary>
    /// 闲置动画
    /// </summary>
    public string idleName = "Idle";
    private Animation anim;
    private void Awake()
    {
        //查找动画组件
        anim = GetComponentInChildren<Animation>();

    }

    /// <summary>
    /// 播放指定名称的人物动画
    /// </summary>
    /// <param name="name">动画名称</param>
    public void Play(string name)
    {
        anim.CrossFade(name);
    }

    /// <summary>
    /// 指定动画片段是否正在播放
    /// </summary>
    /// <param name="name">动画片段名称</param>
    /// <returns></returns>
    public bool IsPlaying(string name)
    {
        return anim.IsPlaying(name);
    }
}
