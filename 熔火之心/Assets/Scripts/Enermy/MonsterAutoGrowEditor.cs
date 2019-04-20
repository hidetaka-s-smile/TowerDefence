using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
///控制怪物生成
/// </summary>
[RequireComponent(typeof(SystemLevelEditor)) ]
public class MonsterAutoGrowEditor : MonoBehaviour
{
    public int creatFlag = 0;
    /// <summary>
    /// 敌人生成延迟时间
    /// </summary>
    public int maxMonsterGrowDelay = 20;
    private int tempDelay;
    private GameObject[] MonsterTypePrefabs;
    /// <summary>
    /// 改关的怪物种类数
    /// </summary>
    private int typeCnt;
    void Start()
    {
        tempDelay = maxMonsterGrowDelay;
        MonsterTypePrefabs = GetComponent<SystemLevelEditor>().monsterType;
    }
    /// <summary>
    /// 生成怪物
    /// </summary>
    public void monsterAutoGrow(int CurrentLevel, int theTypeCnt)
    {
        print(CurrentLevel);
        int theCurrentLevel = CurrentLevel;
        typeCnt = theTypeCnt;
        tempDelay = maxMonsterGrowDelay;
        switch (CurrentLevel)
        {
            case 1:
                creatFlag = 0;
                for (int i = 1; i <= 5; i++)
                {
                    Invoke("CreateEnemy", tempDelay);
                    tempDelay += maxMonsterGrowDelay;
                }
                break;
            case 2:
                for(int i = 1; i <= 6; i++)
                {
                    if (i <= 3)
                    {
                        creatFlag = 0;
                        Invoke("CreateEnemy",tempDelay);
                        tempDelay += maxMonsterGrowDelay;
                    }
                    else
                    {
                        creatFlag = 1;
                        Invoke("CreateEnemy", tempDelay);
                        tempDelay += maxMonsterGrowDelay;
                    }
                }
                break;
            case 3:
                for(int i = 1; i <= 6; i++)
                {
                    int k = (i + 1) / 2;
                    creatFlag = k-1;
                    Invoke("CreateEnemy", tempDelay);
                    tempDelay += maxMonsterGrowDelay;
                }
                break;
            case 4:
                for(int i = 1; i <= 7; i++)
                {
                    int k = (i + 1) / 2;
                    if (k == 4) k--;
                    creatFlag = k ;
                    Invoke("CreateEnemy", tempDelay);
                    tempDelay += maxMonsterGrowDelay;
                }
                break;
        }
        
    }

    public void CreateEnemy()
    {
        GameObject.Instantiate( MonsterTypePrefabs[creatFlag]
            , transform.position + new Vector3(4, 0, 4), Quaternion.identity);
    }
}
