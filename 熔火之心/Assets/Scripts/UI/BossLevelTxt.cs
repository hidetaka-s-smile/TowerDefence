using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Boss来临UI
/// </summary>
public class BossLevelTxt : MonoBehaviour
{
    //单例模式
    public static BossLevelTxt instance;

    private Animation anim;
    private Image img;

    private void Awake()
    {
        instance = this;
        anim = gameObject.GetComponent<Animation>();
        img = gameObject.GetComponent<Image>();
    }

    public void Show()
    {
        img.enabled = true;
        anim.Play();
        Invoke("Hide", 3.0f);
    }

    public void Hide()
    {
        img.enabled = false;
    }
}
