using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 需要伤害值
/// </summary>
public class Bullet : MonoBehaviour
{
    public int damage;
    public int speed = 20;
    public Transform target;
    public void SetTarget(Transform _target)
    {
        this.target = _target;
    }
    // Update is called once per frame
    public virtual void Update()
    {
        //如果目标为空或死亡则销毁子弹
        if(target==null)
        {
            Die();
        }
        if(target!=null)
        {
            if (target.tag == "Enemy")
            {
                if (target.GetComponent<EnemyStatusInfo>().Isdead == true)
                    Die();
                transform.LookAt(target.position);
                transform.Translate(Vector3.forward * speed * Time.deltaTime);
            }
            
        }

    }
    public virtual void OnTriggerEnter(Collider other)
    {
        if(other.tag=="Enemy")
        {
            Die();
            //调用敌人受伤方法  伤害值暂时为5
            other.GetComponent<EnemyStatusInfo>().Damage(damage);
            return;
        }
    }
    //销毁子弹
    public void Die()
    {
        Destroy(this.gameObject);
    }
        
}
