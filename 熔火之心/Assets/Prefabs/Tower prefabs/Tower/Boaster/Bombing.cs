using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 炮弹打中后生成爆炸特效
/// </summary>

public class Bombing : MonoBehaviour
{
    public int damage;//爆炸范围伤害50
    //若敌人在爆炸范围内则受伤
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy")
        {
            Die();
            other.GetComponent<EnemyStatusInfo>().Damage(damage);
            return;
        }
    }
    //销毁子弹
    public void Die()
    {
        Destroy(this.gameObject,1);
    }
}
