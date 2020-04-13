using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnterChariot : MonoBehaviour

{
  public GameObject CircuitRails;
  GameObject MainPlayer;
  GameObject Chariot;
  
  void Start(){
 
    MainPlayer = GameObject.Find("Player");
    Chariot = CircuitRails.transform.GetChild(0).gameObject;
    
  }

  void OnTriggerEnter(Collider collider){

       Debug.Log("enterName : " + collider.gameObject.name); 
    
    if(collider.gameObject.name =="Player"){
                     
      MainPlayer.SetActive(false); 
      Chariot.SetActive(true);        
    }

    if(collider.gameObject.name =="player_chariot"){
                     
      MainPlayer.SetActive(true); 
      Chariot.SetActive(false);        
    }


  }



}
