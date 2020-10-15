using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPrefs_Manager : MonoBehaviour
{
    public static PlayerPrefs_Manager instance;

    void Awake(){
        if(instance == null){
            instance = this;
        }
    }

    public int getIntValue(string key){
        return PlayerPrefs.GetInt(key);
    }
    public void incrementOrDecrementInt(string key, int value){
        PlayerPrefs.SetInt(key, PlayerPrefs.GetInt(key) + value);
    }


    public void initialize_PlayerPrefs_life(){
        if(PlayerPrefs.GetInt("life_current_life") == 0){
            PlayerPrefs.SetInt("life_current_life", 5);
            PlayerPrefs.SetInt("life_max_life", 5);
        }        
    }



    public void saveAll(){
        PlayerPrefs.SetInt("life_current_life", player_life.instance.life_list[enum_manager.type_effets.life][0]);
        PlayerPrefs.SetInt("life_max_life", player_life.instance.life_list[enum_manager.type_effets.life][1]);
        
        

    }





}
