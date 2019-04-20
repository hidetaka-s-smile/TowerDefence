using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserTower : Tower
{
    public LineRenderer laserReanderer;//激光


    // Update is called once per frame
    /// <summary>
    /// 激光塔
    /// </summary>
    public override void Update()
    {
        if(IsBuilding==false)
        {
            if (enemys.Count > 0)
            {
                if (laserReanderer.enabled == false)
                {
                    laserReanderer.enabled = true;
                }
                CanYHeadFollow();
                if (enemys.Count > 0&&enemys[0].gameObject!=null)
                {
                    if(enemys[0].GetComponent<EnemyStatusInfo>().Isdead == false)
                       Attack();
                }
            }
            else
            {
                bulletPrefeb.SetActive(false);
                laserReanderer.enabled = false;
            }
        }
    }
    //攻击
    public override void Attack()
    {
        if ( enemys.Count != 0 && ( enemys[0].GetComponent<EnemyStatusInfo>().Isdead == true) || enemys[0] == null )
        {
            UpdateEnemys();
        }
        laserReanderer.SetPositions(new Vector3[] { firepostion.position, enemys[0].transform.position });
        bulletPrefeb.SetActive(true);
        enemys[0].GetComponent<EnemyStatusInfo>().Damage(ad*Time.deltaTime);
    }
}
