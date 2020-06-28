using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.UI;

public class tests_manager : MonoBehaviour
{
    public bool destroyPlayerPrefs;
    public bool player_never_die;
    public Transform Player;
    public Transform Playerkart;
 
    void Start() {
        if(destroyPlayerPrefs){
            PlayerPrefs.DeleteAll();
        }
        if(player_never_die){
           InvokeRepeating("PlayerInfinityPv",0, 2.0f);
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
}
