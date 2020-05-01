using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player_equipement : MonoBehaviour
{
    public static player_equipement instance;





    void Start()
    {
        if(instance == null){
            instance = this;
        }


    }

    public void equipe_un_objet(inventory_object obj){
        Debug.Log("j'equipe " + obj.nom);

        // Unequiped current equiped object
        inventory_object currently_equiped = inventory_main.instance.object_list.FirstOrDefault(o => o._type_object == obj._type_object && o.is_equiped);
        if(currently_equiped != null){
            currently_equiped.is_equiped = false;
        }

        // Equip selected object
        obj.is_equiped = true;


    }
}
