using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnterChariot : MonoBehaviour


{

    public GameObject MainPlayer;
    public GameObject PlayerInChariot;


    void OnTriggerEnter(Collider other){

        if(other.gameObject.name =="Player"){
            MainPlayer.SetActive(false);
            PlayerInChariot.SetActive(true);
            GameObject.Find("player_chariot").GetComponent<ChariotPlayer>().enabled = true; // On active le script du player Chariot
        }
    }



   

}
