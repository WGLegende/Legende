using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class teleportation : MonoBehaviour
{

    public GameObject destination ;
    Transform positionPlayer;
    Animator anim;
    
    void Start(){

        anim = GetComponent<Animator>(); 
        positionPlayer = GameObject.Find("Player").GetComponent<Transform>(); 
    }

    void OnTriggerEnter(){
             
        if (destination != null){ 
            anim.SetBool("teleportationGate", true);
            GameObject.Find("Player").GetComponent<Animator>().SetTrigger("teleportation");   
            PlayerGamePad.canMove = false;
        }
    }

    void OnTriggerExit(){

        anim.SetBool("teleportationGate", false);
    }

    void endAnim(){
 
        positionPlayer.transform.position = new Vector3 (destination.transform.position.x,2f, destination.transform.position.z);
        PlayerGamePad.canMove = true;
    }


}
