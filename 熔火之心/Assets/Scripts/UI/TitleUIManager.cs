using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TitleUIManager : MonoBehaviour
{
    public AudioClip enterClip;
    public Animation maskAnim;
    public Image maskImg;
    public Text loadingTxt;
    public Animation loadingAnim;

    private AudioSource audioSource;

    private void Awake()
    {
        audioSource = gameObject.GetComponent<AudioSource>();
    }

    public void OnStartBtn()
    {
        audioSource.PlayOneShot(enterClip);
        maskImg.enabled = true;
        maskAnim.Play();
        Invoke("ShowLoading", 0.4f);
        Invoke("LoadGame", 1.0f);
    }

    public void OnQuitBtn()
    {
        Application.Quit();
    }

    private void ShowLoading()
    {
        loadingTxt.enabled = true;
        loadingAnim.Play();
    }

    /// <summary>
    /// 异步加载游戏
    /// </summary>
    private void LoadGame()
    {
        SceneManager.LoadSceneAsync(1);
    }


}
