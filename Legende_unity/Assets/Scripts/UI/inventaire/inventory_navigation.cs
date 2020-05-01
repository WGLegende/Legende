using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class inventory_navigation : MonoBehaviour
{
    public static inventory_navigation instance;

    public enum nav_direction{ up, right, down, left };

    public GameObject _PF_slot;

    public inventory_slots_container selected_inventory_slots_container; 

    public inventory_slots_container[] main_inventory_part; 
    int inventory_part_selected = 0; 


    void Start()
    {
        if(instance == null){
            instance = this;
        }
    }



    public void navigateInMainMenus(int direction){
        if(inventory_part_selected + direction >= 0 && inventory_part_selected + direction < main_inventory_part.Length){
            inventory_part_selected += direction;
            back_all_open_slots_container();
            closeAllMainParts();
            enter_a_slot_container(main_inventory_part[inventory_part_selected]);
        }
    }


    public void closeAllMainParts(){
        for(int i = 0; i < main_inventory_part.Length; i++){
            main_inventory_part[i].gameObject.SetActive(false);
        }
    }

    public void back_all_open_slots_container(){// close every active slots containers
        back();
        back();
        back();
    }

    public void open_close_inventory(){
        bool is_open = !inventory_main.instance.TR_Inventaire.gameObject.activeSelf;
        GamePad_manager.instance.open_close_inventory(is_open);

        inventory_main.instance.TR_Inventaire.gameObject.SetActive(is_open);    

        if(is_open){
            closeAllMainParts();
            enter_a_slot_container(main_inventory_part[inventory_part_selected]);
        }else{
            back_all_open_slots_container();
        }

    }

    public void enter_a_slot_container(inventory_slots_container inventory_slots_container){

        selected_inventory_slots_container = inventory_slots_container; // for test ( a mettre en propriete de la methode)

        if(selected_inventory_slots_container.gameObjects_to_open.Length > 0){
            foreach (GameObject g in selected_inventory_slots_container.gameObjects_to_open)
            {
                g.SetActive(true);
            }
        }
        selected_inventory_slots_container.gameObject.SetActive(true);
        selected_inventory_slots_container.create_slots(selected_inventory_slots_container);
    }

    public void navigate_in_slots(int direction){

        int new_slot_id = 
            (nav_direction)direction == inventory_navigation.nav_direction.up  ?
                selected_inventory_slots_container.current_hovered_slot_id-selected_inventory_slots_container.number_of_slot_per_line : 
            (nav_direction)direction == inventory_navigation.nav_direction.right  ?
                selected_inventory_slots_container.current_hovered_slot_id+1 :
            (nav_direction)direction == inventory_navigation.nav_direction.down  ?
                selected_inventory_slots_container.current_hovered_slot_id+selected_inventory_slots_container.number_of_slot_per_line :
            (nav_direction)direction == inventory_navigation.nav_direction.left  ?
                selected_inventory_slots_container.current_hovered_slot_id-1 : 0;

        if(new_slot_id >= 0 && new_slot_id < selected_inventory_slots_container.slots_list.Count()){
            hover_slot(selected_inventory_slots_container, new_slot_id);
        }
    }

    public void hover_slot(inventory_slots_container slot_container, int id){
        if(slot_container.slots_list.Count == 0){
            return;
        }

        if(slot_container != null && slot_container.hovered_slot != null){
            slot_container.hovered_slot.gameObject.GetComponent<Outline>().enabled = false;
        }
        slot_container.hovered_slot = slot_container.slots_list.ElementAt(id).Key;
        slot_container.hovered_slot.gameObject.GetComponent<Outline>().enabled = true;

        inventory_object hovered_object = slot_container.slots_list.FirstOrDefault(o => o.Key == slot_container.hovered_slot).Value;
        slot_container.selected_slot_details_UI.Show_Object_Detail(hovered_object, slot_container);

        slot_container.current_hovered_slot_id = id;
    }

    public void action_equiper_ou_utiliser(){
        if(selected_inventory_slots_container.hovered_slot != null && selected_inventory_slots_container.hovered_slot.children_slots_navigation != null){
            enter_a_slot_container(selected_inventory_slots_container.hovered_slot.children_slots_navigation);
        }else{
            if(selected_inventory_slots_container.action_utiliser_active){
                utiliser_objet(selected_inventory_slots_container.hovered_slot);
            }else if(selected_inventory_slots_container.action_equiper_active){
                equiper_objet(selected_inventory_slots_container.hovered_slot);
            }
        }
    }

    
    public void action_jeter(){
        inventory_object selected_object = selected_inventory_slots_container.slots_list.FirstOrDefault(o => o.Key == selected_inventory_slots_container.hovered_slot).Value;

        if(selected_object != null){
            Debug.Log("Jeter " + selected_object.nom);
            
        }else{
            Debug.Log("impossible de jeter");
        }
    }


    public void go_to_shortcut(int direction){
        if(selected_inventory_slots_container.hovered_slot != null){
            Debug.Log("go_to_shortcut " + direction);
            inventory_object obj = selected_inventory_slots_container.slots_list.FirstOrDefault(o => o.Key == selected_inventory_slots_container.hovered_slot).Value;
            inventory_shortcuts.instance.create_shortcut(direction, obj);
        }
    }


    public void equiper_objet(_Slot slot){
        inventory_object obj = selected_inventory_slots_container.slots_list.FirstOrDefault(o => o.Key == slot).Value;

        if(obj == null){
            Debug.Log("Impossible d'équiper");
            return;
        }

        // envoi l'objet à la logique du player pour qu'il l'équipe (avec les animations et tout le tralala)
        player_equipement.instance.equipe_un_objet(obj);

        // refresh l'endroit ou sont affichés les objets équipés
        int current_hovered_slot_id = selected_inventory_slots_container.parent_Slot_Container.current_hovered_slot_id;
        selected_inventory_slots_container.parent_Slot_Container.create_slots(selected_inventory_slots_container.parent_Slot_Container);
        hover_slot(selected_inventory_slots_container.parent_Slot_Container, current_hovered_slot_id);

    }


    public void utiliser_objet(_Slot slot){
        inventory_object obj = selected_inventory_slots_container.slots_list.FirstOrDefault(o => o.Key == slot).Value;

        if(obj == null){
            Debug.Log("Impossible d'utiliser");
        }else{
            player_utilisables.instance.utilise_un_objet(obj);
        }
    }

    public void back(){
        if(selected_inventory_slots_container.parent_Slot_Container != null){
            if(selected_inventory_slots_container.gameObjects_to_open.Length > 0){
                foreach (GameObject g in selected_inventory_slots_container.gameObjects_to_open)
                {
                    g.SetActive(false);
                }
            }
            selected_inventory_slots_container.gameObject.SetActive(false);
            selected_inventory_slots_container.selected_slot_details_UI.Show_Object_Detail(null, null);
            enter_a_slot_container(selected_inventory_slots_container.parent_Slot_Container);
        }
    }
}
