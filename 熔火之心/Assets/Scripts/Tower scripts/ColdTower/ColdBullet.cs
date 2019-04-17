using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColdBullet : MonoBehaviour
{
    public int speed = 20;
    private Transform target;
    public void SetTarget(Transform _target)
    {
        this.target = _target;
    }
    // Update is called once per frame
    void Update()
    {
        //如果目标为空或死亡则销毁子弹
        if (target == null)
        {
            Die();
        }
        transform.LookAt(target.position);
        transform.Translate(Vector3.forward * speed * Time.deltaTime);

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy")
        {
            Die();
            //调用敌人受伤方法  伤害值暂时为5
            GetComponent<EnemyStatusInfo>().Damage(5);
            return;
        }
    }
    //销毁子弹
    public void Die()
    {
        Destroy(this.gameObject);
    }

}