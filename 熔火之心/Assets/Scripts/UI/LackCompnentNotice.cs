using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LackCompnentNotice : MonoBehaviour
{
    public static LackCompnentNotice instance;

    private Text img;
    private Animation anim;

    private void Awake()
    {
        instance = this;
        img = gameObject.GetComponent<Text>();
        anim = gameObject.GetComponent<Animation>();
    }

    public void Show()
    {
        img.enabled = true;
        if(!anim.isPlaying)
            anim.Play();
        Invoke("Hide", 1.0f);
    }

    public void Hide()
    {
        img.enabled = false;
    }
}
