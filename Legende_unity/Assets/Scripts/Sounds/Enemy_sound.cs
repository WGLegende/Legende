
using UnityEngine.Audio;
using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class Enemy_sound : MonoBehaviour
{
    public static Enemy_sound instance;
    public AudioMixerGroup SfxMix;

    public List<AudioClip> Robot;
    public List<AudioClip> Human;

    void Awake(){

        if (instance == null)
            instance = this;
        else{
            Destroy(gameObject);
            return;
        }
    }

    // Lecture du son
    public void PlaySound(GameObject gameObj, AudioClip audioclip){

        if (audioclip == null){
            Debug.LogError("Le son n'a pas ete trouve !");
            return;
        }  
        gameObj.GetComponent<AudioSource>().PlayOneShot(audioclip);
    }


    // Stop le son
    public void StopSound(GameObject gameObj, AudioClip audioclip){
        gameObj.GetComponent<AudioSource>().Stop();
    }

    
}

