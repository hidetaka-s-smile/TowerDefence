using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MaskImg : MonoBehaviour
{
    public static MaskImg instance;

    private Image img;

    private void Awake()
    {
        instance = this;
        img = gameObject.GetComponent<Image>();
    }

    /// <summary>
    /// 显示遮罩
    /// </summary>
    public void Show()
    {
        img.enabled = true;
    }

    /// <summary>
    /// 隐藏遮罩
    /// </summary>
    public void Hide()
    {
        img.enabled = false;
    }
}
