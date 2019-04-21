using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class EnemyInspectTower:MonoBehaviour
{

    public float MoveSpeed = 2; //避开时的移动速度
    public float ColliderDistance = 2; //检测距离

    public string[] ObstacleTags= { Tags.enemy, Tags.player }; //障碍物的标签,就是要避开的障碍物
    public bool IsObstacle { get { return _IsObstacle; } } //是否有障碍物，用来给外部的接口

    private Vector3 pos; //记录避开障碍物前的的旋转坐标

    private bool IsForward = false; //前方是否有障碍物
    private bool IsInLeft = false; //左方是否有障碍物
    private bool IsInRight = false; //右方是否有障碍物
    private bool _IsObstacle = false; //是否有障碍物

    private float timer = 0; //避开障碍物后返回原先状态的缓冲计时

    private Transform m_transform;
    // Use this for initialization
    void Awake()
    {
        m_transform = transform;

    }
    // Update is called once per frame
    void Update()
    {

    }
    //当侧面有障碍物而前方没有时向前移动
    public void MoveForward()
    {
        if (_IsObstacle)
        {
            if (!IsForward)
            {
                pos = m_transform.localEulerAngles;
                m_transform.Translate(Vector3.forward * Time.deltaTime * MoveSpeed);
            }
        }
    }
    //障碍物检测
    #region Detection
    public void Detection()
    {
        IsForward = DirectionForward();
        IsInLeft = DirectionLeft();
        IsInRight = DirectionRight();

        if (IsForward)
        {
            if (IsInLeft && !IsInRight)
            {
                float rotateAnglesY = RotateAngles(pos.y + 90);
                m_transform.localEulerAngles = new Vector3(m_transform.localEulerAngles.x,
                                                           rotateAnglesY,
                                                           m_transform.localEulerAngles.z);
            }
            else if (!IsInLeft && IsInRight)
            {
                float rotateAnglesY = RotateAngles(pos.y - 90);
                m_transform.localEulerAngles = new Vector3(m_transform.localEulerAngles.x,
                                                           rotateAnglesY,
                                                           m_transform.localEulerAngles.z);
            }
            else if (IsInLeft && IsInRight)
            {
                float rotateAnglesY = RotateAngles(pos.y + 180);
                m_transform.localEulerAngles = new Vector3(m_transform.localEulerAngles.x,
                                                           rotateAnglesY,
                                                           m_transform.localEulerAngles.z);
            }
            else
            {
                float rotateAnglesY = RotateAngles(pos.y + Random_1_1() * 90);
                m_transform.localEulerAngles = new Vector3(m_transform.localEulerAngles.x,
                                                           rotateAnglesY,
                                                           m_transform.localEulerAngles.z);
            }
        }
        else
        {
            if (!IsInLeft && !IsInRight)
            {
                if (IsObstacle)
                {
                    //避开障碍物到返回原先状态的缓冲时间
                    timer += Time.deltaTime;
                    if (timer > 0.4f)
                    {
                        timer = 0;
                        _IsObstacle = false;
                    }
                }
            }
        }
    }
    //检测前方
    bool DirectionForward()
    {
        bool check = false;
        RaycastHit hitForward;
        Vector3 LocalForward = m_transform.TransformPoint(Vector3.forward) - m_transform.position;
        if (Physics.Raycast(m_transform.position,
                             m_transform.forward,
                             out hitForward, ColliderDistance / 2))
        {
            if (CompareTags(hitForward.transform.gameObject.tag))
            {
                _IsObstacle = true;
                check = true;
            }
        }
        return check;
    }
    //检测右方
    bool DirectionRight()
    {
        bool check = false;
        RaycastHit hitRight;
        Vector3 LocalRight = m_transform.TransformPoint(Vector3.right) - m_transform.position;
        if (Physics.Raycast(m_transform.position,
                             LocalRight,
                             out hitRight, ColliderDistance/2))
        {
            if (CompareTags(hitRight.transform.gameObject.tag))
            {
                _IsObstacle = true;
                check = true;
            }
        }
        return check;
    }
    //检测左方
    bool DirectionLeft()
    {
        bool check = false;
        RaycastHit hitLeft;
        Vector3 LocalLeft = m_transform.TransformPoint(-Vector3.right) - m_transform.position;
        if (Physics.Raycast(m_transform.position,
                             LocalLeft,
                             out hitLeft, ColliderDistance/2))
        {
            if (CompareTags(hitLeft.transform.gameObject.tag))
            {
                _IsObstacle = true;
                check = true;
            }
        }
        return check;
    }
    #endregion
    //自己写的一些小功能
    #region MyFunction
    //遍历ObstacleTags数组，是否与其中的一个值相符
    bool CompareTags(string name)
    {
        bool isSame = false;
        for (int i = 0; i < ObstacleTags.Length; i++)
        {
            if (ObstacleTags[i].Equals(name))
            {
                isSame = true;
                break;
            }
        }
        return isSame;
    }
    //将角度规范，变为0~360之间
    float RotateAngles(float Angles)
    {
        float angles = 0;
        if (Angles >= 0)
        {
            angles = Angles - ((int)(Angles / 360)) * 360;
        }
        else
        {
            angles = (((int)(Angles / 360)) + 1) * 360 - Angles;
        }
        return angles;
    }
    //产生-1~1的随机数，不包括0
    float Random_1_1()
    {
        float index = Random.Range(-1, 1);
        if (index == 0)
        {
            index = Random_1_1();
        }
        return index;
    }
    #endregion
    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        //Gizmos.DrawLine(transform.position, transform.TransformPoint(Vector3.forward * ColliderDistance / 2));
        //Gizmos.DrawLine(transform.position, transform.TransformPoint(Vector3.right * ColliderDistance));
        //Gizmos.DrawLine(transform.position, transform.TransformPoint(Vector3.left * ColliderDistance));
    }

}
