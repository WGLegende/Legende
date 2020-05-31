using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class inventory_navigation : MonoBehaviour
{
    public static inventory_navigation instance;

    public GameObject _PF_slot;

    public inventory_slot_master active_slot_master = null;
    public int hovered_slot_id = 0;

    void Start()
    {
        if(instance == null){
            instance = this;
        }
    }

    public void navigateInMainMenus(int direction){
        inventory_main_structure.instance.navigate_inventory_panel(direction);
    }
    
    public void setActiveSlotMaster(inventory_slot_master slot_master, int default_hovered_slot_id){
        if(active_slot_master != null){
            unhover_all_slot();
        }
        active_slot_master = slot_master;
        hovered_slot_id = default_hovered_slot_id;
        _Slot slot = active_slot_master.slots_list.ElementAt(hovered_slot_id).Key;
        hover_slot(slot, true);
        showSlotObjectDetail(slot.object_in_slot);
    }
    public void removeActiveSlotMaster(){
        active_slot_master = null;
    }


    public void hover_slot(_Slot hovered_slot, bool hover_on){
        active_slot_master.last_hovered_id = hovered_slot_id;
        hovered_slot.hover_slot(hover_on);
    }

    public void unhover_all_slot(){
        foreach(_Slot slot in active_slot_master.slots_list.Keys){
            hover_slot(slot, false);
        }
    }

public void navigate_in_slots(string direction){
    int new_slot_id = 0;

    int slotsPerLine = active_slot_master.number_of_slot_per_line;
    int slotsMax = active_slot_master.number_of_slot_max;
     
    new_slot_id = direction == "up" ?    hovered_slot_id - slotsPerLine :
                  direction == "right" ? hovered_slot_id + 1 :
                  direction == "down" ?  hovered_slot_id + slotsPerLine :
                  direction == "left" ?  hovered_slot_id - 1 : 0;

    if(hovered_slot_id < slotsPerLine && direction == "up" && active_slot_master.slotMasterOnTheSides[0] != null){
        active_slot_master.slotMasterOnTheSides[0].focus_on_this(slotsMax - (slotsPerLine-hovered_slot_id));
    }
    else if(hovered_slot_id % slotsPerLine == slotsPerLine-1 && direction == "right" && active_slot_master.slotMasterOnTheSides[1] != null){
        active_slot_master.slotMasterOnTheSides[1].focus_on_this(hovered_slot_id - (slotsPerLine-1));
    }
    else if(hovered_slot_id >= slotsMax-slotsPerLine && direction == "down" && active_slot_master.slotMasterOnTheSides[2] != null){
        active_slot_master.slotMasterOnTheSides[2].focus_on_this((slotsPerLine + hovered_slot_id) - slotsMax);
    }
    else if(hovered_slot_id % slotsPerLine == 0 && direction == "left" && active_slot_master.slotMasterOnTheSides[3] != null){
        active_slot_master.slotMasterOnTheSides[3].focus_on_this(hovered_slot_id + (slotsPerLine-1));
    }else if(new_slot_id >= 0 && new_slot_id < slotsMax){
        // hover slot normalement
        unhover_all_slot();
        hovered_slot_id = new_slot_id;
        _Slot slot = active_slot_master.slots_list.ElementAt(hovered_slot_id).Key;
        hover_slot(slot, true);
        showSlotObjectDetail(slot.object_in_slot);
    }
 }

    public void showSlotObjectDetail(inventory_object object_in_slot){
        if(active_slot_master.selected_slot_details_UI != null){
            active_slot_master.selected_slot_details_UI.Show_Object_Detail(object_in_slot);
        }
    }



    public void action_A(){
        _Slot slot = active_slot_master.slots_list.ElementAt(hovered_slot_id).Key;
        if(slot != null){
            inventory_equipement_slot slot_equipement = slot.GetComponent<inventory_equipement_slot>();

            if(slot_equipement != null){
                // entre dans un nouveau slot master ou sont rangés les slots de ce type d'équipement
                slot_equipement.slot_master_slave.Initialize(slot_equipement.type_equipement);
            }
            else if(slot.object_in_slot != null){
                inventory_actions.instance.create_action_on_inventory_object(slot.object_in_slot, active_slot_master.slot_master_of_this);
            }
        }
    }


    public void action_Y(){
        _Slot slot = active_slot_master.slots_list.ElementAt(hovered_slot_id).Key;
        if(slot != null){
            // jeter l'objet si possible
            inventory_actions.instance.jeter_un_objet(slot, active_slot_master.slot_master_of_this);
        }
    }

    public void action_Back(){
        if(active_slot_master.slot_master_of_this != null){
            inventory_slot_master slot_master_of_this = active_slot_master.slot_master_of_this;
            active_slot_master.Close();
            slot_master_of_this.Initialize();
        }else{
            inventory_main_structure.instance.navigate_inventory_panel(0);
        }
    }


        // if(selected_inventory_slots_container.hovered_slot != null && selected_inventory_slots_container.hovered_slot.children_slots_navigation != null){
        //     enter_a_slot_container(selected_inventory_slots_container.hovered_slot.children_slots_navigation);
        // }else{
        //     if(selected_inventory_slots_container.action_utiliser_active){
        //         utiliser_objet(selected_inventory_slots_container.hovered_slot);
        //     }else if(selected_inventory_slots_container.action_equiper_active){
        //         equiper_objet(selected_inventory_slots_container.hovered_slot);
        //     }
        // }



    // public void navigateInMainMenus(int direction){
    //     if(inventory_part_selected + direction >= 0 && inventory_part_selected + direction < main_inventory_part.Length){
    //         inventory_part_selected += direction;
    //         back_all_open_slots_container();
    //         closeAllMainParts();
    //         enter_a_slot_container(main_inventory_part[inventory_part_selected]);
    //     }
    // }

    // public void closeAllMainParts(){
    //     for(int i = 0; i < main_inventory_part.Length; i++){
    //         main_inventory_part[i].gameObject.SetActive(false);
    //     }
    // }

    // public void back_all_open_slots_container(){// close every active slots containers
    //     back();
    //     back();
    //     back();
    // }

    // public void open_close_inventory(){
    //     bool is_open = !inventory_main.instance.TR_Inventaire.gameObject.activeSelf;
    //     GamePad_manager.instance.open_close_inventory(is_open);

    //     inventory_main.instance.TR_Inventaire.gameObject.SetActive(is_open);    

    //     if(is_open){
    //         closeAllMainParts();
    //         enter_a_slot_container(main_inventory_part[inventory_part_selected]);
    //     }else{
    //         back_all_open_slots_container();
    //     }

    // }

    // public void enter_a_slot_container(inventory_slots_container inventory_slots_container){

    //     selected_inventory_slots_container = inventory_slots_container; // for test ( a mettre en propriete de la methode)

    //     if(selected_inventory_slots_container.gameObjects_to_open.Length > 0){
    //         foreach (GameObject g in selected_inventory_slots_container.gameObjects_to_open)
    //         {
    //             g.SetActive(true);
    //         }
    //     }
    //     selected_inventory_slots_container.gameObject.SetActive(true);
    //     ////// selected_inventory_slots_container.create_slots(selected_inventory_slots_container);
    // }

   


    
    // public void action_jeter(){
    //     inventory_object selected_object = selected_inventory_slots_container.slots_list.FirstOrDefault(o => o.Key == selected_inventory_slots_container.hovered_slot).Value;

    //     if(selected_object != null){
    //         Debug.Log("Jeter " + selected_object.nom);
            
    //     }else{
    //         Debug.Log("impossible de jeter");
    //     }
    // }


    // public void go_to_shortcut(int direction){
    //     if(selected_inventory_slots_container.hovered_slot != null){
    //         Debug.Log("go_to_shortcut " + direction);
    //         inventory_object obj = selected_inventory_slots_container.slots_list.FirstOrDefault(o => o.Key == selected_inventory_slots_container.hovered_slot).Value;
    //         inventory_shortcuts.instance.create_shortcut(direction, obj);
    //     }
    // }


    // public void equiper_objet(_Slot slot){
    //     inventory_object obj = selected_inventory_slots_container.slots_list.FirstOrDefault(o => o.Key == slot).Value;

    //     if(obj == null){
    //         Debug.Log("Impossible d'équiper");
    //         return;
    //     }

    //     // envoi l'objet à la logique du player pour qu'il l'équipe (avec les animations et tout le tralala)
    //     player_equipement.instance.equipe_un_objet(obj);

    //     // refresh l'endroit ou sont affichés les objets équipés
    //     int current_hovered_slot_id = selected_inventory_slots_container.parent_Slot_Container.current_hovered_slot_id;
    //     //////// selected_inventory_slots_container.parent_Slot_Container.create_slots(selected_inventory_slots_container.parent_Slot_Container);
    //     hover_slot(selected_inventory_slots_container.parent_Slot_Container, current_hovered_slot_id);

    // }


    // public void utiliser_objet(_Slot slot){
    //     inventory_object obj = selected_inventory_slots_container.slots_list.FirstOrDefault(o => o.Key == slot).Value;

    //     if(obj == null){
    //         Debug.Log("Impossible d'utiliser");
    //     }else{
    //         player_utilisables.instance.utilise_un_objet(obj);
    //     }
    // }


}
