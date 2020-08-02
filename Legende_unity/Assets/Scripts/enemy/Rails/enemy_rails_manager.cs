using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy_rails_manager : MonoBehaviour{

    public static enemy_rails_manager instance;
   
   
    public List<enemy_nest_rails> list_nest_rails = new List<enemy_nest_rails>();

    void Awake(){

        if(instance == null){
            instance = this;
        }   
    }

    public void reinitializeAllEnemy(){

        foreach(enemy_nest_rails e in list_nest_rails){

            e.StopAllCoroutines();
            e.justeOnce = false;
            e.destroyAllEnemy(); 
        }
    }

    

}
