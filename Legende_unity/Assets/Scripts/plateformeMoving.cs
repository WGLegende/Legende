using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class plateformeMoving : MonoBehaviour
{

    public GameObject player;
    
    
    private void OnTriggerEnter(Collider other){

        player.transform.parent = transform;   
    }

    private void OnTriggerExit(Collider other){
        player.transform.parent = null; 
    }



}