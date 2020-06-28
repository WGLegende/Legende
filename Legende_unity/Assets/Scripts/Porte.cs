using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.UI;


public class Porte : MonoBehaviour
{

 public GameObject[] keysList;

 string typeAnimation;
 
 public typeOuverture _type_ouverture;
    public enum typeOuverture{
        classique,
        coulissant,
        slideUp,
        chute     
    }

 Animator animPorte;
 public GameObject Switch; // on attache le switch sol voulu
 switchSol SwitchScript; // variable pour recuperer la bool dnas le script switchSol
 Inventaire UIInventaire; // variable pour recuperer les animations de l'UI

 bool OneShot; // on affiche qu'une fois "porte verrouilée"
 public bool AutoClosed;
 public int time_auto_closed = 3;
 bool isOpen;

    void Start(){

        UIInventaire = GameObject.Find("UI_Main").GetComponent<Inventaire>();
        OneShot = true;
        animPorte = GetComponentInChildren<Animator>(); 

        switch (_type_ouverture){

            case typeOuverture.classique : typeAnimation = "isOpenPivot"; break;
            case typeOuverture.coulissant : typeAnimation = "isOpenSlide"; break;
            case typeOuverture.slideUp : typeAnimation = "isOpenUp"; break;
            case typeOuverture.chute : typeAnimation = "isChute"; break;
        }
     
        if (Switch != null){
            SwitchScript = Switch.GetComponent<switchSol>(); // On recupere la bool dans le script
        }    
    } 

    void OnTriggerEnter(Collider collider){ 
        if(collider.tag == "Player" && !isOpen){
            ButtonAction.instance.Action("Ouvrir"); 
           
        }
        StopCoroutine("fermeture");
    }


    void OnTriggerStay(){

        if(hinput.anyGamepad.A.justPressed){

            if (Switch == null){
            
                if (keysList.Where(a => a != null).Count() == 0){
                    animPorte.SetBool(typeAnimation, true);
                    isOpen = true;
                    Inventaire.cleTrouve = 0;
                    UIInventaire.compteurCle();
                }
                if(keysList.Where(a => a != null).Count() > 0){

                    if (keysList.Length > 1){
                        UIInventaire.afficheInfoText("Il vous faut "+keysList.Length+" clés");
                    }
                    else{
                        UIInventaire.afficheInfoText("Il vous faut "+keysList.Length+" clé");
                    }
                }  
            }

            if (Switch != null){

                if( SwitchScript.switchSolIsPressed == false){
                    UIInventaire.afficheInfoText("Trouvez l'interrupteur !");  
                }
                if(SwitchScript.switchSolIsPressed == true){
                    animPorte.SetBool(typeAnimation, true);

                    if(OneShot){
                        UIInventaire.afficheInfoText("Vous avez déverrouillé la Porte !");
                        OneShot = false; 
                        isOpen = true;
                    }
                
                }
            }
            ButtonAction.instance.Hide(); 
        }  
    }

    void OnTriggerExit(Collider collider){

        if(collider.tag == "Player"){

            if(AutoClosed){
               StartCoroutine("fermeture");
            }
            ButtonAction.instance.Hide(); 
        }
    }

    IEnumerator fermeture(){

        yield return new WaitForSeconds(time_auto_closed);
         
        animPorte.SetBool(typeAnimation, false);      
        isOpen = false;    
    }
    

}








