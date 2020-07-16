using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class level_main : MonoBehaviour
{
    public static level_main instance;

    Transform player;
    Transform kart;
    Transform CheckPointSave;
    public float save_position_kart;
   
    public bool hasCheckPoint;
    public bool hasCheckPointKart;


    void Start(){

        if(instance == null){
            instance = this;
        }

        player = player_main.instance.player.transform;
        
        if(player_main.instance.kart != null){
        kart = player_main.instance.kart.GetComponent<Transform>(); 
        }
    }



    // reload checkpoint position 
    public void MovePlayerToCheckpoint(){
        player.position = CheckPointSave.position;
        player.transform.rotation = CheckPointSave.transform.parent.rotation;
        player_gamePad_manager.instance.put_camera_behind_player();
        player_main.instance.AddPlayerPv(100);         
    }
    // On stocke la derniere position du checkPoint
    public void CheckpointSavePosition(Transform value){ 
        CheckPointSave = value;
        GameObject.Find ("TextInfo").GetComponent<Text>().text = "CheckPoint Save";
        GameObject.Find ("Player_Info").GetComponent<Animator>().SetTrigger("panelInfo");
        hasCheckPoint = true;    
    }



    // On stocke la derniere position du checkPoint avec le kart
    public void CheckpointSavePositionKart(float value){ 
        save_position_kart = value;
        GameObject.Find ("TextInfo").GetComponent<Text>().text = "CheckPoint Save";
        GameObject.Find ("Player_Info").GetComponent<Animator>().SetTrigger("panelInfo");
        hasCheckPointKart = true;    
    }

    // On load le dernier checkpoint du kart 
    public void MoveKartToCheckpoint(){ 
        kart_manager.instance.SplineFollow.Spline = AiguillageManager.instance.SaveTrajetKart[AiguillageManager.instance.position_trajet];
        kart_manager.instance.SplineFollow.Restart();
        kart_manager.instance.canMoveAvance = true;
        kart_manager.instance.canMoveRecul = true;
        kart.transform.localPosition = new Vector3(-0.04f,0f,0f);
        kart.transform.localRotation = Quaternion.Euler(0,0,0);
        kart_manager.instance.SplineFollow.m_t = save_position_kart;      
    }


}
