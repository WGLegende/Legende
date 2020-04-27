using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class player_main : MonoBehaviour

{
    # region Singleton

    public static player_main instance;

    void Awake()
    {
        instance = this;
    }

    #endregion

    public GameObject player;

    public float player_current_pv;
    float player_max_pv = 100f;

    Transform playerTransform;
    Transform cam;
    Vector3 startPosition;
    Quaternion startRotation;
 
 
    void Start()
    {
       
        playerTransform = GameObject.Find("Player").GetComponent<Transform>(); 
        cam = GameObject.Find("cam_container").GetComponent<Transform>(); 
        startPosition = player.transform.position;
        startRotation = player.transform.rotation; 
    }

   
    void Update()
    {
        
    }



    public void AddPlayerPv(float value){

        player_current_pv += value;
        player_current_pv = Mathf.Clamp(player_current_pv, 0, 100); 
        Barre_de_Vie.instance.RefreshPvPlayerUI(player_current_pv);
    }

    public void DegatPlayerPv(float value){

        player_current_pv -= value;
        player_current_pv = Mathf.Clamp(player_current_pv, 0, 100); 
        Barre_de_Vie.instance.RefreshPvPlayerUI(player_current_pv);
        
        if (player_current_pv == 0){
            
            GameObject.Find ("TextInfo").GetComponent<Text>().text = "Vous etes une Quiche";
            GameObject.Find ("PanelInfo").GetComponent<Animator>().SetTrigger("panelInfo");

            if (level_main.instance.hasCheckPoint){
                level_main.instance.MovePlayerToCheckpoint();
             }else{
                playerTransform.transform.position = startPosition;
                playerTransform.transform.rotation = startRotation;
                cam.transform.localEulerAngles = new Vector3(0f,0f,0f);
                player_main.instance.AddPlayerPv(100); 
            }
            
        }
    }
}
