using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.UI;

public class tests_manager : MonoBehaviour
{
    public bool destroyPlayerPrefs;
    public bool player_never_die;
    public GameObject Player;
    public GameObject Playerkart;
    public bool test_player_kart;
    

    void Start() {

       
        if(destroyPlayerPrefs){
            PlayerPrefs.DeleteAll();
        }
        if(player_never_die){
           InvokeRepeating("PlayerInfinityPv",0, 2.0f);
        } 
          if(test_player_kart){
               Invoke("switchKart",0.1f);
           
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


     void switchKart(){
       player_actions.instance.do_action_enter_kart(EnterChariot.instance);
    }
}
