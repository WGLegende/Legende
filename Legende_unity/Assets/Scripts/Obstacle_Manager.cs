using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Obstacle_Manager : MonoBehaviour
{
  
  public static Obstacle_Manager instance;

  Transform Player;
  Transform camera_container;
  Transform CheckPointSave;

 
    void Start(){

        instance = this;
        Player = GameObject.Find("Player").GetComponent<Transform>(); 
        camera_container =  GameObject.Find("cam_container").GetComponent<Transform>();
    }

   
    void Update(){
       
    }


    public void DegatPlayer(int value){

        Barre_de_Vie.instance.PvPlayer(value);
    }


    public void GoCheckPointMananger(){   
 
        Player.transform.position = CheckPointSave.transform.position;
        Player.transform.rotation = CheckPointSave.transform.parent.rotation;
        camera_container.transform.localEulerAngles = new Vector3(0f,0f,0f);
        Barre_de_Vie.instance.PvPlayer(100);
    }


    public void CheckPointSaveManager(Transform value){ // On stocke la derniere position du checkPoint
 
        CheckPointSave = value;
        GameObject.Find ("TextInfo").GetComponent<Text>().text = "CheckPoint Save";
        GameObject.Find ("PanelInfo").GetComponent<Animator>().SetTrigger("panelInfo");
        print("sauvegarde"); 
    }





}
