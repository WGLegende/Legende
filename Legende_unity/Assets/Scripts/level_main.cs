using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Cinemachine;


public class level_main : MonoBehaviour
{
    public static level_main instance;

    Transform player;
    Transform kart;
    Transform CheckPointSave;
    public float save_position_kart;
    public int  save_spline_actuelle;
   
    public bool hasCheckPoint;
    public bool hasCheckPointKart;

    Camera cam;
    CinemachineBrain cam_brain;

    
    void Start(){

        if(instance == null){
            instance = this;
        }

        player = player_main.instance.player.transform;
        cam = GameObject.Find("Camera").GetComponent<Camera>();
        cam_brain = cam.GetComponent<CinemachineBrain>();
        
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

        save_position_kart = value; // on save la position du kart par rapport a la spline
        save_spline_actuelle = AiguillageManager.instance.id_rails; // on save la spline actuelle
        GameObject.Find ("TextInfo").GetComponent<Text>().text = "CheckPointKart Save";
        GameObject.Find ("Player_Info").GetComponent<Animator>().SetTrigger("panelInfo");
        hasCheckPointKart = true;    
    }




    // On load le dernier checkpoint du kart 
    public IEnumerator MoveKartToCheckpoint(){ 

        yield return new WaitForSeconds(2f);

       // kart_manager.instance.anim_kart.enabled = true; 
        kart.transform.localPosition = new Vector3(-0.04f,0f,0f);
        kart.transform.localRotation = Quaternion.Euler(0,0,0);
        kart_manager.instance.chariot_siege.transform.localRotation = Quaternion.Euler(270f,90f,-90f); 
        kart_manager.instance.rb.isKinematic = true;
        kart_manager.instance.rb.useGravity = false;
        kart_manager.instance.canMoveAvance = true;
        kart_manager.instance.canMoveRecul = true;

        kart_manager.instance.SplineFollow.Spline = AiguillageManager.instance.List_spline_rails[save_spline_actuelle];// on rappelle la spline sauve
        AiguillageManager.instance.id_rails = save_spline_actuelle; // maj de la position dans la liste 
        kart_manager.instance.SplineFollow.Restart();
        kart_manager.instance.SplineFollow.m_t = save_position_kart; 
       
        StartCoroutine(Camera_control.instance.CameraBehindKart());
        Camera_control.instance.cam_crash.Priority = 0;

        enemy_rails_manager.instance.reinitializeAllEnemy();
    }


}
