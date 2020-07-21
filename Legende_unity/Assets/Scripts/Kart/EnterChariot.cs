using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnterChariot : MonoBehaviour
{
  public static EnterChariot instance;

 
  public Transform chariot_siege;
 
  [HideInInspector] public kart_manager script_kart_manager;
  [HideInInspector] public Animator ui_chariot;
  
  
  void Start(){

    if(instance == null){
      instance = this;
    }

    instance = this;
    ui_chariot = GameObject.Find("UI_Chariot").GetComponent<Animator>();
    script_kart_manager = GetComponentInChildren<kart_manager>(); 
   
  }


  void OnTriggerEnter(Collider collider){ 

    if(collider.gameObject.tag == "Player"){
      player_actions.instance.display_actions(this,collider); 
    }
  }

  
  void OnTriggerExit(Collider collider) {  

    if(collider.gameObject.tag == "Player"){
      player_actions.instance.clear_action(collider.tag == "Player");  
   }
  } 

}
