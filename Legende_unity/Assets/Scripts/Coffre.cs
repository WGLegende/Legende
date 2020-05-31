using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coffre : MonoBehaviour
{

    Animator anim;
    bool isOpen;
  
    void Start(){

        anim = GetComponent<Animator>(); 
    }

  
    void OnTriggerEnter(){

        if(!isOpen){

            isOpen = true;
            anim.SetTrigger("OpenCoffre");

            //Time.timeScale = 0;

            player_gamePad_manager.canMove = false;
            player_gamePad_manager.canAttack = false;
            player_gamePad_manager.canJump = false;

            Player_sound.instance.PlayMusicEventPlayer(Player_sound.instance.MusicEventPlayer[0]); 
            StartCoroutine(FadeMixer.StartFade(Music_sound.instance.MusicMaster, "musicMasterVolume", 2f , 0f)); // cut zic
        }
    }



    void finAnim(){ // declenchee en fin anim

        player_gamePad_manager.canMove = true;
        player_gamePad_manager.canAttack = true;
        player_gamePad_manager.canJump = true;

        //Time.timeScale = 1;

        StartCoroutine(FadeMixer.StartFade(Music_sound.instance.MusicMaster, "musicMasterVolume", 2f , 20f)); // remove zic
    }
}
