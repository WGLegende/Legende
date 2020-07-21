using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class MainInteractable : MonoBehaviour
{
    public static MainInteractable instance;

    Animator animPlayer;
    Animator animButton;

    public List<Interactable> mesInteractables = new List<Interactable>();

    bool onInteraction = false;

    public Dictionary<string, string> TextActionButton = new Dictionary<string, string>(){
        {"chariot", "Entrer"},
        {"bouton", "Activer"},
        {"coffre", "Ouvrir"},
        {"porte", "Ouvrir"},
        {"consommable", "Prendre"},
        {"cuisine", "Manger"}
    };

    // public Dictionary<string, Animator> mesAutresIntteraclable = new Dictionary<string, Animator>(){
    //     {"monInterSuper", new Animator()}
    // };

    void Start()
    {
        //mesAutresIntteraclable["monInterSuper"];

        instance = this;
        animPlayer = GameObject.Find("Player").GetComponent<Animator>();
        animButton = GameObject.Find("ButtonAction").GetComponent<Animator>();
    }

  


    void Update()
    {
        if (Hinput.anyGamepad.A.justPressed && !mesInteractables.Any(i => i.isInteracting)){
            animPlayer.SetTrigger("attack01");
        }

        if (Hinput.anyGamepad.A.justPressed && mesInteractables.Any(i => i.isInteracting)){
            Action();
        }

     

    }

    public void NewInterraction(Interactable Obj){
        
        if (!mesInteractables.Contains(Obj)){
            Obj.isInteracting = true;
            mesInteractables.Add(Obj);
            DoInterraction(Obj);
        }
      
      
      //  afficheButton();
    }

    public void PrepareInteraction(){
        if(mesInteractables.Count > 0 && !mesInteractables.Any(i => i.isInteracting)){ // si un seul des éléments de ma liste mesInteractables a son bool isInterracting a true
            foreach (Interactable obj in mesInteractables){
                obj.isInteracting = true;
                DoInterraction(obj);
                print(obj);
            }
        }
    }


    public void DoInterraction(Interactable Obj){
          
        GameObject.Find("ButtonTextAction").GetComponent<Text>().text = TextActionButton[Obj.Objet.ToString()];
        animButton.SetBool("afficheButtonAction",true);

        mesInteractables.Remove(Obj);
        PrepareInteraction();
    }

    

    public void afficheButton(){
       
       
    }



    public void ExitInteractable(){

        animButton.SetBool("afficheButtonAction",false);
    }



    public void Action(){
      
        animButton.SetBool("afficheButtonAction",false);
    }



}
