using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

    public void Show()
    {

    }

    private void ShowFrame()
    {
        frameImg.enabled = true;
        
    }
}
