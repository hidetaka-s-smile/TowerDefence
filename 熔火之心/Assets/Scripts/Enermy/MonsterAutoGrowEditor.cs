using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
///控制怪物生成
/// </summary>
[RequireComponent(typeof(SystemLevelEditor)) ]
public class MonsterAutoGrowEditor : MonoBehaviour
{
    /// <summary>
    /// 敌人生成延迟时间
    /// </summary>
    public int maxMonsterGrowDelay = 1;
    private GameObject[] MonsterTypePrefabs;
    /// <summary>
    /// 要生成敌人的数量
    /// </summary>
    private int maxCnt;
    /// <summary>
    /// 改关的怪物种类数
    /// </summary>
    private int typeCnt;
    void Start()
    {

        MonsterTypePrefabs = GetComponent<SystemLevelEditor>().monsterType;
    }
    /// <summary>
    /// 生成怪物
    /// </summary>
    public void CreateEnemy()
    {
        int tempRandomIndex = Random.Range(0, typeCnt);
        GameObject.Instantiate(MonsterTypePrefabs[tempRandomIndex] ,transform.position+new Vector3(1,1,1), Quaternion.identity);
    }

    public void monsterAutoGrow(int theMaxCnt, int theTypeCnt)
    {
        maxCnt = theMaxCnt;
        typeCnt = theTypeCnt;
        int nowCnt = 0;///当前关卡敌人生成数
        while (nowCnt < maxCnt)
        {
            nowCnt++;
            Invoke("CreateEnemy", maxMonsterGrowDelay*Time.deltaTime);
        }
    }




}
