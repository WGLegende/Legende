using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;


public class testMute : MonoBehaviour
{

    public static testMute instance;
    
    public AudioMixer musicMix;

    public AudioMixer soundMix;

  
    [Range(-80f,0f)]
    public float musicVolume = 0f;
    [Range(-80f,0f)]
    public float sfxVolume = 0f;


    void Start()
    {
        instance = this;
        musicMix.SetFloat("musicMasterVolume",musicVolume);
        soundMix.SetFloat("soundMasterVolume",sfxVolume);
        
    }
    void Update(){

    }



}
