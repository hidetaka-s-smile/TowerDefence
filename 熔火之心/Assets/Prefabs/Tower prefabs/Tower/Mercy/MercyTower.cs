using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MercyTower : Tower
{
    public GameObject bulletrocket;
    public GameObject circle1;
    public GameObject circle2;
    public GameObject circle3;
    public GameObject ball1;
    public GameObject ball2;
    public GameObject ball3;
    public override void Update()
    {
        //如果不在建造中
        if (IsBuilding == false)
        {
            if(timer>7.05)
            {
                timer = 0;
            }
            //塔的头部跟随敌人
            if (enemys.Count > 0)
            {
                CantYHeadFollow();
            }
            timer += Time.deltaTime;
            //攻击
            if (enemys.Count > 0 && timer > 1)
            {
                ball1.SetActive(true);
                circle1.SetActive(true);
            }
            if (enemys.Count > 0 && timer > 3)
            {
                ball2.SetActive(true);
                circle2.SetActive(true);
            }
            if (enemys.Count > 0 && timer > 5)
            {
                ball3.SetActive(true);
                circle3.SetActive(true);
            }
            if (enemys.Count > 0 && timer > 7)
            {
                Attack();
                ball1.SetActive(false);
                circle1.SetActive(false);
                ball2.SetActive(false);
                circle2.SetActive(false);
                ball3.SetActive(false);
                circle3.SetActive(false);
                timer = 0;
            }
        }
    }
    public override void Attack()
    {
        if ((enemys[0] == null || enemys[0].GetComponent<EnemyStatusInfo>().Isdead == true) && enemys.Count != 0)
        {
            UpdateEnemys();
        }
        GameObject bullet1 = GameObject.Instantiate(bulletPrefeb, firepostion.position, firepostion.rotation);
        GameObject rocket= GameObject.Instantiate(bulletrocket, firepostion.position, firepostion.rotation);
        if (enemys[0] != null)
        {
            bullet1.GetComponent<MercyBullet>().SetTarget(enemys[0].transform);
            rocket.GetComponent<MercyBullet>().SetTarget(enemys[0].transform);
        }
    }
}
