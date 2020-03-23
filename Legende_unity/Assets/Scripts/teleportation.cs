using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class teleportation : MonoBehaviour
{


public GameObject destination ;
public GameObject player;



    void OnTriggerEnter(){

        if (destination != null){
            player.transform.position = destination.transform.position; 
        }
    }

    void OnTriggerExit(){
     
    }

   


}
