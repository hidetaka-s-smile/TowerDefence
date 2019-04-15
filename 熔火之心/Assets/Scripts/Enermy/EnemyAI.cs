using UnityEngine;
using System.Collections;

/// <summary>
/// 人工智能
/// </summary>
[RequireComponent(typeof(EnemyAnimation), typeof(EnemyMotor), typeof(EnemyStatusinfo))]
public class EnemyAI : MonoBehaviour
{
    /// <summary>
    /// 敌人状态
    /// </summary>
    public enum State
    {
        /// <summary>
        /// 攻击状态
        /// </summary>
        Attack,
        /// <summary>
        /// 死亡状态
        /// </summary>
        Death,
        /// <summary>
        /// 寻路状态
        /// </summary>
        Run
    }

    private EnemyAnimation animAction;
    private EnemyMotor motor;
    private void Start()
    {
        animAction = GetComponent<EnemyAnimation>();
        motor = GetComponent<EnemyMotor>();
    }
    /// <summary>
    /// 敌人状态
    /// </summary>
    public State state = State.Run;
    //每帧判断状态
    private void Update()
    {
        switch (state)
        {
            case State.Run:
                Run();
                break;
            case State.Attack:
                Attack();
                break;
        }
    }

    private float atkTime=Time.time;
    /// <summary>
    /// 攻击间隔
    /// </summary>
    public float atkInterval = 3;

    /// <summary>
    /// 攻击延迟时间
    /// </summary>
    public float delay = 0.3f;

    private void Attack()
    {
        //限制攻击频率
        //播放攻动画
        if (atkTime <= Time.time )
        {
            animAction.Play(animAction.atkName);
            //攻击具体内容
            atkTime = Time.time + atkInterval;
        }

        if (!animAction.IsPlaying(animAction.atkName) )
        {
            //如果攻击动画没有播放  再  播放闲置动画
            animAction.Play(animAction.idleName);
        }
    }

    private void Run()
    {
        //播放跑步动画
        animAction.Play(animAction.runName);
        //调用马达寻路功能  如果到达终点，修改状态为 state 攻击
        if (!motor.run()) state = State.Attack;
    }

}
