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
            equipement,
            consommable,
            ressource,
            plan,
            carte,
            quete,
            savoir,
            relique
        };

        public enum equipement{
            aucun,
            arme_CaC,
            arme_Distance,
            arme_Projectile,
            bouclier,
            armure_Tete,
            armure_Corps,
            armure_Mains,
            armure_Pieds
        };

        public enum consommable{ aucun, change_player_caracteristique, change_equipement_caracteristique };
        public enum ressource{ aucun, composite, textile, plante_ou_champignon, liquide, metal, pierre, bois };
        public enum plan{ aucun, plan };
        public enum carte{ aucun, carte };
        public enum quete{ aucun, quete };
        public enum savoir{ aucun, savoir };
        public enum relique{ aucun, relique, composant };


        public enum type_effets{ 
                aucun, 
                brulant,
                glace,
                energie
            };


    public List<inventory_object> object_list = new List<inventory_object>();


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

    public void add_new_object(inventory_object new_obj){
        inventory_object existing_object = object_list.LastOrDefault(o => o.nom == new_obj.nom);

        if(existing_object != null){
            if(new_obj.quantite + existing_object.quantite > new_obj.max_stack){
                int new_stack_quantity = new_obj.quantite + existing_object.quantite-new_obj.max_stack;
                existing_object.quantite = existing_object.max_stack;
                new_obj.quantite = new_stack_quantity;
                object_list.Add(new_obj);
            }else{
                existing_object.quantite += new_obj.quantite;
            }
        }else{
            object_list.Add(new_obj);
        }
    }


    public void load_inventory(){
            
    }







}





