using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class regenerePv : MonoBehaviour
{
   

public float speed;


    void OnTriggerEnter(Collider collider){

        if(collider.name == "Player"){

           StartCoroutine(regeneration()); 
        }
    }


    void OnTriggerExit(Collider collider){

        if(collider.name == "Player"){

           StopAllCoroutines();
        }
    }


    IEnumerator regeneration(){

        while(true){

            player_main.instance.AddPlayerPv(Time.deltaTime * speed);
            yield return null;
        }
    }
}
