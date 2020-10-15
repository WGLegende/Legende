using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ascenseur : MonoBehaviour{

    [HideInInspector] public bool isPositionUp = true;
    [HideInInspector] public Animator anim_elevator;
    public float vitesse = 2f;
    public bool has_vapeur;

    public enum_manager.Direction _position_ascenseur;

    [HideInInspector] public AudioSource _audio_source;
    public AudioClip[] _audio_clip;


    void Start(){

        anim_elevator = GetComponent<Animator>();
        anim_elevator.SetFloat("vitesse_deplacement",vitesse);  
        _audio_source = GetComponent<AudioSource>();  

        if(_position_ascenseur == enum_manager.Direction.down){
            isPositionUp = false;
            anim_elevator.SetBool("position_up",false);
        }
    }
   
    void elevatorPositionDown(){ // on appelle la fonction en fin d'anim
        isPositionUp = false;
        _audio_source.clip = _audio_clip[1];
        _audio_source.Play();
    }

    void elevatorPositionUp(){ // on appelle la fonction en fin d'anim
        isPositionUp = true;
        _audio_source.clip = _audio_clip[1];
        _audio_source.Play();  
    }

     void start_elevator(){ // on appelle la fonction en debut d'anim
       
        _audio_source.clip = _audio_clip[0];
        _audio_source.Play();  
    }


}
