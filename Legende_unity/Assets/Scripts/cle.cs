using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cle : MonoBehaviour
{
  
    public string type;
    Inventaire Varinventaire;

  void Start(){

       Varinventaire = GameObject.Find("UI_Main").GetComponent<Inventaire>();

    }


    void OnTriggerEnter(){

        Destroy(gameObject);
        Inventaire.cleTrouve += 1; // var cleTrouve dans le script Inventaire
        Inventaire.instance.compteurCle(); // Affichage UI
        Inventaire.instance.afficheInfoText(type);          
    }

    
}
