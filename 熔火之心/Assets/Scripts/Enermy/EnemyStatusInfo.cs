﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 敌人状态信息类
/// </summary>
public class EnemyStatusInfo : MonoBehaviour
{
    public bool Isdead;
    ParticleSystem blood;
    /// <summary>
    /// 敌人执行攻击动作的范围与伤害距离的差距
    /// </summary>
    public float atkExecuteRange;
    /// <summary>
    /// 攻击范围
    /// </summary>
    public float atkRange;
    /// <summary>
    /// 当前血量
    /// </summary>
    public float currentHP;
    /// <summary>
    /// 最大血量
    /// </summary>
    public float maxHP;
    public void Damage(float amount)
    {
        //如果敌人已经死亡 则退出(防止虐尸)
        if (currentHP <= 0) return;
        currentHP -= amount;
        if (currentHP <= 0)
            Death();
        blood.Play();
    }
    /// <summary>
    /// 死亡延迟时间
    /// </summary>
    public float deathDelay = 10;
    //敌人生成器引用  敌人创建时由生成器传递
    SystemLevelEditor theEditor;
    private void Start()
    {
        blood = GetComponentInChildren<ParticleSystem>();
        theEditor = GameObject.FindGameObjectWithTag("LevelEditor").GetComponent<SystemLevelEditor>();

    }

    /// <summary>
    /// 死亡
    /// </summary>
    public void Death()
    {
        Isdead = true;
        //销毁当前游戏物体
        Destroy(gameObject, deathDelay);
        //播放动画
        var anim = GetComponent<EnemyAnimation>();
        anim.Play(anim.deathName);
        //修改状态
        GetComponent<EnemyAI>().state = EnemyAI.State.Death;
        //给生成器传输当前死亡数加一
        theEditor.deachCnt();
    }

}
