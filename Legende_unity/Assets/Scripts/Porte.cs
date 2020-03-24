using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Porte : MonoBehaviour
{

 public GameObject[] keysList;

 string typeAnimation;
 public bool OpenPivot;
 public bool OpenUp;
 public bool OpenSlide;
 public bool OpenChute;

 Animator animPorte;
 public GameObject Switch; // on attache le switch sol voulu
 switchSol SwitchScript; // variable pour recuperer la bool dnas le script switchSol
 Inventaire UIInventaire; // variable pour recuperer les animations de l'UI

 bool OneShot; // on affiche qu'une fois "porte verrouilée"
 public bool AutoClosed;

    void Start(){

         UIInventaire = GameObject.Find("Inventaire").GetComponent<Inventaire>();
        OneShot = true;
        animPorte = GetComponentInChildren<Animator>(); 
       
        if (OpenPivot){typeAnimation = "isOpenPivot";};
        if (OpenUp){typeAnimation = "isOpenUp";};
        if (OpenSlide){typeAnimation = "isOpenSlide";};
        if (OpenChute){typeAnimation = "isChute";};

        if (Switch != null){
            SwitchScript = Switch.GetComponent<switchSol>(); // On recupere la bool dans le script
        }    
    } 
   void OnTriggerEnter(){

        if (Switch == null){
           
            if (keysList.Where(a => a != null).Count() == 0){
                animPorte.SetBool(typeAnimation, true);
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

            if(SwitchScript.switchSolIsPressed == true && OneShot == true){
                UIInventaire.afficheInfoText("Vous avez déverrouillé la Porte !");  
               
                animPorte.SetBool(typeAnimation, true);
                OneShot = false;
            }
        }

        
    }

    void OnTriggerExit(){
        
        if(AutoClosed){
            if (Switch == null){
                if (keysList.Where(a => a != null).Count() == 0){
                    animPorte.SetBool(typeAnimation, false);
                }
            }
        }
    }
    

}








