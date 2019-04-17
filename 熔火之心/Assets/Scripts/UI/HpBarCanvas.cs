using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HpBarCanvas : MonoBehaviour
{
    private void Update()
    {
        //面朝摄像机
        transform.LookAt(Camera.main.transform);
    }
}
