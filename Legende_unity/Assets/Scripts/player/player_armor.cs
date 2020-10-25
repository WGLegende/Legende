using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player_armor : MonoBehaviour
{
    public static player_armor instance;
    public List<inventory_object> equipedArmorList = new List<inventory_object>();

    public Dictionary<enum_manager.type_effets, int> extra_armor = new Dictionary<enum_manager.type_effets, int>();


    void Start(){
        if(instance == null){
            instance = this;
        }
    }

    public void Start_player_armor(){
        PlayerPrefs_Manager.instance.initialize_PlayerPrefs_armor();
        
        foreach(string v in PlayerPrefs_Manager.instance.getStringValue("extra_armor").Split(';')){
            extra_armor.Add((enum_manager.type_effets)int.Parse((v.Split(':')[0])), int.Parse(v.Split(':')[1])); 
        }

        // fill equipedArmorList with objects recorded in playerprefs
    }

    public void equipe_armor(inventory_object obj){
        inventory_object current = equipedArmorList.Find(o => o._type_equipement == obj._type_equipement);

        if(current != null){
            current.is_equiped = false;
            equipedArmorList.Remove(current);
        }
        equipedArmorList.Add(obj);

        getEquipementTotalArmor();
    }

    public void change_player_armor(int amount, enum_manager.type_effets type_armor){
        // special change only on armor (repear for exemple)
        extra_armor[type_armor] += amount; 

        // life_list[type_armor][0] = (int)Mathf.Clamp(life_list[type_armor][0] + amount, 0, life_list[type_armor][1]);
        player_life.instance.init_player_life();
    }

    public void getEquipementTotalArmor(){
        
        resetArmorsValues();

        foreach (inventory_object obj in equipedArmorList)
        {
            if(helpers.i.isObjectAnArmor(obj)){
                //Additionner les armures entre elles !
                 player_life.instance.life_list[obj._type_armure] = new int[]{
                    player_life.instance.life_list[obj._type_armure][0] + obj.armure_current, 
                    player_life.instance.life_list[obj._type_armure][1] + obj.armure_max
                };
            }
        }

        // Add extra armors (due to potion for example)
        foreach (int v in Enum.GetValues(typeof(enum_manager.type_effets))){
            player_life.instance.life_list[(enum_manager.type_effets)v] = new int[]{
                player_life.instance.life_list[(enum_manager.type_effets)v][0] + extra_armor[(enum_manager.type_effets)v], 
                player_life.instance.life_list[(enum_manager.type_effets)v][1] + extra_armor[(enum_manager.type_effets)v]
            };
        }

        player_life.instance.init_player_life();
    }

    public void resetArmorsValues(){
        player_life.instance.life_list[enum_manager.type_effets.brut_force]       = new int[]{0,0};
        player_life.instance.life_list[enum_manager.type_effets.hot]   = new int[]{0,0};
        player_life.instance.life_list[enum_manager.type_effets.cold]  = new int[]{0,0};
        player_life.instance.life_list[enum_manager.type_effets.ondes] = new int[]{0,0};
    }


    public int repearArmor(int toolValue, inventory_object obj){
        if(!helpers.i.isObjectAnArmor(obj)){
            return 0;
        }

        int qtyRepeared = obj.armure_max - obj.armure_current;
        obj.armure_current = qtyRepeared >= toolValue ? obj.armure_max : obj.armure_current + qtyRepeared;
        return toolValue - qtyRepeared;
    }






}
