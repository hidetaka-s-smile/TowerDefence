using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 炮塔大炮
/// </summary>
public class Boaster : Tower
{
    // Start is called before the first frame update
    void Start()
    {
        //初始化
        buildTime = 4;
        buildCost = 15;
        hp = 100;
        attackRateTime = 3f;
        ad = 5;
    }

    // Update is called once per frame
    //攻击
    public override void Attack()
    {
        //若敌人死亡或离开范围则更新
        if (enemys[0] == null)
        {
            UpdateEnemys();
        }
        //使炮弹实例化且以第一个敌人为目标
        GameObject bullet = GameObject.Instantiate(bulletPrefeb, firepostion.position, firepostion.rotation);
        bullet.GetComponent<BoasterBullet>().SetTarget(enemys[0].transform);
    }
}
