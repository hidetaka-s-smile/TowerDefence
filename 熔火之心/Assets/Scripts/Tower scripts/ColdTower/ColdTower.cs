using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColdTower : Tower
{
    // Update is called once per frame
    public override void Attack()
    {
        GameObject bullet = GameObject.Instantiate(bulletPrefeb, firepostion.position, firepostion.rotation);
        bullet.GetComponent<ColdBullet>().SetTarget(enemys[0].transform);
        if ((enemys[0] == null || enemys[0].GetComponent<EnemyStatusInfo>().Isdead == true) && enemys.Count != 0)
        {
            UpdateEnemys();
        }
    }
}
