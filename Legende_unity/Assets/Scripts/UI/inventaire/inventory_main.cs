using System;
using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;




public class inventory_main : MonoBehaviour
{
    public static inventory_main instance;

    public enum consommable{ aucun, change_player_caracteristique, change_equipement_caracteristique };
    public enum ressource{ aucun, composite, textile, plante_ou_champignon, liquide, metal, pierre, bois };
    public enum plan{ aucun, plan };
    public enum carte{ aucun, carte };
    public enum quete{ aucun, quete };
    public enum savoir{ aucun, savoir };
    public enum relique{ aucun, relique, composant };


    void Start(){
        if(instance == null){
            instance = this;
        }


        check_if_objects_ids_are_unique();
    }

    public void check_if_objects_ids_are_unique(){
        int same_ids_error =  GameObject.FindObjectsOfType<inventory_object>().Length - GameObject.FindObjectsOfType<inventory_object>().GroupBy(o => o.state_id).ToList().Count();

        if(same_ids_error > 0){
            throw new Exception("WARNING : " + same_ids_error + " objets d'inventaires semblent avoir le même ID !!!");
        }
    }



    IEnumerator load_invetory_TEST(){
        yield return new WaitForSeconds(0.5f);
        load_inventory();
    }


    public void load_inventory(){
            
    }







}





