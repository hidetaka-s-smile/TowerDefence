using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 关卡数提示字体
/// </summary>
public class LevelNumTxt : MonoBehaviour
{
    public static LevelNumTxt instance;

    private Animation anim;
    private Text numText;

    private void Awake()
    {
        instance = this;
        anim = gameObject.GetComponent<Animation>();
        numText = gameObject.GetComponent<Text>();
    }

    /// <summary>
    /// 显示当前关卡数
    /// </summary>
    /// <param name="num"></param>
    public void ShowLevelNum(int num)
    {       
        switch (num)
        {
            case 1:
                numText.text = "第一波";
                break;
            case 2:
                numText.text = "第二波";
                break;
            case 3:
                numText.text = "第三波";
                break;
            case 4:
                numText.text = "第四波";
                break;
            case 5:
                numText.text = "第五波";
                break;
            default:
                numText.text = "再来一波";
                break;
        }
        print(numText.text);
        numText.enabled = true;
        anim.Play();
        Invoke("Hide", 1.2f);
    }

    private void Hide()
    {
        numText.enabled = false;
    }
}
