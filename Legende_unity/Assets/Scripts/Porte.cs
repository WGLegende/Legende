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

 bool OneShot; // on affiche qu'une fois "porte verrouilée"
 public bool AutoClosed;

    void Start(){

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
                Inventaire.instance.cleTrouve = 0;
                Inventaire.instance.compteurCle();
            }

            if(keysList.Where(a => a != null).Count() > 0){

                if (keysList.Length > 1){
                    Inventaire.instance.afficheInfoText("Il vous faut "+keysList.Length+" clés");
                }
                 else{
                    Inventaire.instance.afficheInfoText("Il vous faut "+keysList.Length+" clé");
                }

            }  
        }

        if (Switch != null){

            if( SwitchScript.switchSolIsPressed == false){
                Inventaire.instance.afficheInfoText("Trouvez l'interrupteur !");
               
            }

            if(SwitchScript.switchSolIsPressed == true){
                animPorte.SetBool(typeAnimation, true);

                if(OneShot){
                Inventaire.instance.afficheInfoText("Vous avez déverrouillé la Porte !");
                OneShot = false;  
                }
              
            }
        }

        
    }

    void OnTriggerExit(){

        if(AutoClosed){
            animPorte.SetBool(typeAnimation, false);          
        }
    }
    

}








