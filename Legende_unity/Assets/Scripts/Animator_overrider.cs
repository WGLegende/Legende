using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animator_overrider : MonoBehaviour
{

    public static Animator_overrider instance;
    Animator _animator;
    public AnimatorOverrideController[] override_controller;
    public Animator_overrider Player_animator;


    void Start(){

        if(instance == null){
            instance = this;
        }
        _animator = GetComponent<Animator>();
        
    }

   
    public void SetAnimations(AnimatorOverrideController overrideController){

        _animator.runtimeAnimatorController = overrideController;
    }

    public void Set(int value){

        Player_animator.SetAnimations(override_controller[value]);
           
    }

    
}
