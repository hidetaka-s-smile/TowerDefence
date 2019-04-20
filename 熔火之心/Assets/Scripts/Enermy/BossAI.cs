using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 人工智能
/// </summary>
[RequireComponent(typeof(EnemyAnimation), typeof(EnemyMotor), typeof(EnemyStatusInfo))]
[RequireComponent(typeof(EnemyInspectTower))]
public class BossAI : MonoBehaviour
{ 
    public GameObject fireOBJ;
    public int atkValue = 10;
    private EnemyStatusInfo theSta;
    private EnemyInspectTower theObstaclesInspect;
    public Transform thePlayerTF;
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
    private void Awake()
    {
        theSta = GetComponent<EnemyStatusInfo>();
        theObstaclesInspect = GetComponent<EnemyInspectTower>();
        animAction = GetComponent<EnemyAnimation>();
        motor = GetComponent<EnemyMotor>();
        theExecuteRange = theSta.atkExecuteRange;
        theAtkRange = theSta.atkRange;
        RecoveyMove = motor.moveSpeed;
        RecoveyAtk = atkInterval;
    }
    private void Start()
    {
        thePlayerTF = motor.thePlayerTF;
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
                if (!theObstaclesInspect.IsObstacle)
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

        if (thePlayerTF != null && Vector3.Distance(thePlayerTF.position, transform.position) < theAtkRange)
        {
            if (thePlayerTF.tag == Tags.player)
                thePlayerTF.GetComponent<Player>().GetDamage(atkValue);
            else if (thePlayerTF.GetComponent<Tower>() != null) thePlayerTF.GetComponent<Tower>().GetDamage(atkValue);
        }
    }
    private void Attack()
    {
        int FireChance = Random.Range(0, 4);
        //限制攻击频率
        //播放攻动画
        if (atkTime <= Time.time)
        {
            if (thePlayerTF != null)
            {
                if (FireChance <= 1)
                {

                    GameObject[] players = GameObject.FindGameObjectsWithTag(Tags.tower);
                    transform.LookAt(players[0].transform.position);
                    GameObject fireFX = Instantiate(fireOBJ,
                            GameObject.FindGameObjectWithTag("head").transform.position
                            + new Vector3(10, -10, 0), Quaternion.identity) as GameObject;
                    animAction.Play(animAction.fireName);

                    fireFX.transform.LookAt(players[0].transform.position);

                    Destroy(fireFX, 0.3f);
                    foreach (GameObject player in players)
                    {
                        if (CalSec(player.transform))
                        {
                            //if (player.GetComponent<Player>().tag == Tags.player)
                            //player.GetComponent<Player>().GetDamage(atkValue / 2);
                            //else 
                            player.GetComponent<Tower>().GetDamage(atkValue / 2);
                        }
                    }
                    atkTime = Time.time + atkInterval; 
                }
                else
                {
                    transform.LookAt(thePlayerTF.transform.position);
                    animAction.Play(animAction.atkName);
                    Invoke("CaculateDamaga", delay);

                    atkTime = Time.time + atkInterval;
                }

            }
            if (!animAction.IsPlaying(animAction.atkName))
            {
                animAction.Play(animAction.idleName);
            }
            if (motor.run()) state = State.Run;
        }
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
    public void frozen(int debuff, float recovetTime)
    {
        motor.moveSpeed /= debuff;
        atkInterval *= recovetTime;
        Invoke("recovey", recovetTime);
    }
    private void recovey()
    {
        motor.moveSpeed = RecoveyMove;
        atkInterval = RecoveyAtk;
        print("a");
    }
    public bool CalSec(Transform target)
    {
        Transform Target = target; 
        float SkillDistance = 2 * theAtkRange;//扇形距离
        float SkillAng = 60;//扇形的角度
        float distance = Vector3.Distance(transform.position, Target.position);//距离
        Vector3 norVec = transform.rotation * Vector3.forward * 5;
        Vector3 temVec = Target.position - transform.position;
        Debug.DrawLine(transform.position, norVec, Color.red);//画出技能释放者面对的方向向量
        Debug.DrawLine(transform.position, Target.position, Color.green);//画出技能释放者与目标点的连线
        float angle = Mathf.Acos(Vector3.Dot(norVec.normalized, temVec.normalized)) * Mathf.Rad2Deg;
        if (distance < SkillDistance && angle < SkillAng * 0.5f)
        {
            Debug.Log("在扇形范围内");
            return true;
        }
        else return false;
    }

}
        
        




    

    

