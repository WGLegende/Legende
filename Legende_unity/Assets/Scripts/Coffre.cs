using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coffre : MonoBehaviour
{

    Animator anim;
  
    void Start()
    {
        anim = GetComponent<Animator>(); 
    }

  
    void OnTriggerEnter(){

     anim.SetTrigger("OpenCoffre");
    }




}
