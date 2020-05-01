using System;
using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class inventory_main : MonoBehaviour
{
    public static inventory_main instance;

    public enum inventory_main_parts{
         equipement, 
         inventaire,
         reliques 
    };

        public enum type_object{ 
            aucun,
            arme_CaC,
            arme_Distance,
            arme_Projectile,
            armure_Tete,
            armure_Corps,
            armure_Mains,
            armure_Pieds,
            consommable_player,
            consommable_ressources,
            relique_relique,
            relique_composant 
        };


            public enum type_consommable_player{ 
                vie, 
                vapeur
            };
            public enum type_consommable_ressource{ 

            };

            public enum type_effets{ 
                    aucun, 
                    brulant,
                    glace,
                    energie
                };

    public Transform TR_Inventaire, TR_Inventaire_equipement, TR_Inventaire_consommables, TR_Inventaire_relique;




    public List<inventory_object> object_list = new List<inventory_object>();
    // public Dictionary<type_object, Base_sous_objects> Base_sous_objects_list = new Dictionary<type_object, Base_sous_objects>();


    void Start(){
        if(instance == null){
            instance = this;
        }



        // for (int x = 0; x < _Base_sous_objects.Length; x++) {
        //     Base_sous_objects_list.Add(((inventory_main.type_object)x), _Base_sous_objects[x]);
        // }


        // StartCoroutine(load_invetory_TEST());

        // foreach(type_object type in Enum.GetValues(typeof(type_object)))
        // {
        //     slots_lists.Add(type, new List<Transform>());
        // }

        // foreach (KeyValuePair<type_object, List<Transform>> slot_container in slots_lists)
        // {
        //     Debug.Log(slot_container.Key.ToString());

        // }
    }
    IEnumerator load_invetory_TEST(){
        yield return new WaitForSeconds(0.5f);
        load_inventory();
    }



    void FixedUpdate(){


        
    }


    public void add_new_object(inventory_object obj){
        object_list.Add(obj);

        //for test only

        // Inventaire.gameObject.SetActive(false);
        // _TypesInventaires.Inventaire_equipement.gameObject.SetActive(false);
        // _TypesInventaires.Inventaire_consommables.gameObject.SetActive(false);
        // _TypesInventaires.Inventaire_relique.gameObject.SetActive(false);

    }

    public void open_inventory(){
        
        TR_Inventaire.gameObject.SetActive(true);
        TR_Inventaire_equipement.gameObject.SetActive(true);
        TR_Inventaire_consommables.gameObject.SetActive(false);
        TR_Inventaire_relique.gameObject.SetActive(false);
    }


    public void load_inventory(){
            
        // foreach(inventory_object obj in object_list){

        //     _inventory_slot Equiped_slot = Base_sous_objects_list[obj._type_object].Equiped_slot;
        //     inventory_slot_main SlotContainer = Base_sous_objects_list[obj._type_object].SlotContainer; // pas utile ici

        //     if(obj.is_equiped){
        //         Equiped_slot.show_object_in_slot(obj);
        //     }
        // }
    }



    // public void load_active_slot_objects(inventory_slot_main active_slot, type_object type_object){
    //         Debug.Log("load_active_slot_objects");

    //     foreach(inventory_object obj in object_list){

    //         // _inventory_slot Equiped_slot = Base_sous_objects_list[obj._type_object].Equiped_slot;
    //         // inventory_slot_main SlotContainer = Base_sous_objects_list[obj._type_object].SlotContainer; // pas utile ici


    //         // if(obj.is_equiped){
    //         //     Equiped_slot.show_object_in_slot(obj);
    //         // }


    //     }
    // }

    // float distance_avec_le_joueur;
    // oldComportement = "attend"
    // comportement = "attaque"


    // void Update(){
    //     distance_avec_le_joueur = Vector3.Distance(...);

    //     if(oldComportement != comportement){

    //         if(comportement == "attaque"){
    //             StartCoroutine(attaque());
    //         }
    //         //else if...

    //         oldComportement = comportement;
    //     }
    // }



    // public IEnumerator attaque(){

    //         // Ta logique que tu ne fais qu'une fois  (quand l'attaque commence)

    //         while(distance_avec_le_joueur < 10f){

    //             // Ta logique que tu répète (comme dans l'update)
                
    //             yield return new WaitForSeconds(0.02f);
    //         }

    //     yield return new WaitForSeconds(0.1f);
    // }











}















