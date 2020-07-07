using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;


public class testMute : MonoBehaviour
{

    public static testMute instance;

    public AudioMixer masterMix;
    public AudioMixer musicMix;

  
    [Range(-80f,20f)]
    public float musicVolume = 0f;
    [Range(-80f,20f)]
    public float sfxVolume = 0f;


    void Start()
    {
        instance = this;
        musicMix.SetFloat("volumeMusic",musicVolume);
        masterMix.SetFloat("sfxVol",sfxVolume);
        
    }
    void Update(){

    }



}
