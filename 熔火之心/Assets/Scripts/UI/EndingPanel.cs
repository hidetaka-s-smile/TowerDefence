using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndingPanel : MonoBehaviour
{
    public Animation thx1Anim;
    public Animation thx2Anim;
    public Animation thx3Anim;
    public Animation endAnim;
    public GameObject byeBtn;

    private void Awake()
    {
        byeBtn.SetActive(false);
        Show();
    }

    private void Show()
    {
        Invoke("ShowThx1", 20.0f);
        Invoke("ShowThx2", 22.0f);
        Invoke("ShowThx3", 24.0f);
        Invoke("ShowEnd", 26.0f);
        Invoke("ShowByeBtn", 30.0f);
    }

    private void ShowEnd()
    {
        endAnim.Play();
    }

    private void ShowThx1()
    {
        thx1Anim.Play();
    }

    private void ShowThx2()
    {
        thx2Anim.Play();
    }

    private void ShowThx3()
    {
        thx3Anim.Play();
    }

    private void ShowByeBtn()
    {
        byeBtn.SetActive(true);
    }

    /// <summary>
    /// 退出游戏
    /// </summary>
    public void OnByeButton()
    {
        Application.Quit();
    }
}
