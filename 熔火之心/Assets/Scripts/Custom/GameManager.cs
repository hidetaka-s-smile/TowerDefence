using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 游戏运行状态
/// </summary>
public enum GameStatus
{
    Run,        //正在运行
    Pause,      //暂停
    Win,        //游戏胜利
    GameOver    //游戏结束
}

/// <summary>
/// 游戏管理器，控制游戏进程和UI切换
/// </summary>
public class GameManager : MonoBehaviour
{
    //单例模式
    public static GameManager instance;

    /// <summary>
    /// 游戏运行状态
    /// </summary>
    public GameStatus gameStatus = GameStatus.Run;

    private void Awake()
    {
        instance = this;
    }

    /// <summary>
    /// 游戏是否正在运行 是则返回真
    /// </summary>
    /// <returns></returns>
    public bool IsRunning()
    {
        return gameStatus == GameStatus.Run;
    }

    /// <summary>
    /// 游戏结束事件
    /// </summary>
    public void GameOver()
    {
        gameStatus = GameStatus.GameOver;
        //调用音频管理器播放游戏死亡音乐

        //人物模型倒地动画，游戏停止运行

        //显示死亡界面UI
        ShowGameOverUI();
    }

    /// <summary>
    /// 游戏胜利事件
    /// </summary>
    public void GameWin()
    {
        gameStatus = GameStatus.Win;
        //调用音频管理器播放游戏胜利音乐

        //人物模型胜利动画，游戏停止运行

        //显示胜利界面UI
    }

    /// <summary>
    /// 游戏暂停
    /// </summary>
    public void Pause()
    {

    }

    /// <summary>
    /// 显示玩家死亡界面UI
    /// </summary>
    private void ShowGameOverUI()
    {

    }
}
