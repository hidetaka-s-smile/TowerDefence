using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TitleUIManager : MonoBehaviour
{
    public AudioClip enterClip;
    public Animation maskAnim;

    private AudioSource audioSource;

    private void Awake()
    {
        audioSource = gameObject.GetComponent<AudioSource>();
    }

    public void OnStartBtn()
    {
        audioSource.PlayOneShot(enterClip);
        maskAnim.Play();
        Invoke("LoadGame", 1.0f);
    }

    public void OnQuitBtn()
    {
        Application.Quit();
    }

    /// <summary>
    /// 异步加载游戏
    /// </summary>
    private void LoadGame()
    {
        SceneManager.LoadSceneAsync(1);
    }


}
