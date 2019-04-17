using UnityEngine;
using System.Collections;

/// <summary>
/// 人工智能
/// </summary>
[RequireComponent(typeof(EnemyAnimation), typeof(EnemyMotor), typeof(EnemyStatusinfo))]
public class EnemyAI : MonoBehaviour
{
    private Transform thePlayer;
    private float theExecuteRange;
    private float theAtkRange;
    private EnemyAnimation animAction;
    private EnemyMotor motor;
    private float atkTime = 0;
    /// <summary>
    /// 攻击间隔
    /// </summary>
    public float atkInterval = 3;
    /// <summary>
    /// 攻击延迟时间
    /// </summary>
    public float delay = 0.3f;
    /// <summary>
    /// 敌人状态
    /// </summary>
    private float RecoveyMove ;
    private float RecoveyAtk;
    
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
    private void Start()
    {
        thePlayer = GameObject.FindGameObjectWithTag("player").transform;
        theExecuteRange = GetComponent<EnemyStatusinfo>().atkExecuteRange;
        theAtkRange = GetComponent<EnemyStatusinfo>().atkRange;
        animAction = GetComponent<EnemyAnimation>();
        motor = GetComponent<EnemyMotor>();
        RecoveyMove = motor.moveSpeed;
        RecoveyAtk = atkInterval; 
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
    public void CaculateDamaga()
    {
        
        if (Vector3.Distance(thePlayer.position, transform.position) < theAtkRange)
        {
            print("D");
            thePlayer.GetComponent<Player>().GetDamage(1);
        }
    }
    private void Attack()
    {
        animAction.Play(animAction.atkName);
        Invoke("CaculateDamaga", delay);
        //限制攻击频率
        //播放攻动画
        if (atkTime <= Time.time )
        {
            animAction.Play(animAction.atkName);
            
            atkTime = Time.time + atkInterval;
        }

        if (!animAction.IsPlaying(animAction.atkName) )
        {
            //如果攻击动画没有播放  再  播放闲置动画
            animAction.Play(animAction.idleName);
        }
        if (motor.run()) state = State.Run;
    }
    private void Run()
    {
        //播放跑步动画
        animAction.Play(animAction.runName);
        //调用马达寻路功能  如果到达终点，修改状态为 state 攻击
        if (!motor.run()) state = State.Attack;
    } 
    /// <summary>
    /// 冰冻
    /// </summary>
    /// <param name="debuff">减少倍数 整数</param>
    /// <param name="recovetTime">恢复时间 浮点类型</param>
    public void frozen(int debuff,float recovetTime)
    {
        motor.moveSpeed /= debuff;
        atkInterval /= recovetTime;
        Invoke("recovey", recovetTime);
    }
    private void recovey()
    {
        motor.moveSpeed = RecoveyMove;
        atkInterval = RecoveyAtk;
    }

}
