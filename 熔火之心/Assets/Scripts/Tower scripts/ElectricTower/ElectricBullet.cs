using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElectricBullet : Bullet
{
    public bool Isdebuff;
    public int debufftime;
    public override void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy" || other.tag == "Boss")
        {
            Die();
            other.GetComponent<EnemyStatusInfo>().Damage(damage);
            //调用敌人受伤方法  伤害值暂时为5
            //冰冻
            if (other.GetComponent<EnemyAI>() != null)
                other.GetComponent<EnemyAI>().dizz(debufftime);
            else other.GetComponent<BossAI>().dizz(debufftime);
            return;
        }
    }
}
