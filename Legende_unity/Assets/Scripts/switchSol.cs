using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class switchSol : MonoBehaviour
{

  Animator anim;
  public bool switchSolIsPressed;
  public bool JustOnce; // comportement du switch


   void Start(){

      anim = GetComponent<Animator>();
      switchSolIsPressed = false; 
    } 

    void OnTriggerEnter(){

      anim.SetBool("switchSol", true);   
    }

    void OnTriggerExit(){

      if(JustOnce == false){
        anim.SetBool("switchSol", false); 
        switchSolIsPressed = false;  
      }
    }


    void SwitchPressed(){  // declenche en fin d'animatiom

      switchSolIsPressed = true; 
    }

    

}
