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
            GameObject.Find("Player").GetComponent<PlayerGamePad>().enabled = false;
            GameObject.Find("Player").GetComponent<Animator>().SetTrigger("teleportation");
        }
    }

    void OnTriggerExit(){

        anim.SetBool("teleportationGate", false);
    }

    void endAnim(){

        positionPlayer.transform.position = destination.transform.position; 
        GameObject.Find("Player").GetComponent<PlayerGamePad>().enabled = true;
        
    }


}
