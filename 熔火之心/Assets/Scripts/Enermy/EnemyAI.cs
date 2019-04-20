using UnityEngine;
using System.Collections;

/// <summary>
/// 人工智能
/// </summary>
[RequireComponent(typeof(EnemyAnimation), typeof(EnemyMotor), typeof(EnemyStatusInfo) ) ]
[RequireComponent(typeof(EnemyInspectTower))]

public class EnemyAI : MonoBehaviour
{
    public GameObject frozenOBJ;
    public GameObject dizzOBJ;
    public GameObject bullet;
    public int atkValue = 10;
    private EnemyStatusInfo theSta;
    private EnemyInspectTower theObstaclesInspect;
    public Transform thePlayerTF;
    private float theExecuteRange;
    private float theAtkRange;
    private EnemyAnimation animAction;
    private EnemyMotor motor;
    private float atkTime=0;
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
    private float RecoveyMove;
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
    void Awake()
    {
        theSta = GetComponent<EnemyStatusInfo>();
        theObstaclesInspect = GetComponent<EnemyInspectTower>();
        animAction = GetComponent<EnemyAnimation>();
        motor = GetComponent<EnemyMotor>();
        theExecuteRange =theSta.atkExecuteRange;
        theAtkRange = theSta.atkRange;
        RecoveyMove = motor.moveSpeed;
        RecoveyAtk = atkInterval; 
    }
     void Start()
    {
        thePlayerTF = motor.thePlayerTF;
    }
    /// <summary>
    /// 敌人状态
    /// </summary>
    public State state = State.Run;
    //每帧判断状态
     void Update()
    {
        thePlayerTF = motor.thePlayerTF;
        switch (state)
        {
            case State.Run:
                if(!theObstaclesInspect.IsObstacle)
                Run();
                theObstaclesInspect.MoveForward();
                theObstaclesInspect.Detection();
                break;
            case State.Attack:
                Attack();
                break;
        }
    }
    public void CaculateDamaga()
    {
        
        if (thePlayerTF!=null&& Vector3.Distance(thePlayerTF.position, transform.position) < theAtkRange)
        {
            if (thePlayerTF.tag == Tags.player)
                thePlayerTF.GetComponent<Player>().GetDamage(atkValue);
            else if(thePlayerTF.GetComponent<Tower>()!=null)thePlayerTF.GetComponent<Tower>().GetDamage(atkValue);
        }
    }
    private void Attack()
    {
        
        //限制攻击频率
        //播放攻动画
        if (atkTime <= Time.time)
        {
            if (thePlayerTF != null)
            {
                animAction.Play(animAction.atkName);
                if (bullet != null)
                {
                    GameObject bulletGO = Instantiate(bullet, 
                        transform.position + new Vector3(2, 4, 2), Quaternion.identity) as GameObject;
                    bulletGO.GetComponent<BulletfFy>().ini(thePlayerTF,atkValue);
                }
                else Invoke("CaculateDamaga", delay);
                atkTime = Time.time + atkInterval;
            }
            
        }
        if (!animAction.IsPlaying(animAction.atkName))
        {
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
    public void dizz(float recoveyTime)
    {

        GameObject FxObj = Instantiate(dizzOBJ,
        transform.position + new Vector3(0, 1, 0), Quaternion.identity) as GameObject;
        Destroy(FxObj, recoveyTime);
        motor.moveSpeed =0;
        atkInterval =100;
        animAction.stopAll();
        Invoke("recovey",recoveyTime);
        
    }
    /// <summary>
    /// 冰冻
    /// </summary>
    /// <param name="debuff">减少倍数 整数</param>
    /// <param name="recovetTime">恢复时间 浮点类型</param>
    public void frozen(int debuff,float recoveyTime)
    {
        GameObject FxObj = Instantiate(frozenOBJ,
            transform.position + new Vector3(0, 0, 0), Quaternion.identity) as GameObject;
        Destroy(FxObj, recoveyTime);
        motor.moveSpeed /= debuff;
        atkInterval *= debuff;
        Invoke("recovey", recoveyTime);
    }
    private void recovey()
    {
        motor.moveSpeed = RecoveyMove;
        atkInterval = RecoveyAtk;
    }
}
