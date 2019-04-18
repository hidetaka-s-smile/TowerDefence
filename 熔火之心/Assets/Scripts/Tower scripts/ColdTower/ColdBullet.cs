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
            
            //调用敌人受伤方法  伤害值暂时为5
            //冰冻
            other.GetComponent<EnemyAI>().frozen(debuff, debufftime);
            other.GetComponent<EnemyStatusInfo>().Damage(damage);
            Die();
            
            return;
        }
    }

}