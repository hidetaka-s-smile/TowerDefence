using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tower : MonoBehaviour
{
    public bool IsBuilding;  //是否在建造中
    public int buildTime;  //建造时间
    public int buildCost;  //建造费用
    public int hp;             //血量
    public int hpMax;          //最大血量
    public int ad;               //攻击力
    public GameObject bulletPrefeb; //子弹
    public float attackRateTime;//攻击间隔
    public float timer = 0;        //计时器
    public Transform firepostion;   //子弹初始位置
    public Transform head;          //发射子弹的头部
    public List<GameObject> enemys = new List<GameObject>();//可攻击敌人的存放数组
    public Slider hpBarSlider;//血条UI

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
        //如果不在建造中
        if(IsBuilding==true)
        {
            //塔的头部跟随敌人
            if (enemys.Count > 0)
            {
                CantYHeadFollow();
            }
            timer += Time.deltaTime;
            //攻击
            if (enemys.Count > 0 && timer > attackRateTime)
            {
                timer = 0;
                Attack();
            }
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
        //要不要做成当检测到自己被攻击时，切换目标优先攻击这个敌人？自己把握一下时间看看要不要实现这个AI,这种大改动做前记得备份脚本噢
        //可以用下面那个受到伤害的方法来获取伤害来源（加个参数，让敌人把自己传递过来,做炮塔记得要和做敌人的世祥多沟通，让战斗交互更智能）
    }

    /// <summary>
    /// 受到伤害
    /// </summary>
    /// <param name="damage">伤害值</param>
    public void GetDamage(int damage)
    {
        hp -= damage;
        //反应在血条UI上
        hpBarSlider.value = hp / hpMax;
        //播放被敲打的音效

        if (hp <= 0)
        {
            BeDestroyed();
        }
    }

    /// <summary>
    /// 被摧毁事件(可被玩家调用主动拆除)
    /// </summary>
    public void BeDestroyed()
    {
        //播放摧毁音效和动画

        //根据该塔的零件需求的50%实例化掉落零件（和JJ沟通）

        //毁灭该物体
        Destroy(gameObject);
    }
}
