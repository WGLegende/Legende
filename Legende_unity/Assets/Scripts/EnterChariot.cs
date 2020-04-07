using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnterChariot : MonoBehaviour


{
    ChariotPlayer ScriptChariotPlayer ;
    public GameObject MainPlayer;
    public GameObject PlayerInChariot;
    public GameObject UIChariot;
    public GameObject Chariot_Bonbonne;


    void Start(){

       ScriptChariotPlayer = GameObject.Find("player_chariot").GetComponent<ChariotPlayer>();

    }

    void OnTriggerEnter(Collider other){

        if(other.gameObject.name =="Player"){
              
            if(ScriptChariotPlayer.equipement_Bouteille == true){
                MainPlayer.SetActive(false);
                PlayerInChariot.SetActive(true);
                UIChariot.SetActive(true);
                Chariot_Bonbonne.SetActive(true);
                GameObject.Find("player_chariot").GetComponent<ChariotPlayer>().enabled = true; // On active le script du player Chariot
            
             }else{
             
                Inventaire.instance.afficheInfoText("Il vous faut une Bouteille avec de la Vapeur");
            }
        }

    }



   

}
