using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColdBullet : Bullet
{
    public bool Isdebuff;
    public int debuff;//
    public int debufftime;
    public override void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy")
        {
            Die();
            other.GetComponent<EnemyStatusInfo>().Damage(damage);
            //调用敌人受伤方法  伤害值暂时为5
            //冰冻
            if(other.GetComponent<EnemyAI>()!=null)
            other.GetComponent<EnemyAI>().frozen(debuff, debufftime);
            else other.GetComponent<BossAI>().frozen(debuff, debufftime);
            return;
        }
    }

}