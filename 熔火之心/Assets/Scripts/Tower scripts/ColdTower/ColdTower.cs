using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColdTower : Tower
{
    // Start is called before the first frame update
    void Start()
    {
        //初始化
        buildTime = 4;
        buildCost = 15;
        hp = 100;
        attackRateTime = 1f;
        ad = 5;
    }

    // Update is called once per frame
    public override void Attack()
    {
        if (enemys[0].GetComponent<EnemyStatusInfo>().Isdead == true)
        {
            UpdateEnemys();
        }
        GameObject bullet = GameObject.Instantiate(bulletPrefeb, firepostion.position, firepostion.rotation);
        bullet.GetComponent<ColdBullet>().SetTarget(enemys[0].transform);
    }
}
