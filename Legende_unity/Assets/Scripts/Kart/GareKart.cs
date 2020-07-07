using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Cinemachine;


public class GareKart : MonoBehaviour
{
    public static GareKart instance;

    public typeGare _type_gare;
    public enum typeGare{
        Depart,
        Station,
        Terminus       
    }

    public SensSortie _sens_sortie;
    public enum SensSortie{
        Droite,
        Gauche         
    }

    public bool kart_in_station;

    public GameObject player_foot;
    public GameObject player_kart;
    public Transform chariot_container;

    [HideInInspector] public Vector3 offset_exit_chariot;
    
    [HideInInspector] public CanvasScaler ui_chariot;

    
    void Start(){
         
        if(instance == null){
            instance = this;
        }
  
        player_foot = GameObject.Find("Player");
        chariot_container = GameObject.Find("Chariot_Container").GetComponent<Transform>();
        ui_chariot = GameObject.Find("UI_Chariot").GetComponent<CanvasScaler>();
        
        if(_sens_sortie == SensSortie.Droite){
            offset_exit_chariot = new Vector3(0f, 0f,0f);
        }else{
            offset_exit_chariot = new Vector3(0f,0f, 0f);
        }  
    }


    void OnTriggerEnter(Collider collider){

        if(collider.gameObject.tag == "PlayerKart"){

        }   
    }

    void OnTriggerStay(Collider collider){ 

        if(collider.gameObject.tag == "PlayerKart"){
            
            if(Mathf.Abs(kart_manager.instance.vitesse_actuelle) > 1 && _type_gare == typeGare.Station) 
            return; // si trop vite a une station, on fait rien

            if(!kart_in_station){
                kart_in_station = true;
               
                switch (_type_gare){ 

                    case typeGare.Depart :  kart_manager.instance.canMoveRecul = false;
                                            kart_manager.instance.SplineFollow.IsRunning = false; 
                                            ButtonActionKart.instance.Action("Descendre");                                                       
                    break;

                    case typeGare.Terminus: kart_manager.instance.canMoveAvance = false;
                                            kart_manager.instance.SplineFollow.IsRunning = false; // todo pour un arret smooth
                                            ButtonActionKart.instance.Action("Descendre");                                  
                    break;

                    case typeGare.Station: ButtonActionKart.instance.Action("Descendre");                         
                    break;
                }
            }

            if(Input.GetKeyDown("joystick button 0")){ // A

                ExitKart();
                ButtonAction.instance.Hide();
            }
        }
    }

    void OnTriggerExit(Collider collider) {
        
        if(collider.gameObject.tag =="PlayerKart"){   
            kart_in_station = false;  
            kart_manager.instance.canMoveAvance = true; 
            kart_manager.instance.canMoveRecul = true;
            ButtonActionKart.instance.Hide(); 
        }
    } 









    public void ExitKart(){ // Bascule sur player

        Camera_control.instance.CameraBehindPlayer();
        Camera_control.instance.player_kart_camera.Priority = 9;
        kart_manager.instance.vitesse_actuelle = 0f;
        kart_manager.instance.SplineFollow.IsRunning = false;

        Vector3 rotationPlayer = new Vector3(0,player_kart.transform.eulerAngles.y,0); // on le tourne dans le meme sens que player_kart
        player_foot.transform.rotation = Quaternion.Euler(rotationPlayer);
        player_foot.transform.localPosition =  chariot_container.transform.position + offset_exit_chariot;

        GamePad_manager.instance._game_pad_attribution = GamePad_manager.game_pad_attribution.player; 

        player_kart.SetActive(false);
        ui_chariot.scaleFactor = 0f; //todo hide ui kart

        player_foot.SetActive(true); 
        player_gamePad_manager.instance.PlayerCanMove(true);
        player_gamePad_manager.instance.changeEquipement(); 
    }

}
