using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

/// <summary>
/// 敌人马达，负责敌人运动功能
/// </summary>

public class EnemyMotor : MonoBehaviour
{
    public Transform  thePlayerTF ;
    private float theExecuteRange;
    private float atkRange;
    public string firstTargetTag = Tags.player;
    /// <summary>
    /// 移动速度
    /// </summary>
    public float moveSpeed = 10;
    private void Awake()
    {
        if(thePlayerTF==null)
            if (GameObject.FindGameObjectWithTag(firstTargetTag).transform==false)
                thePlayerTF = thePlayerTF = GameObject.FindGameObjectWithTag(Tags.player).transform;
            else thePlayerTF = GameObject.FindGameObjectWithTag(firstTargetTag).transform;
    }

    void Start()
    {
        theExecuteRange = GetComponent<EnemyStatusInfo>().atkExecuteRange;
        atkRange = GetComponent<EnemyStatusInfo>().atkRange;
    }
    private void Update()
    {
        if (thePlayerTF == null)
        {
            if ((thePlayerTF = GameObject.FindGameObjectWithTag(firstTargetTag).transform) == null)
                thePlayerTF = GameObject.FindGameObjectWithTag(Tags.player).transform;
        }
    }
    //执行寻路 如果攻击进入范围返回F 给状态类切换状态
    public bool run()
    {
        if (thePlayerTF == null) return true;
        if (Vector3.Distance(thePlayerTF.position, transform.position) < atkRange )
        {
            return false;
        }

        LookRotation(thePlayerTF.position);
        
        MovementForward();
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


