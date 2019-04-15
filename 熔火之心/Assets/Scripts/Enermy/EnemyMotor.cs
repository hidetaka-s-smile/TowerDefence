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
    float atkRange = EnemyStatusinfo.atkRange;
    /// <summary>
    /// 移动速度
    /// </summary>
    public float moveSpeed = 10;
    private Transform player;
    private CharacterController theCC;
    private NavMeshAgent theAgent;
    void Start()
    {
        theCC = this.GetComponent<CharacterController>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        theAgent = GetComponent<NavMeshAgent>();
    }

    //执行寻路 如果攻击进入范围返回F 给状态类切换状态
    public bool run()
    {
        Vector3 targetPos = player.position;
        if (Vector3.Distance(player.position, transform.position) < atkRange - atkExecuteRange)
        {
            return false;
        }
        transform.LookAt(targetPos);
        theCC.SimpleMove(transform.forward * moveSpeed * Time.deltaTime);
        return true;
    }
}
