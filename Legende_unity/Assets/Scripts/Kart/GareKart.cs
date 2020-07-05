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
        Terminus,
        Aiguillage       
    }

    public SensSortie _sens_sortie;
    public enum SensSortie{
        Droite,
        Gauche         
    }

    public CircuitActuel _type_rails;
    public enum CircuitActuel{
        Principal,
        Secondaire         
    }

    public bool kart_in_station;
    
    public GameObject player_foot;
    public GameObject player_kart;
    public Transform chariot_siege;
    public Transform chariot_container;

    public GameObject rails_principal;
    public GameObject rails_aiguillage;
    Battlehub.MeshDeformer2.SplineBase rails;
    Battlehub.MeshDeformer2.SplineBase new_rails;
    [HideInInspector] public float position_save_kart; 
    [HideInInspector] public bool switch_rails;

    [HideInInspector] public Vector3 offset_exit_chariot;
    
    [HideInInspector] public CanvasScaler ui_chariot;

    CinemachineVirtualCamera cam_panneau;
    
    public Animator signaletique;
    
    void Start(){
         
        if(instance == null){
            instance = this;
        }

        ui_chariot = GameObject.Find("UI_Chariot").GetComponent<CanvasScaler>();
        cam_panneau = GetComponentInChildren<CinemachineVirtualCamera>(); // test

        if(rails_aiguillage != null){
            new_rails = rails_aiguillage.GetComponent<Battlehub.MeshDeformer2.SplineBase>();
        }
        if(rails_principal != null){
            rails = rails_principal.GetComponent<Battlehub.MeshDeformer2.SplineBase>();
        }
  
        if(_sens_sortie == SensSortie.Droite){
            offset_exit_chariot = new Vector3(0f,-1f,-2.5f);
        }else{
            offset_exit_chariot = new Vector3(0f,-1f,2.5f);
        }  
    }


    void OnTriggerEnter(Collider collider){

        if(collider.gameObject.tag == "PlayerKart"){

            if(_type_gare == typeGare.Aiguillage && kart_manager.instance.SplineFollow.Speed < 0){

                if(_type_rails == CircuitActuel.Secondaire){
                   
                    kart_manager.instance.SplineFollow.Spline = rails;// on load le circuit principale
                    kart_manager.instance.SplineFollow.Restart();
                    kart_manager.instance.SplineFollow.m_t = position_save_kart; // on recupere la dernier position du kart
                }   
            }

            else if( _type_gare == typeGare.Aiguillage && kart_manager.instance.SplineFollow.Speed > 0){
    
                if(_type_rails == CircuitActuel.Secondaire){
                   
                    kart_manager.instance.SplineFollow.Spline = new_rails;// on load le nouveau circuit
                    kart_manager.instance.SplineFollow.Restart(); 
                } 
               
            }
        }   
    }

    void OnTriggerStay(Collider collider){ 

        if(collider.gameObject.tag == "PlayerKart"){
            
            if(Mathf.Abs(kart_manager.instance.vitesse_actuelle) > 1 && _type_gare != typeGare.Terminus) 
            return; // si trop vite a une station, on fait rien

            if(!kart_in_station){
                kart_in_station = true;
                if(cam_panneau != null){ cam_panneau.Priority = 12;} // test

                switch (_type_gare){ 
                    case typeGare.Depart :  kart_manager.instance.canMoveRecul = false; 
                                            ButtonActionKart.instance.Action("Descendre");                                      
                    break;

                    case typeGare.Terminus: kart_manager.instance.canMoveAvance = false;
                                            kart_manager.instance.SplineFollow.IsRunning = false; // todo pour un arret smooth
                                            ButtonActionKart.instance.Action("Descendre");                                  
                    break;

                    case typeGare.Station: ButtonActionKart.instance.Action("Descendre");                         
                    break;

                    case typeGare.Aiguillage :  ButtonActionKart.instance.Action("Bifurquer");                                                                              
                    break;
                }
            }

            if(Input.GetKeyDown("joystick button 0")){ // A

                if(_type_gare == typeGare.Aiguillage){

                    if(!switch_rails){
                        switch_rails = true;
                        position_save_kart = kart_manager.instance.SplineFollow.m_t; // on save la position du circuit principal
                        kart_manager.instance.SplineFollow.Spline = new_rails;// on load le nouveau circuit
                        kart_manager.instance.SplineFollow.Restart(); // on restart le nouveau circuit
                        _type_rails = CircuitActuel.Secondaire; 
                        signaletique.SetBool("principal",false);
                    } 
                    else{
                        kart_manager.instance.SplineFollow.Spline = rails; // on load le circuit principal
                        kart_manager.instance.SplineFollow.Restart(); // on restart le nouveau circuit
                        kart_manager.instance.SplineFollow.m_t = position_save_kart; // on recupere la derniere position du circuit principale
                        switch_rails = false;
                        _type_rails = CircuitActuel.Principal;
                        signaletique.SetBool("principal",true);
                    } 
                }
                else{
                    ExitKart();
                }
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
            if(cam_panneau != null){ cam_panneau.Priority = 8;} // test
           
        }
    } 

    public void ExitKart(){ // Bascule sur player

        Camera_control.instance.CameraBehindPlayer();
        Camera_control.instance.player_kart_camera.Priority = 9;
        kart_manager.instance.vitesse_actuelle = 0f;
        kart_manager.instance.SplineFollow.IsRunning = false;

        player_foot.transform.position = kart_manager.instance.chariot_siege.transform.position + offset_exit_chariot; // on deplace le player en dehors du kart
        Vector3 rotationPlayer = new Vector3(0,chariot_container.transform.eulerAngles.y,0); // on le tourne dans le meme sens que le kart
        player_foot.transform.rotation = Quaternion.Euler(rotationPlayer);

        GamePad_manager.instance._game_pad_attribution = GamePad_manager.game_pad_attribution.player; 

        player_kart.SetActive(false);
        ui_chariot.scaleFactor = 0f; //todo hide ui kart

        player_foot.SetActive(true); 
        player_gamePad_manager.instance.PlayerCanMove(true);
        player_gamePad_manager.instance.changeEquipement(); 
    }

}
