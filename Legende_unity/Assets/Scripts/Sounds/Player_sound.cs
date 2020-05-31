
using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class Player_sound : MonoBehaviour
{
    public static Player_sound instance;

    public AudioSource marcheAudio;
    public AudioSource music_event_player;

    public string TypeSol; // maj par script Player_foot_step

    public AudioClip[] MarcheFx;
    public AudioClip[] FightFx;
    public AudioClip[] MusicEventPlayer;
   

    void Start(){

        instance = this;  
       
    }


    public void PlayFightFx(GameObject gameObj, AudioClip audioclip){ // on recupere l'audiosource attache au player
        
        gameObj.GetComponent<AudioSource>().PlayOneShot(audioclip);
    }

    public void PlayMusicEventPlayer(AudioClip audioclip){ 
        
        music_event_player.PlayOneShot(audioclip);
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

    
}
