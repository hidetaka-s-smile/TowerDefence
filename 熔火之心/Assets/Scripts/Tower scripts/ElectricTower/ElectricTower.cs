using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElectricTower :Tower
{
    public override void Update()
    {
        //如果不在建造中
        if (IsBuilding == false)
        {
            
            timer += Time.deltaTime;
            //攻击
            if (enemys.Count > 0 && timer > attackRateTime)
            {
                timer = 0;
                Attack();
            }
        }
    }
}
                                