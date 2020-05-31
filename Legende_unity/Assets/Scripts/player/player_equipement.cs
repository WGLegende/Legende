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

        switch(obj.nom){

            case "arc": player_gamePad_manager.instance.modePlayer = "bow"; player_gamePad_manager.instance.Player_Animator.SetTrigger("changeEquipement");
            break;
            case "Epee d'acier": player_gamePad_manager.instance.modePlayer = "sword"; player_gamePad_manager.instance.Player_Animator.SetTrigger("changeEquipement");
            break;
        }

        if(obj._type_equipement == inventory_main.equipement.arme_CaC){

            //equipe epee
        }
        else if(obj._type_equipement == inventory_main.equipement.arme_Distance){

            //equipe arc
        }
        
      

    }
}
