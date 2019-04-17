using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower1 :Tower
{
    // Start is called before the first frame update
    void Start()
    {
        //初始化
        hp = 100;
        attackRateTime = 0.2f;
        ad = 10;
        //这些属性好像做成public之后就可以不用初始化了，不同的预制体可以单独在Inspector面板更改自己的属性
    }

    // Update is called once per frame
    
}
