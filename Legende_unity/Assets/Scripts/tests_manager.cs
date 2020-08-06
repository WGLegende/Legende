using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.UI;

public class tests_manager : MonoBehaviour
{
    public bool destroyPlayerPrefs;
    public bool player_never_die;
    public bool always_vapeur;
    public bool EarthQuakeEffect;

    public GameObject Player;
    public GameObject Playerkart;

    public CinemachineVirtualCamera testCamKart;

    [Header("Position Raccourci PLayer")]
    public Transform touche_1;
    public Transform touche_2;
    public Transform touche_3;
    public Transform touche_4;
  

   
    
    void Start(){

        Player = player_main.instance.player;

        if(destroyPlayerPrefs){
            PlayerPrefs.DeleteAll();
        }

        if(player_never_die){
           InvokeRepeating("PlayerInfinityPv",0,2.0f);
           
        } 

        if(always_vapeur){
           InvokeRepeating("AlwaysVapeur",0,5f);   
        } 

        if(EarthQuakeEffect){
            StartCoroutine(Camera_control.instance.start_earthquake());
        }

    }


    void Update(){

        if(Input.GetKeyDown(KeyCode.O)){
            foreach(enemy enemy in enemy_manager.instance.mesEnemyList){
                enemy.current_comportement = enemy_manager.comportement.dead;
            }
            if(!Player.activeSelf){
                enemy_rails_manager.instance.reinitializeAllEnemy();
                print("reinitialisation des enemy rails");
            }
        }

        if(Input.GetKeyDown(KeyCode.Space)){   // Rempli la jauge vapeur TRICHE todo
            VapeurBar.instance.fill_vapeur_stock();
            StockBullet.instance.update_stock_bullet(20); 
            player_main.instance.AddPlayerPv(100);
           
        }

        if(Input.GetKeyDown("c")){  // test cam Auto kart
            if(testCamKart.Priority < 15){
                testCamKart.Priority = 15;
            }
            else{
                testCamKart.Priority = 0;
            }
        }

       
        switch(Input.inputString){

            case "1" : Player.transform.position = touche_1.transform.position; break;
            case "2" : Player.transform.position = touche_2.transform.position; break;
            case "3" : Player.transform.position = touche_3.transform.position; break;
            case "4" : Player.transform.position = touche_4.transform.position; break;
            case "5" : switchKart(); break;
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
