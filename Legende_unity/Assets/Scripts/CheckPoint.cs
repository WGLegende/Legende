using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CheckPoint : MonoBehaviour
{
    public static CheckPoint instance;

    ParticleSystem particule;
    Transform Player;
     Transform camera_container;
    bool isCheck;
    bool justOnce;
    
    void Start(){
        
        instance = this;
        particule = GetComponent<ParticleSystem>();
        Player = GameObject.Find("Player").GetComponent<Transform>(); 
        camera_container =  GameObject.Find("cam_container").GetComponent<Transform>();
    }

    void OnTriggerEnter(){

        if(!justOnce){
            isCheck = true;
            GameObject.Find ("TextInfo").GetComponent<Text>().text = "CheckPoint Save";
            GameObject.Find ("PanelInfo").GetComponent<Animator>().SetTrigger("panelInfo");
            particule.startColor = Color.green;
            justOnce = true;
        }
         
    }


    public void MonCheckPoint(){
        
        if(isCheck){
            Player.transform.position = this.transform.position;
            Player.transform.rotation = this.transform.parent.rotation;
            camera_container.transform.localEulerAngles = new Vector3(0f,0f,0f);
            Barre_de_Vie.instance.PvPlayer(100);
        }
    }
}
