using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Burner : MonoBehaviour
{
    public static Burner instance;

    public GameObject gear;
    [Header("生产零件的个数")]
    public int creatNum;

    private void Awake()
    {
        instance = this;
    }

    /// <summary>
    /// 每一波结束生产零件
    /// </summary>
    public void Creat()
    {
        GameObject NewGear = GameObject.Instantiate(gear, new Vector3(transform.position.x, transform.position.y, transform.position.z - 1f), Quaternion.Euler(0.0f, 0.0f, 90.0f)) as GameObject;
        NewGear.GetComponent<Gear>().num = creatNum;
    }
}
