
using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class Player_sound : MonoBehaviour
{
    public static Player_sound instance;

    public AudioSource marcheAudio;
    public AudioSource kart_audio_rails;
    public AudioSource kart_audio_fx;
    public AudioSource music_event_player;

    public string TypeSol; // maj par script Player_foot_step

    public AudioClip[] MarcheFx;
    public AudioClip[] FightFx;
    public AudioClip[] KartFx;
    public AudioClip[] MusicEventPlayer;
    public AudioClip[] Inventory;
   

    void Awake(){
        if(instance == null){
            instance = this; 
        }  
    }


    public void PlayFightFx(GameObject gameObj, AudioClip audioclip){ // on recupere l'audiosource attache au player
        gameObj.GetComponent<AudioSource>().PlayOneShot(audioclip);
    }

    public void PlayMusicEventPlayer(AudioClip audioclip){   
        music_event_player.PlayOneShot(audioclip);
    }





    // Sound Kart Rails
    public void PlayKart(float speed){
        
        if(speed < 10){
            kart_audio_rails.pitch = 0.8f;
        }
        else if(speed == 10){
            kart_audio_rails.pitch = 1f;
        }
        else if(speed > 10){
            kart_audio_rails.pitch = 1.6f;
        }
        kart_audio_rails.clip = KartFx[0];
        if(kart_audio_rails.isPlaying)
        return;
        kart_audio_rails.Play();
    }
    // Stop Sound KartRails
    public void StopKart(){
        kart_audio_rails.clip = null;
    }
    

    // Sound Fx Kart
    public void PlayKartFx(int i){

       kart_audio_fx.clip = KartFx[i];
       if(kart_audio_fx.isPlaying)
       return;
       kart_audio_fx.Play();
    }




    // Son Marche Player
    public void Walk(){

        marcheAudio.pitch = 1f;
        marcheAudio.volume = 1f;

        switch (TypeSol){

            case "gravel": marcheAudio.clip = MarcheFx[0]; break;
            case "wood":   marcheAudio.clip = MarcheFx[2]; break;
            case "water":  marcheAudio.clip = MarcheFx[4]; break;
            case "stone":  marcheAudio.clip = MarcheFx[6]; break;
            case "snow":   marcheAudio.clip = MarcheFx[8];  marcheAudio.volume = 0.2f; break;
        }

        if(marcheAudio.isPlaying)
        return;
        marcheAudio.Play(); 
       
    }

    // Son Run Player
    public void Run(){

        marcheAudio.pitch = 0.8f;
        marcheAudio.volume = 1f;

        switch (TypeSol){

            case "gravel": marcheAudio.clip = MarcheFx[1]; break;
            case "wood":   marcheAudio.clip = MarcheFx[3]; break;
            case "water":  marcheAudio.clip = MarcheFx[5]; marcheAudio.pitch = 1.3f; break;
            case "stone":  marcheAudio.clip = MarcheFx[7]; break;
            case "snow":   marcheAudio.clip = MarcheFx[9]; marcheAudio.pitch = 1.1f; break;
        }

        if(marcheAudio.isPlaying)
        return;
        marcheAudio.Play();   
    }

    // Stop Son FootStep
    public void StopMove(){
        marcheAudio.clip = null;
    }


     public void FadeOutAndStopEvent(float time_fade){
        StartCoroutine(FadeOutZicAndStop (time_fade));
    }


    public IEnumerator FadeOutZicAndStop(float FadeTime) {

        float startVolume =  music_event_player.volume;
       
        while (music_event_player.volume > 0) {

            music_event_player.volume -= startVolume * Time.deltaTime / FadeTime;
            yield return null;
        }
        music_event_player.Stop();
        music_event_player.volume = startVolume;
    }

    

    
}
