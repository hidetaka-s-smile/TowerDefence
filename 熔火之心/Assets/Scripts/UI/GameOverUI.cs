using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 玩家死亡UI
/// </summary>
public class GameOverUI : MonoBehaviour
{
    public static GameOverUI instance;

    public Image blackMask;//黑色遮罩
    public Image defeatImg;//失败图标
    public Animation defeatAnim;//失败图标动画
    public Animation blackMaskAnim;//黑色遮罩动画
    public GameObject titleBtn;//返回标题界面按钮

    private void Awake()
    {
        instance = this;
        titleBtn.SetActive(false);
    }

    /// <summary>
    /// 播放玩家死亡UI系列动画
    /// </summary>
    public void Show()
    {
        ShowBlackMask();
        Invoke("ShowDefeatImg", 2.0f);
        Invoke("ShowTitleBtn", 4.5f);
    }

    private void ShowBlackMask()
    {
        blackMask.enabled = true;
        blackMaskAnim.Play();
    }

    private void ShowDefeatImg()
    {
        defeatImg.enabled = true;
        defeatAnim.Play();
    }

    private void ShowTitleBtn()
    {
        titleBtn.SetActive(true);
    }
}
