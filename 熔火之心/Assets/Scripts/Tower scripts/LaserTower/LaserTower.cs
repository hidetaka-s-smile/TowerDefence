using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserTower : Tower
{
    public LineRenderer laserReanderer;//激光
    // Start is called before the first frame update
    void Start()
    {
        hp = 100;
    }

    // Update is called once per frame
    /// <summary>
    /// 激光塔
    /// </summary>
    public override void Update()
    {
        if (enemys.Count > 0)
        {
            if(laserReanderer.enabled ==false)
            {
                laserReanderer.enabled = true;
            }
            CanYHeadFollow();
            Attack();
        }
        else
        {
            laserReanderer.enabled = false;
        }
    }
    //攻击
    public override void Attack()
    {
        if (enemys[0] == null)
        {
            UpdateEnemys();
        }
        laserReanderer.SetPositions(new Vector3[] { firepostion.position, enemys[0].transform.position });
        enemys[0].GetComponent<EnemyStatusInfo>().Damage(ad*Time.deltaTime);
    }
}
