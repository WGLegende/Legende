using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class inventory_actions : MonoBehaviour
{
    public static inventory_actions instance;

    void Start(){
        if(instance == null){
            instance = this;
        }
    }

    public void jeter_un_objet(_Slot slot, inventory_slot_master slot_master){

        if(slot.object_in_slot.jetable){

            // Check if it is an equiped object
            inventory_object currently_equiped = inventory_objects_manager.instance.object_list.FirstOrDefault(o => o.state_id == slot.object_in_slot.state_id && o.is_equiped);
            if(currently_equiped != null){
                currently_equiped.is_equiped = false;
            }
            inventory_objects_manager.instance.object_list.Remove(inventory_objects_manager.instance.object_list.First(o => o.state_id == slot.object_in_slot.state_id));
            
            slot_master.empty_slots();
            slot_master.update_equipement_slots();
            slot.set_slot(null);
        }
    }

    public void create_action_on_inventory_object(inventory_object obj, inventory_slot_master slot_master){
        switch(obj._type_object){
            case inventory_main.type_object.equipement : equipe_object(obj, slot_master);
                break;
            case inventory_main.type_object.consommable : consomme_object(obj);
                break;
            case inventory_main.type_object.ressource : // nothing needs to happen so far...
                break;
            case inventory_main.type_object.relique : equipe_relique(obj);
                break;
            case inventory_main.type_object.savoir : affiche_savoir(obj);
                break;
            case inventory_main.type_object.plan : affiche_plan(obj);
                break;
            case inventory_main.type_object.quete : affiche_quete(obj);
                break;
            case inventory_main.type_object.carte : affiche_carte(obj);
                break;
        }
    }

    public void equipe_object(inventory_object obj, inventory_slot_master slot_master){
        Debug.Log("J'equipe l'objet " + obj.nom);

        // Unequiped current equiped object
        inventory_object currently_equiped = inventory_objects_manager.instance.object_list.FirstOrDefault(o => o._type_object == obj._type_object && o._type_equipement == obj._type_equipement && o.is_equiped);
        if(currently_equiped != null){
            currently_equiped.is_equiped = false;
        }

        // Equip selected object
        obj.is_equiped = true;

        player_equipement.instance.equipe_un_objet(obj);
        slot_master.empty_slots();
        slot_master.update_equipement_slots();



    }
    public void consomme_object(inventory_object obj){

        
    }
    public void equipe_relique(inventory_object obj){

        
    }
    public void affiche_savoir(inventory_object obj){

        
    }
    public void affiche_plan(inventory_object obj){

        
    }
    public void affiche_quete(inventory_object obj){

        
    }
    public void affiche_carte(inventory_object obj){

        
    }
}
