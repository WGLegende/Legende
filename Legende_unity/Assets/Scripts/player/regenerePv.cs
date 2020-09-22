using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class regenerePv : MonoBehaviour
{
   

public float vitesse_de_recharge;


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

            player_life.instance.change_player_life(Time.deltaTime * vitesse_de_recharge);
            yield return null;
        }
    }
}
