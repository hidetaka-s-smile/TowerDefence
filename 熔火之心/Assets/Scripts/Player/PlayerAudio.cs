using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAudio : MonoBehaviour
{
    public AudioSource Player;
    public AudioClip Die;
    public AudioClip Damage;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void DamageAudio()
    {
        gameObject.GetComponent<AudioSource>().PlayOneShot(Damage);
    }
    public void DieAudio()
    {
        gameObject.GetComponent<AudioSource>().PlayOneShot(Die);
    }
}
