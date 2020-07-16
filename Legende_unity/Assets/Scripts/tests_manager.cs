using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tests_manager : MonoBehaviour
{
    public bool destroyPlayerPrefs;
    public bool player_never_die;
    public bool test_player_kart;
    public bool always_vapeur;

    public GameObject Player;
    public GameObject Playerkart;


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
    }


    void Update(){

        if(Input.GetKeyDown(KeyCode.O)){
            foreach(enemy enemy in enemy_manager.instance.mesEnemyList){
                enemy.current_comportement = enemy_manager.comportement.dead;
            }
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
