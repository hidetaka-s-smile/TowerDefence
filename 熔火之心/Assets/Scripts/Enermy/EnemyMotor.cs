﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

/// <summary>
/// 敌人马达，负责敌人运动功能
/// </summary>

public class EnemyMotor : MonoBehaviour
{
    private Transform  thePlayerTF ;
    //private  Transform  thePlayer;
    private float theExecuteRange;
    private float atkRange;
    /// <summary>
    /// 移动速度
    /// </summary>
    public float moveSpeed = 10;

    //private CharacterController theCC;
    //private NavMeshAgent theAgent;
    private void Awake()
    {
<<<<<<< HEAD
        thePlayerTF = GameObject.FindGameObjectWithTag("Player").transform;
=======
        thePlayerTF = GameObject.FindGameObjectWithTag(Tags.player).transform;
>>>>>>> 3c7faffe5525b19fcb6c3f6ce45f0e83fd0b6a06
    }
    void Start()
    {
        theExecuteRange = GetComponent<EnemyStatusInfo>().atkExecuteRange;
        atkRange = GetComponent<EnemyStatusInfo>().atkRange;
        //theCC = this.GetComponent<CharacterController>();

        //theAgent = GetComponent<NavMeshAgent>();
    }
    

    //执行寻路 如果攻击进入范围返回F 给状态类切换状态
    public bool run()
    {
        
        if (Vector3.Distance(thePlayerTF.position, transform.position) < atkRange - theExecuteRange)
        {
            return false;
        }
        LookRotation(thePlayerTF.position);
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
