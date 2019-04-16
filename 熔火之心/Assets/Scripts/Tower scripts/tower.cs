using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    public float hp;             //血量
    public float ad;               //攻击力
    public GameObject bulletPrefeb; //子弹
    public float attackRateTime;//攻击间隔
    public float timer = 0;        //计时器
    public Transform firepostion;   //子弹初始位置
    public Transform head;          //发射子弹的头部
    public List<GameObject> enemys = new List<GameObject>();//可攻击敌人的存放数组
    /// <summary>
    /// 触发器判断是否有敌人
    /// </summary>
    /// <param name="other"></param>
    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy")
        {
            enemys.Add(other.gameObject);
        }
    }
    public void OnTriggerExit(Collider other)
    {
        if (other.tag == "Enemy")
        {
            enemys.Remove(other.gameObject);
        }
    }
    void Start()
    {
        
    }
    public virtual void Update()
    {
        //塔的头部跟随敌人
        if (enemys.Count > 0 )
        {
            CantYHeadFollow();
        }
        timer += Time.deltaTime;
        //攻击
        if(enemys.Count>0&&timer>attackRateTime)
        {
            timer =0;
            Attack();
        }
    }
    //攻击
    public virtual void Attack()
    {
        if(enemys[0]==null)
        {
            UpdateEnemys();
        }
        GameObject bullet= GameObject.Instantiate(bulletPrefeb, firepostion.position, firepostion.rotation);
        bullet.GetComponent<Bullet>().SetTarget(enemys[0].transform);
    }
    /// <summary>
    /// 头部可随y轴旋转摆动跟随敌人
    /// </summary>
    public void CanYHeadFollow()
    {
        Vector3 targetPosition = enemys[0].transform.position;
        head.LookAt(enemys[0].transform.position);
    }
    /// <summary>
    /// 头部不可随y轴旋转跟随敌人
    /// </summary>
    public void CantYHeadFollow()
    {
        Vector3 targetPosition = enemys[0].transform.position;
        Vector3 dir = targetPosition - transform.position;
        dir.y = 0;
        float angle = Vector3.Angle(dir, new Vector3(0, 0, 1));
        angle *= Mathf.Sign(dir.x);
        head.localEulerAngles = new Vector3(0, angle, 0);

    }
    //更新敌人
    //当enemys[0]为null时，更新敌人
    public void UpdateEnemys()
    {
        List<int> emptyIndex = new List<int>();
        for(int index=0;index<enemys.Count;index++)
        {
            if(enemys[index]==null)
            {
                emptyIndex.Add(index);
            }
        }
        for(int i=0;i<emptyIndex.Count;i++)
        {
            enemys.RemoveAt(emptyIndex[i] - i);
        }
    }
    /// <summary>
    /// 被攻击
    /// </summary>
    public void UnderAttack()
    {

    }
}
