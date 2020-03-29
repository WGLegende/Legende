using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class teleportation : MonoBehaviour
{

    public GameObject destination ;
    Transform positionPlayer;
    
    void Start(){

        positionPlayer = GameObject.Find("Player").GetComponent<Transform>(); 
    }

    void OnTriggerEnter(){
             
        if (destination != null){ 
            positionPlayer.transform.position = destination.transform.position ;
        }
    }

  

}
