using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// BOSS状态栏UI
/// </summary>
public class BossStatusUI : MonoBehaviour
{
    public static BossStatusUI instance;

    private void Awake()
    {
        instance = this;
        gameObject.SetActive(false);
    }

    /// <summary>
    /// 显示状态栏
    /// </summary>
    public void ShowStatus()
    {
        gameObject.SetActive(true);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }
}
