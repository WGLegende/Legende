using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class inventory_slot_master : MonoBehaviour
{
    public int number_of_slot_per_line = 4;
    public int number_of_slot_max;

    public Dictionary<_Slot, inventory_object> slots_list = new Dictionary<_Slot, inventory_object>();
    public enum_manager.type_object type_object_slots;

    public inventory_equipement_slot[] preExistingEquipementSlots;

    public inventory_slot_master slot_master_of_this;

    public GameObject[] GameObjectsToActivate;

    public inventory_slot_master[] slotMasterOnTheSides = new inventory_slot_master[4]; // up right bottom left
    public inventory_show_details selected_slot_details_UI;


    [Serializable]
    public struct sous_type_slots{
        public enum_manager.equipement _type_equipement;
        public inventory_main.consommable _type_consommable;
        public enum_manager.ressource _type_ressource;
        public inventory_main.plan _type_plan;
        public inventory_main.carte _type_carte;
        public inventory_main.quete _type_quete;
        public inventory_main.savoir _type_savoir;
        public inventory_main.relique _type_relique;
    };
    public sous_type_slots _sous_type_slots;

    public int last_hovered_id;

    
    public void Initialize(enum_manager.equipement _type_equipement = 0)
    {
        empty_slots();
        setDependentGameObjects(true);

        if(preExistingEquipementSlots.Length > 0){
            update_equipement_slots();
        }else{
            float parent_width =  GetComponent<RectTransform>().rect.width/number_of_slot_per_line;
            GetComponent<GridLayoutGroup>().cellSize = new Vector2(parent_width, parent_width);
            create_all_slots(_type_equipement);
        }
        
        if(inventory_navigation.instance.active_slot_master == null || (inventory_navigation.instance.active_slot_master != this.GetComponent<inventory_slot_master>() && slot_master_of_this != null)){
            focus_on_this(last_hovered_id);
        }
    }

    public void focus_on_this(int default_hovered_slot_id){
        inventory_navigation.instance.setActiveSlotMaster(this.GetComponent<inventory_slot_master>(), default_hovered_slot_id);
    }

    public void Close()
    {
        setDependentGameObjects(false);
        inventory_navigation.instance.removeActiveSlotMaster();
        empty_slots();
    }


    public void empty_slots(){
        slots_list = new Dictionary<_Slot, inventory_object>();
        if(preExistingEquipementSlots.Length == 0){
            foreach (Transform child in transform){
                Destroy(child.gameObject);
            }
        }
    }

    public void create_all_slots(enum_manager.equipement _type_equipement = 0){

        foreach(inventory_object obj in inventory_objects_manager.instance.object_list.FindAll(
            o => o._type_object == type_object_slots && (_type_equipement != 0 ? _type_equipement == o._type_equipement : true) &&
                (
                    _sous_type_slots._type_equipement != 0 ? o._type_equipement == _sous_type_slots._type_equipement :
                    _sous_type_slots._type_consommable != 0 ? o._type_consommable == _sous_type_slots._type_consommable :
                    _sous_type_slots._type_ressource != 0 ? o._type_ressource == _sous_type_slots._type_ressource :
                    _sous_type_slots._type_plan != 0 ? o._type_plan == _sous_type_slots._type_plan :
                    _sous_type_slots._type_carte != 0 ? o._type_carte == _sous_type_slots._type_carte :
                    _sous_type_slots._type_quete != 0 ? o._type_quete == _sous_type_slots._type_quete :
                    _sous_type_slots._type_savoir != 0 ? o._type_savoir == _sous_type_slots._type_savoir :
                    _sous_type_slots._type_relique != 0 ? o._type_relique == _sous_type_slots._type_relique: true
                )

            )){
            create_slot_object(obj);
        }




        for(int i = slots_list.Count; i < number_of_slot_max; i++){
            create_slot_object(null);
        }
        
    }

    public void setDependentGameObjects(bool state){
        foreach (GameObject go in GameObjectsToActivate){
            go.SetActive(state);
        }
    }



    public void update_equipement_slots(){
        foreach(inventory_equipement_slot equipement_Slot in preExistingEquipementSlots){
            _Slot slot = equipement_Slot.GetComponent<_Slot>();
            inventory_object obj = equipement_Slot.getEquipedObject();
            slots_list.Add(slot, obj);
        }
    }


    public void create_slot_object(inventory_object obj){
        _Slot newSlot = Instantiate(inventory_navigation.instance._PF_slot).GetComponent<_Slot>();
        newSlot.gameObject.transform.SetParent(GetComponent<Transform>(), false);
        newSlot.set_slot(obj);
        slots_list.Add(newSlot.GetComponent<_Slot>(), obj);
    }

}
