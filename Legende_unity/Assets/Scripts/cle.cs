using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cle : MonoBehaviour
{
  
    public string type;
    Inventaire UIInventaire;

  void Start(){

       UIInventaire = GameObject.Find("Inventaire").GetComponent<Inventaire>();

    }


    void OnTriggerEnter(){

        Destroy(gameObject);
        Inventaire.cleTrouve += 1;
        UIInventaire.compteurCle(); // Affichage UI
        UIInventaire.afficheInfoText(type);
               
    }

    
}
