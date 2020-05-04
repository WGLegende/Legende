using System;
using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class inventory_slots_container : MonoBehaviour
{


    public GameObject[] gameObjects_to_open;

    public inventory_slots_container parent_Slot_Container;
    public GameObject _PF_slot;
    public Dictionary<_Slot, inventory_object> slots_list;

    public inventory_main.type_object type_object_slots;
    public inventory_show_details selected_slot_details_UI;

    public int number_of_slot_per_line = 4;
    public int number_of_slot_max;

    public _Slot hovered_slot;
    public int current_hovered_slot_id;

    public bool action_equiper_active;
    public bool action_utiliser_active;
    public bool action_jeter_active;


    [Serializable]
    public struct equipement_slot {
        public inventory_main.type_object type_Object;
        public _Slot Slot;
    }

    public equipement_slot[] _equipement_slot;
    public bool slots_defined_in_editor;

    void Start()
    {
        float parent_width =  GetComponent<RectTransform>().rect.width/number_of_slot_per_line;
        slots_defined_in_editor = _equipement_slot.Count() > 0;

        if(!slots_defined_in_editor){
            GetComponent<GridLayoutGroup>().cellSize = new Vector2(parent_width, parent_width);
        }
    }

    public void empty_slots(){
        foreach (Transform child in transform){
             Destroy(child.gameObject);
         }
    }

    public void create_slots(inventory_slots_container slot_container){
        slots_list = new Dictionary<_Slot, inventory_object>();
        if(slots_defined_in_editor){
            update_defined_slots(slot_container);
        }else{
            create_new_slots(slot_container);
        }
    }




    public void create_new_slots(inventory_slots_container slot_container){
        empty_slots();
        foreach(inventory_object obj in inventory_main.instance.object_list.FindAll(o => o._type_object == type_object_slots)){
            create_slot_object(obj);
        }
        for(int i = slots_list.Count; i < number_of_slot_max; i++){
             create_slot_object(null);
        }


        inventory_navigation.instance.hover_slot(slot_container, 0);
    }


    public void update_defined_slots(inventory_slots_container slot_container){
        foreach (equipement_slot equipement_slot in _equipement_slot)
        {
                inventory_object obj = inventory_main.instance.object_list.FirstOrDefault(o => o._type_object == equipement_slot.type_Object && o.is_equiped);
                equipement_slot.Slot.set_slot(obj);
                slots_list.Add(equipement_slot.Slot, obj);




            // if(equipement_slot.type_Object == inventory_main.type_object.consommable_player || equipement_slot.type_Object == inventory_main.type_object.consommable_ressources){
            //     equipement_slot.Slot.set_slot(obj);
            //     slots_list.Add(equipement_slot.Slot, null);
            // }else{
            //     inventory_object obj = inventory_main.instance.object_list.FirstOrDefault(o => o._type_object == equipement_slot.type_Object && o.is_equiped);
            //     equipement_slot.Slot.set_slot(obj);
            //     slots_list.Add(equipement_slot.Slot, obj);
            // }
        }

        
        if(hovered_slot == null){
            inventory_navigation.instance.hover_slot(slot_container, 0);
        }else{
            inventory_navigation.instance.hover_slot(slot_container, current_hovered_slot_id);
        }
    }


    public void create_slot_object(inventory_object obj){

            _Slot newSlot = Instantiate(inventory_navigation.instance._PF_slot).GetComponent<_Slot>();
            newSlot.gameObject.transform.SetParent(GetComponent<Transform>(), false);
            newSlot.set_slot(obj);
            slots_list.Add(newSlot.GetComponent<_Slot>(), obj);

    }



}
