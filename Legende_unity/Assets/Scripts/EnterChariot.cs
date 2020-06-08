using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnterChariot : MonoBehaviour



{
  public GameObject CircuitRails;
  GameObject MainPlayer;
  GameObject Chariot;
  bool inFoot = true;
  
  void Start(){
 
    MainPlayer = GameObject.Find("Player");
    Chariot = CircuitRails.transform.GetChild(0).gameObject;
    
  }



  void OnTriggerEnter(Collider collider){

    if(collider.gameObject.name =="Player" && inFoot){           
      MainPlayer.SetActive(false); 
      Chariot.SetActive(true);      
    }
    if(collider.gameObject.name =="player_chariot" && !inFoot){                 
      MainPlayer.SetActive(true); 
      Chariot.SetActive(false);        
    }

  }


  void OnTriggerExit(Collider collider) {
        
    if(collider.gameObject.name =="player_chariot"){              
      inFoot = false; 
    }
     if(collider.gameObject.name =="Player"){              
      inFoot = true; 
    }

  } 





}
