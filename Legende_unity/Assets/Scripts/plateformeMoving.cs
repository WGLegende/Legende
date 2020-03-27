using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class plateformeMoving : MonoBehaviour
{
    Animator anim;
    public GameObject player;
    
     void Start(){

      anim = GameObject.Find("elevator").GetComponent<Animator>();
      
    } 
    private void OnTriggerEnter(Collider other){

        if(other.gameObject == player){
        player.transform.parent = transform;   
        print("trigger");
        anim.SetBool("elevatorOn",true);
          
        }
    }

    private void OnTriggerExit(Collider other){

         if(other.gameObject == player){
        player.transform.parent = null;
         anim.SetBool("elevatorOn",false);
       

         }
    }



}