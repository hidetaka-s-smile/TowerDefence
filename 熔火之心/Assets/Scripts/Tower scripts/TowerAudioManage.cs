using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerAudioManage : MonoBehaviour
{
    public AudioSource audios;
    public AudioClip audio1;
    // Start is called before the first frame update
    void Start()
    {
        audios = this.GetComponent<AudioSource>();
        audio1 = this.GetComponent<AudioClip>();
    }

    public void PlayAudio()
    {
        audios.PlayOneShot(audio1);
    }
}
