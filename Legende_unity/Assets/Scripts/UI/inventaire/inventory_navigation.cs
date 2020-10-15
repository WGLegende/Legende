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

    public void go_to_shortcut(int direction){
        _Slot slot = active_slot_master.slots_list.ElementAt(hovered_slot_id).Key;
        if(slot != null){
            inventory_shortcuts.instance.create_shortcut(direction, slot.object_in_slot);
        }
    }
}
