using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player_fall : MonoBehaviour
{

    public float timeInAir = 0f;
    public float deathTimer = 3f;
    private bool deadGirl = false;

    void Start()
    {
        
    }
 
 
    void Update () {

        ShesDeadJim ();
    }



    void ShesDeadJim(){

    CharacterController controller = GetComponent<CharacterController> ();

    if (controller.isGrounded) {
         timeInAir = 0f;
    }
     
    if (!controller.isGrounded){

        timeInAir += Time.deltaTime;
 
    }

    if (timeInAir >= deathTimer){
        deadGirl = true;
        print ("She's dead, Jim!");
    }
 }
}
