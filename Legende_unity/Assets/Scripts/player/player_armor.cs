using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player_armor : MonoBehaviour
{
    public static player_armor instance;
    public List<inventory_object> equipedArmorList = new List<inventory_object>();

    void Start(){
        if(instance == null){
            instance = this;
        }
    }

    public void initArmors(){
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
