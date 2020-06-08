using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class level_main : MonoBehaviour
{
    public static level_main instance;

    Transform player;
    Transform camera_container;
    Transform CheckPointSave;
   
    public bool hasCheckPoint;

     void Start(){

        instance = this;
        player = GameObject.Find("Player").GetComponent<Transform>(); 
        camera_container =  GameObject.Find("cam_container").GetComponent<Transform>();
      
    }

   
    void Update()
    {
        
    }


    public void MovePlayerToCheckpoint(){ // reload checkpoint position 

        player.position = CheckPointSave.position;
        player.transform.rotation = CheckPointSave.transform.parent.rotation;
        camera_container.transform.localEulerAngles = new Vector3(0f,0f,0f);
        player_main.instance.AddPlayerPv(100);         
    }


    public void CheckpointSavePosition(Transform value){ // On stocke la derniere position du checkPoint
 
        CheckPointSave = value;
        GameObject.Find ("TextInfo").GetComponent<Text>().text = "CheckPoint Save";
        GameObject.Find ("PanelInfo").GetComponent<Animator>().SetTrigger("panelInfo");
        hasCheckPoint = true;    
    }


}
