using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 显示游戏胜利界面
/// </summary>
public class GameWinUI : MonoBehaviour
{
    //单例模式
    public static GameWinUI instance;

    public Image frameImg;
    public Animation frameAnim;
    public Image victoryImg;
    public Animation victoryAnim;
    public Text grandPlayerTxt;
    public Animation grandPlayerAnim;
    public Text saveWorldTxt;
    public Animation saveWorldAnim;
    public GameObject endBtn;
    private Animation endBtnAnim;

    private void Awake()
    {
        instance = this;
        endBtnAnim = endBtn.GetComponent<Animation>();
        endBtn.SetActive(false);
    }

    public void Show()
    {
        MaskImg.instance.Show();
        ShowFrame();
        Invoke("ShowVictoryImg", 1.0f);
        Invoke("ShowGrandPlayerTxt", 2.2f);
        Invoke("ShowSaveWorldTxt", 3.2f);
        Invoke("ShowEndBtn", 4.7f);
    }

    private void ShowFrame()
    {
        frameImg.enabled = true;
        frameAnim.Play();
    }

    private void ShowVictoryImg()
    {
        victoryImg.enabled = true;
        victoryAnim.Play();
    }

    private void ShowGrandPlayerTxt()
    {
        grandPlayerTxt.enabled = true;
        grandPlayerAnim.Play();
    }

    private void ShowSaveWorldTxt()
    {
        saveWorldTxt.enabled = true;
        saveWorldAnim.Play();
    }

    private void ShowEndBtn()
    {
        endBtn.SetActive(true);
        endBtnAnim.Play();
    }
}
