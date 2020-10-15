using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class inventory_equipement_slot : MonoBehaviour
{
    public enum_manager.equipement type_equipement;
    public inventory_slot_master slot_master_slave;


    void Start()
    {
        
    }

    public inventory_object getEquipedObject()
    {
        
        // inventory_object test = new inventory_object();

        // foreach(inventory_object o in inventory_objects_manager.instance.object_list){
        //     if(o._type_object == enum_manager.type_object.equipement && o._type_equipement == type_equipement && o.is_equiped){
        //         GetComponent<_Slot>().set_slot(o);
        //         test =  o;
        //     }
        // }
        inventory_object obj = inventory_objects_manager.instance.object_list.FirstOrDefault(o => o._type_object == enum_manager.type_object.equipement && o._type_equipement == type_equipement && o.is_equiped);
        GetComponent<_Slot>().set_slot(obj);
        return obj;
    }
}
