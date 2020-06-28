using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Cinemachine;

public class EnterChariot : MonoBehaviour
{
  public static EnterChariot instance;

  public GameObject player_foot;
  public GameObject player_kart;
  public Vector3 offsetExitChariot;
  public CinemachineFreeLook camKart;

  kart_manager script_kart_manager;
  
  CanvasScaler ui_chariot;
  public bool kart_in_station;
  
  void Start(){

    instance = this;
    ui_chariot = GameObject.Find("UI_Chariot").GetComponent<CanvasScaler>();
    script_kart_manager = GetComponentInChildren<kart_manager>();
  }

  
  void OnTriggerEnter(Collider collider){ 

    if(collider.gameObject.name == "Player"){
      ButtonAction.instance.Action("Monter a Bord");
    }
   
    if(collider.gameObject.name == "PlayerKart" && kart_in_station){
      ButtonActionKart.instance.Action("Descendre"); 
    }

  }

  private void OnTriggerStay(Collider collider){

    if(hinput.anyGamepad.A.justPressed){ // A
     print("appuie bouton");

      if(collider.gameObject.name =="Player"){  
        EnterKart();
      }
      else if(collider.gameObject.name =="PlayerKart" && kart_in_station ){ 
        ExitKart();
      }  
    }  
  }

  void OnTriggerExit(Collider collider) {   
    if(collider.gameObject.name =="Player"){              
      ButtonAction.instance.Hide();
    }
  } 

  
  void EnterKart(){

    camKart.m_XAxis.Value = 9f;
    camKart.m_YAxis.Value = 0.5f;
    camKart.Priority = 11;
    
    ButtonAction.instance.Hide();
    player_gamePad_manager.canMove = false;
    player_foot.SetActive(false); 

    player_kart.SetActive(true);
    ui_chariot.scaleFactor = 0.8f; 
    script_kart_manager.enabled = true; 
    GamePad_manager.instance._game_pad_attribution = GamePad_manager.game_pad_attribution.kart; 
    

  }

  void ExitKart(){

    Camera_control.instance.CameraBehindPlayer();
    camKart.Priority = 9;
    kart_manager.instance.SplineFollow.IsRunning = false;
    ButtonActionKart.instance.Hide();
    player_foot.transform.position = this.transform.position + offsetExitChariot;
    player_foot.transform.rotation =  this.transform.rotation;
    GamePad_manager.instance._game_pad_attribution = GamePad_manager.game_pad_attribution.player; 
    player_kart.SetActive(false);
    ui_chariot.scaleFactor = 0f; 
    player_foot.SetActive(true); 
    player_gamePad_manager.canMove = true;
    player_gamePad_manager.instance.changeEquipement(); 
  }


    
}
