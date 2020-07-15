
using UnityEngine.Audio;
using System.Collections;
using System;
using UnityEngine;


public class Music_sound : MonoBehaviour
{

    public static Music_sound instance;

    public AudioMixerGroup MusicMix;
    public AudioMixer MusicMaster;

    public clipSound[] music;
    


    void Awake(){

        if (instance == null)
            instance = this;

        else{
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);


        foreach(clipSound s in music){

            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.volume = s.volume;
            s.source.loop = s.loop;
            s.source.outputAudioMixerGroup = MusicMix;
        }

    }

    void Update(){

        if(Input.GetKeyDown("o")){
            StartCoroutine(FadeMixer.StartFade(MusicMaster, "musicMasterVolume", 5f , 0f));
        }

        if(Input.GetKeyDown("i")){
            StartCoroutine(FadeMixer.StartFade(MusicMaster, "musicMasterVolume", 5f , 20f));
        }
    }



    public void PlayMusic(string name){

        clipSound s = Array.Find(music, sound => sound.name == name);
        float startVolume =  s.source.volume;

        if (s == null){
            Debug.LogError("La musique: "+ name + " n'a pas ete trouve !");
            return;
        }  

        if(!s.source.isPlaying){
            s.source.volume = startVolume;
            s.source.Play();
        }  
    }

    


    public void FadeOutAndStop(string name, float value){
        StartCoroutine(FadeOutZicAndStop (name, value));
    }


    public IEnumerator FadeOutZicAndStop(string name, float FadeTime) {

        clipSound s = Array.Find(music, sound => sound.name == name);
        float startVolume =  s.source.volume;
 
        while ( s.source.volume > 0) {

            s.source.volume -= startVolume * Time.deltaTime / FadeTime;
            yield return null;
        }
        s.source.Stop();
        s.source.volume = startVolume;
    }
 
}

