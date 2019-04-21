using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 音频管理器
/// </summary>
public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;
    //Clips
    public Slider slider;
    public AudioClip btnClip;
    public AudioClip settingClip;
    public AudioClip bluePrintClip;
    public AudioClip defeatClip;
    public AudioClip winClip;
    public AudioClip bossRoarClip;
    //BGM
    public AudioSource battleBGM;
    public AudioSource bossBGM;
    public AudioSource bossCreazyBGM;
    public AudioSource audioSourceEffect;//音效的音频源

    private AudioSource currentAudioSource;//当前的bgm
    
    private void Awake()
    {
        instance = this;
        currentAudioSource = battleBGM;
    }

    /// <summary>
    /// 播放按钮音效
    /// </summary>
    public void PlayButtonClip()
    {
        audioSourceEffect.PlayOneShot(btnClip);
    }

    /// <summary>
    /// 播放蓝图界面音效
    /// </summary>
    public void PlayBluePrintClip()
    {
        audioSourceEffect.PlayOneShot(bluePrintClip);
    }

    /// <summary>
    /// 播放设置界面出现音效
    /// </summary>
    public void PlaySettingClip()
    {
        audioSourceEffect.PlayOneShot(settingClip);
    }

    /// <summary>
    /// 播放失败界面音乐
    /// </summary>
    public void PlayDefeatClip()
    {
        currentAudioSource.Stop();
        audioSourceEffect.PlayOneShot(defeatClip);
    }

    /// <summary>
    /// 播放胜利界面音乐
    /// </summary>
    public void PlayWinClip()
    {
        currentAudioSource.Stop();
        audioSourceEffect.PlayOneShot(winClip);
    }

    /// <summary>
    /// 播放指定2D音效
    /// </summary>
    /// <param name="clip"></param>
    public void Play2DClip(AudioClip clip)
    {
        audioSourceEffect.PlayOneShot(clip);
    }
    
    /// <summary>
    /// 滑动条调整音量
    /// </summary>
    public void ControlVolume()
    {
        currentAudioSource.volume = slider.value;
    }

    /// <summary>
    /// 播放boss战音乐
    /// </summary>
    public void PlayBossBGM()
    {
        currentAudioSource.Stop();
        bossBGM.Play();
        currentAudioSource = bossBGM;
    }

    /// <summary>
    /// boss进入狂暴阶段音乐
    /// </summary>
    public void PlayCreazyBossBGM()
    {
        currentAudioSource.Stop();
        bossCreazyBGM.Play();
        currentAudioSource = bossCreazyBGM;
    }

    public void PlayBossRoarClip()
    {
        audioSourceEffect.PlayOneShot(bossRoarClip);
    }
}
