using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Boss来临UI
/// </summary>
public class BossLevelTxt : MonoBehaviour
{
    //单例模式
    public static BossLevelTxt instance;

    private Animation anim;

    private void Awake()
    {
        instance = this;
        anim = gameObject.GetComponent<Animation>();
    }

    public void Show()
    {
        anim.Play();
    }
}
