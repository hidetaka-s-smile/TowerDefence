using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHpNum : MonoBehaviour
{
    public SmoothSlider slider;
    private float maxHp;

    private Text numText;

    private void Awake()
    {
        numText = gameObject.GetComponent<Text>();
        maxHp = slider.GetMaxVal();
    }

    private void Update()
    {
        numText.text = ((int)slider.GetCurVal()).ToString() + "/" + maxHp.ToString();
    }
}
