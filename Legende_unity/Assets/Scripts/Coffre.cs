using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coffre : MonoBehaviour
{

    Animator anim;
    bool isOpen;
  
    void Start()
    {
        anim = GetComponent<Animator>(); 
        isOpen= false;
    }

  
    void OnTriggerEnter(){

        if(!isOpen){
            anim.SetTrigger("OpenCoffre");
            PlayerGamePad.canMove = false;
        }
    }



    void finAnim(){

         PlayerGamePad.canMove = true;
         isOpen = true;
    }
}
