using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class no_bouteille : MonoBehaviour
{
   
    BoxCollider col;

    void Start(){

        col = GetComponent<BoxCollider>();   
    }

    


    void OnTriggerExit(Collider other){

        if(other.gameObject.tag == "PlayerKart" && !kart_manager.instance.equipement_bouteille){

            ame_player.instance.text_de_navy_container = ame_player.instance.text_bouteille_kart;
            StartCoroutine(ame_player.instance.navy_start_speak(3f));
            col.enabled = false;
        }   
    }
}
