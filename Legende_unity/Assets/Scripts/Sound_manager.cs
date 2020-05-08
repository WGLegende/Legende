using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sound_manager : MonoBehaviour
{
    public static Sound_manager Instance;
    public AudioSource SoundSource;

    public AudioClip theme;
    public AudioClip theme2;
    public AudioClip test;
    public AudioClip test2;
    public AudioClip test3;




    void Start(){
        Instance = this; 
        SoundSource.PlayOneShot(theme2);
    }

   
    


    public void lanceMusiqueAttaque(){

        SoundSource.PlayOneShot(theme);
    }

    public void lanceMusiquetest(){
        SoundSource.PlayOneShot(test);
    }
     public void lanceMusiquetest2(){
        SoundSource.PlayOneShot(test2);
    }
}
