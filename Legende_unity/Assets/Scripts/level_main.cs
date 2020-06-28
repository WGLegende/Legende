using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class level_main : MonoBehaviour
{
    public static level_main instance;

    Transform player;
    Transform CheckPointSave;
   
    public bool hasCheckPoint;

    void Start(){

        instance = this;
        player = player_main.instance.player.transform;
    }

   
    public void MovePlayerToCheckpoint(){ // reload checkpoint position 

        player.position = CheckPointSave.position;
        player.transform.rotation = CheckPointSave.transform.parent.rotation;
        player_gamePad_manager.instance.put_camera_behind_player();
        player_main.instance.AddPlayerPv(100);         
    }


    public void CheckpointSavePosition(Transform value){ // On stocke la derniere position du checkPoint
 
        CheckPointSave = value;
        GameObject.Find ("TextInfo").GetComponent<Text>().text = "CheckPoint Save";
        GameObject.Find ("Player_Info").GetComponent<Animator>().SetTrigger("panelInfo");
        hasCheckPoint = true;    
    }


}
