using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

/// <summary>
/// 敌人马达，负责敌人运动功能
/// </summary>

public class EnemyMotor : MonoBehaviour
{
    /// <summary>
    /// 敌人执行攻击动作的范围与伤害距离的差距
    /// </summary>
    public float atkExecuteRange = 5;
    float atkRange;
    /// <summary>
    /// 移动速度
    /// </summary>
    public float moveSpeed = 10;
    private Transform player;
    //private CharacterController theCC;
    //private NavMeshAgent theAgent;
    void Start()
    {
        atkRange = GetComponent<EnemyStatusinfo>().atkRange;
        //theCC = this.GetComponent<CharacterController>();
        player = GameObject.FindGameObjectWithTag("player").transform;
        //theAgent = GetComponent<NavMeshAgent>();
    }

    //执行寻路 如果攻击进入范围返回F 给状态类切换状态
    public bool run()
    {
        Vector3 targetPos = player.position;
        if (Vector3.Distance(player.position, transform.position) < atkRange - atkExecuteRange)
        {
            return false;
        }
        LookRotation(targetPos);
        MovementForward();
        //theCC.SimpleMove(transform.forward * moveSpeed * Time.deltaTime);
        return true;
    }
    /// <summary>
    /// 向前移动
    /// </summary>
    public void MovementForward()
    {
        transform.Translate(0, 0, moveSpeed * Time.deltaTime);
    }

    /// <summary>
    /// 朝向目标点的旋转
    /// </summary>
    /// <param name="targetPos">目标位置</param> 
    public void LookRotation(Vector3 targetPos)
    {
        //暂时……  一帧旋转至目标方位
        transform.LookAt(targetPos);
    }
}
