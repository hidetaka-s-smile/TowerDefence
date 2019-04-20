using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 音频管理器
/// </summary>
public class AudioManager : MonoBehaviour
{
    public static AudioManager _instance;

    public Slider slider;
    public AudioClip btnClip;

    private AudioSource audioSource;

    private void Awake()
    {
        _instance = this;
        audioSource = gameObject.GetComponent<AudioSource>();
    }

    /// <summary>
    /// 播放按钮音效
    /// </summary>
    public void PlayButtonClip()
    {
        audioSource.PlayOneShot(btnClip);
    }

    /// <summary>
    /// 播放指定2D音效
    /// </summary>
    /// <param name="clip"></param>
    public void Play2DClip(AudioClip clip)
    {
        audioSource.PlayOneShot(clip);
    }
    
    /// <summary>
    /// 滑动条调整音量
    /// </summary>
    public void ControlVolume()
    {
        audioSource.volume = slider.value;
    }
}
