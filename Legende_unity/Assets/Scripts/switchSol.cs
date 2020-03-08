using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class switchSol : MonoBehaviour
{


  Animator anim;
  public bool switchSolIsPressed;

   void Start(){

        anim = GetComponent<Animator>();
        switchSolIsPressed = false;
    } 

    void OnTriggerEnter(){

        anim.SetBool("switchSol", true);
    }

    void OnTriggerExit(){

      if(!switchSolIsPressed){
        anim.SetBool("switchSol", false);   
      }

    }


    void SwitchPressed(){  // on attend que le switch s'enfonce completement en fin d'anim

        switchSolIsPressed = true;
    }

    

}
