using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.UI;

public class tests_manager : MonoBehaviour
{
    public bool destroyPlayerPrefs;
    public bool player_never_die;
    public bool test_player_kart;
    public bool always_vapeur;
    public bool EarthQuakeEffect;

    public GameObject Player;
    public GameObject Playerkart;

    public CinemachineVirtualCamera testCamKart;

    public Text debugText4;
    public Text debugText5;
    public Text debugText6;
    public Text debugText7;


    void Start(){

        if(destroyPlayerPrefs){
            PlayerPrefs.DeleteAll();
        }
        if(player_never_die){
           InvokeRepeating("PlayerInfinityPv",0, 2.0f);
        } 

         if(always_vapeur){
           InvokeRepeating("AlwaysVapeur",0, 5f);
        } 
        if(test_player_kart){
            Invoke("switchKart",0.1f);  
        } 

        if(Input.GetKeyDown(KeyCode.Space)){   // Rempli la jauge vapeur TRICHE todo
            VapeurBar.instance.fill_vapeur_stock();
        }
        if(EarthQuakeEffect){
            StartCoroutine(Camera_control.instance.start_earthquake());
        }

        StartCoroutine(angleYkart());   
    }


    void Update(){

        if(Input.GetKeyDown(KeyCode.O)){
            foreach(enemy enemy in enemy_manager.instance.mesEnemyList){
                enemy.current_comportement = enemy_manager.comportement.dead;
            }
        }
        if(Input.GetKeyDown("c")){  // test cam Auto kart
            if(testCamKart.Priority < 15){
                testCamKart.Priority = 15;
            }
            else{
                testCamKart.Priority = 0;
            }
        }

         if(kart_manager.instance.danger_kart){
            debugText5.color = Color.yellow;
            debugText5.text = "DANGER !";
        }else{
            debugText5.text = "";
        }

    }

    IEnumerator angleYkart(){ // angle turn kart

        yield return new WaitForSeconds(0.2f);

        while(true){
        debugText4.text = "angle_Y_kart :"+ Mathf.Abs(kart_manager.instance.angle_rotation_Y).ToString("f0");    
        yield return new WaitForSeconds(0.2f);
        }
    }



    void PlayerInfinityPv(){
        player_main.instance.AddPlayerPv(100);
    }

    void AlwaysVapeur(){  
        VapeurBar.instance.fill_vapeur_stock();
    }

    void switchKart(){
       player_actions.instance.do_action_enter_kart(EnterChariot.instance);
    }

}
