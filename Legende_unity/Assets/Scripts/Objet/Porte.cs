using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.UI;


public class Porte : MonoBehaviour
{

 public GameObject[] keysList;

 public enum_manager.typeOuverture _type_ouverture;

    [HideInInspector] public Animator animPorte;
    [HideInInspector] public string typeAnimation;

    public GameObject Switch; // on attache le switch sol voulu
    [HideInInspector] public switchSol SwitchScript; // variable pour recuperer la bool dnas le script switchSol
    [HideInInspector] public Inventaire UIInventaire; // variable pour recuperer les animations de l'UI
    [HideInInspector] public AudioSource soundFx;

    [HideInInspector] public bool OneShot; // on affiche qu'une fois "porte verrouilée"
    public bool AutoClosed;
    public int time_auto_closed = 3;
    [HideInInspector] public bool isOpen;
    public string nom_de_la_porte = "Porte du Boss";

    void Start(){

        UIInventaire = GameObject.Find("UI_Main").GetComponent<Inventaire>();
        OneShot = true;
        animPorte = GetComponentInChildren<Animator>(); 
        soundFx = GetComponent<AudioSource>();

        switch (_type_ouverture){

            case enum_manager.typeOuverture.classique : typeAnimation = "isOpenPivot"; break;
            case enum_manager.typeOuverture.coulissant : typeAnimation = "isOpenSlide"; break;
            case enum_manager.typeOuverture.slideUp : typeAnimation = "isOpenUp"; break;
            case enum_manager.typeOuverture.chute : typeAnimation = "isChute"; break;
        }
     
        if (Switch != null){
            SwitchScript = Switch.GetComponent<switchSol>(); // On recupere la bool dans le script
        }    
    } 

    void OnTriggerEnter(Collider collider){ 

        if(!isOpen){
            player_actions.instance.display_actions(this,collider); 
        }
        StopCoroutine("fermeture");
    }



    void OnTriggerExit(Collider collider){

        if(AutoClosed && collider.tag =="Player"){
            StartCoroutine("fermeture");
        }
        player_actions.instance.clear_action(collider.tag == "Player");      
    }

    IEnumerator fermeture(){

        yield return new WaitForSeconds(time_auto_closed);   
        animPorte.SetBool(typeAnimation, false);      
        isOpen = false;    
    }
    

}








