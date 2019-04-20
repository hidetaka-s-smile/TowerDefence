using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 滑动式血条
/// </summary>
public class SmoothSlider : MonoBehaviour
{
    [Header("变化速度参数")]
    public float smoothing = 5.0f;

    private float maxValue;//最大值
    private float currentValue;//当前值
    private float targetValue;//目标值
    private Slider slider;

    private void Awake()
    {
        slider = gameObject.GetComponent<Slider>();
    }

    private void Update()
    {
        currentValue = Mathf.Lerp(currentValue, targetValue, Time.deltaTime * smoothing);
        slider.value = currentValue / maxValue;
    }

    /// <summary>
    /// 初始化
    /// </summary>
    /// <param name="hp"></param>
    /// <param name="hpMax"></param>
    public void InitValue(float hp, float hpMax)
    {
        currentValue = hp;
        targetValue = currentValue;
        maxValue = hpMax;       
    }
    /// <summary>
    /// 更改目标值
    /// </summary>
    public void ChangeValue(float newValue)
    {
        targetValue = newValue;
    }
    /// <summary>
    /// 更改最大值
    /// </summary>
    /// <param name="newMaxVal"></param>
    public void ChangeMaxValue(float newMaxVal)
    {
        maxValue = newMaxVal;
    }
    /// <summary>
    /// 获得当前值
    /// </summary>
    public float GetCurVal()
    {
        return currentValue;
    }
}
