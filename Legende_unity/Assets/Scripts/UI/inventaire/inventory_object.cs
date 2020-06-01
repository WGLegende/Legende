 using System;
 using System.Linq;
 using UnityEngine;
 using UnityEngine.UI;
 using System.Collections;
 
 public class inventory_object : MonoBehaviour {

    public string state_id;

    public string nom;
    public string description;
    public Texture2D image;

    public bool jetable = true;
    public int max_stack = 1;

    public inventory_main.inventory_main_parts _type_inventory;

    public inventory_main.type_object _type_object;
    public inventory_main.equipement _type_equipement;
    public inventory_main.consommable _type_consommable;
    public inventory_main.ressource _type_ressource;
    public inventory_main.plan _type_plan;
    public inventory_main.carte _type_carte;
    public inventory_main.quete _type_quete;
    public inventory_main.savoir _type_savoir;
    public inventory_main.relique _type_relique;

    public inventory_main.type_effets _type_effets_secondaire = new inventory_main.type_effets();


    // Armes : common
    public float vitesseCoup;
    public float degatsInfligesMin;
    public float degatsInfligesMax;
    public int pourcentageCritique;
    
    public float puissanceDeRecul;

    public float degatsSecondairesInfligesMin;
    public float degatsSecondairesInfligesMax;

    public float portee;


  // Armures : common
    public float montantArmure_min;
    public float montantArmure_max;
    public float armureSecondaireMin;
    public float armureSecondaireMax;

    public int quantite;

    public bool is_equiped = false;

    void Start(){

      StartCoroutine(checkIfObjectHasBeenTaken());
      // StartCoroutine(getObjectStraight());
    }

    IEnumerator checkIfObjectHasBeenTaken(){
      yield return new WaitForSeconds(0.3f);
      if(PlayerPrefs.GetInt(state_id) == 1){
          gameObject.SetActive(false);
      }
    }


    IEnumerator getObjectStraight(){
      yield return new WaitForSeconds(0.3f);
        addObject();
    }



    void OnTriggerEnter(Collider collider){ // A modifier avec la logique d'interactable
        if(collider.tag == "Player"){
            Debug.Log("add " + nom + " to inventory");
            addObject();
        }
    }

    public void addObject(){
          if(!inventory_main.instance.object_list.Any(o => o._type_equipement == _type_equipement && o._type_equipement != 0 && o.is_equiped)){
            is_equiped = true;
          }


          inventory_main.instance.add_new_object(GetComponent<inventory_object>());
          PlayerPrefs.SetInt(state_id, 1);

          gameObject.SetActive(false);
          // Destroy(gameObject);
    }







 }



