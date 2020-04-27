 using System;
 using System.Linq;
 using UnityEngine;
 using UnityEngine.UI;
 using System.Collections;
 
 public class inventory_object : MonoBehaviour {

    public string nom;
    public string description;
    public Texture2D image;



    public inventory_main.inventory_main_parts _type_inventory;

    public inventory_main.type_object _type_object;

    public inventory_main.type_consommable_player _type_consommable_player;
    public inventory_main.type_consommable_ressource _type_consommable_ressource;

    public inventory_main.type_effets _type_effets_secondaire = new inventory_main.type_effets();

    // public inventory_main.type_object _type_object;
    // public inventory_main.sous_type_arme _type_arme;
    // public inventory_main.sous_type_armure _type_armure;

    // Armes : common
    public float vitesseCoup;
    public float degatsInfligesMin;
    public float degatsInfligesMax;
    public int pourcentageCritique;
    
    public float puissanceDeRecul;

    
    public float degatsSecondairesInfligesMin;
    public float degatsSecondairesInfligesMax;


    // Armes : projectile
    // Armes : distance
    public float portee;

    // ... (public int quantite;)

  // Armures : common
    public float montantArmure_min;
    public float montantArmure_max;
    public float armureSecondaireMin;
    public float armureSecondaireMax;


  // consommable : common
    public int quantite;

  // ressource : common

  // relique : common

  // relique_composant : common

    public bool is_equiped = false;
    public int[] inventory_slot_yx = new int[]{0, 0};

    void Start(){

        // for tests only :


    }

    void OnTriggerEnter(Collider collider){ // A modifier avec la logique d'interactable
        if(collider.tag == "Player"){
            Debug.Log("add " + nom + " to inventory");
            addObject();
        }
    }

    public void addObject(){
      if(!inventory_main.instance.object_list.Any(o => o._type_object == _type_object && o.is_equiped)){
          is_equiped = true;
        }
        inventory_main.instance.add_new_object(GetComponent<inventory_object>());
        gameObject.SetActive(false);
    }







 }



