using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player_life : MonoBehaviour
{
    public static player_life instance;
    public GameObject PF_Player_Life;

    public Dictionary<enum_manager.type_effets, int[]> life_list = new Dictionary<enum_manager.type_effets, int[]>();


    void Start(){
        if(instance == null){
            instance = this;
        }
    }


    public void Start_player_life()
    {
        if(instance == null){
            instance = this;
        }
        PlayerPrefs_Manager.instance.initialize_PlayerPrefs_life();
        
        life_list.Add(enum_manager.type_effets.none,       new int[]{0,0});
        life_list.Add(enum_manager.type_effets.life,        new int[]{PlayerPrefs_Manager.instance.getIntValue("life_current_life"), PlayerPrefs_Manager.instance.getIntValue("life_max_life")});
        life_list.Add(enum_manager.type_effets.brut_force,  new int[]{0, 0});
        life_list.Add(enum_manager.type_effets.hot,         new int[]{0, 0});
        life_list.Add(enum_manager.type_effets.cold,        new int[]{0, 0});
        life_list.Add(enum_manager.type_effets.ondes,       new int[]{0, 0});

        player_armor.instance.getEquipementTotalArmor();
    }

    public void init_player_life(){
        foreach (Transform child in UI_Main.instance.Player_Life_Container){
            Destroy(child.gameObject);
        }
        foreach (Transform child in UI_Main.instance.Player_Armor_Container){
            Destroy(child.gameObject);
        }

        for(int j = 0; j < life_list.Count(); j++){
            for(int i = 1; i <= life_list[(enum_manager.type_effets)j][1]; i++){
                _PLife newLife = Instantiate(PF_Player_Life).GetComponent<_PLife>(); // Ajoute un coeur vide
                newLife.gameObject.transform.SetParent( (j == 1 ? UI_Main.instance.Player_Life_Container : UI_Main.instance.Player_Armor_Container), false);
                newLife.initialize_life((enum_manager.type_effets)j, i <= life_list[(enum_manager.type_effets)j][0]);
            }
        }

        // Extra armor
        foreach(KeyValuePair<enum_manager.type_effets, int> entry in player_armor.instance.extra_armor)
        {
            for(int i = 0; i < entry.Value; i++){
                _PLife newLife = Instantiate(PF_Player_Life).GetComponent<_PLife>(); // Ajoute un coeur vide
                newLife.gameObject.transform.SetParent(UI_Main.instance.Player_Armor_Container, false);
                newLife.initialize_life(entry.Key, true);
            }
        }
    }

    void addNewPlayerLife(){
        PlayerPrefs_Manager.instance.incrementOrDecrementInt("life_max_life", 1);
        PlayerPrefs_Manager.instance.incrementOrDecrementInt("life_current_life", 1);
        init_player_life();
    }
    
    public void change_player_life(int amount, enum_manager.type_effets type_degats = enum_manager.type_effets.brut_force){
        if(amount < 0){
            // check if armor can reduce damage

           for(int j = 2; j < life_list.Count()-2; j++){
               if((enum_manager.type_effets)j == type_degats){

                   int amountValueAfterDefense = life_list[(enum_manager.type_effets)j][0] + amount;

                   life_list[(enum_manager.type_effets)j][0] = amountValueAfterDefense <= 0 ? 0 : amountValueAfterDefense;
                    amount = amountValueAfterDefense > 0 ? 0 : amountValueAfterDefense;
               }
            }
        } 
            
        life_list[enum_manager.type_effets.life][0] = (int)Mathf.Clamp(life_list[enum_manager.type_effets.life][0] + amount, 0, life_list[enum_manager.type_effets.life][1]);

        init_player_life();

        if(life_list[enum_manager.type_effets.life][0] <= 0){
            player_main.instance.player_dies();  
        } else {
            PlayerPrefs_Manager.instance.saveAll();
        }
    }

    public void change_player_life_to_full(){
        change_player_life(life_list[enum_manager.type_effets.life][1]);
    }
}
