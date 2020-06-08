using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.UI;

public class tests_manager : MonoBehaviour
{
    public bool destroyPlayerPrefs;
 
    void Start() {
        if(destroyPlayerPrefs){
            PlayerPrefs.DeleteAll();
        }
    }



    void Update(){

        if(Input.GetKeyDown(KeyCode.O)){
        
            foreach(enemy enemy in enemy_manager.instance.mesEnemyList){
                enemy.current_comportement = enemy_manager.comportement.dead;
            }

    

        }


    }
}
