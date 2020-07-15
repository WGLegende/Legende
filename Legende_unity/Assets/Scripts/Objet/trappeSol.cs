using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class trappeSol : MonoBehaviour
{

  Animator anim;


    void Start(){

     anim = GetComponentInChildren<Animator>();
    } 


    void OnTriggerEnter(){

     anim.SetBool("trappeIsOpen", true);
    }

    void OnTriggerExit(){

     anim.SetBool("trappeIsOpen", false);
    }



}
