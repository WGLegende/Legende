using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class helpers : MonoBehaviour
{
    public static helpers i;
    void Start(){
        if(i == null){
            i = this;
        }
    }

    public bool isObjectAnArmor(inventory_object obj){
        return Enum.GetName(typeof(enum_manager.equipement), obj._type_equipement).Contains("armure");
    }




}
