using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class cle : MonoBehaviour
{
    public string type;

    void OnTriggerEnter(){
        Inventaire.instance.cleTrouve += 1; // var cleTrouve dans le script Inventaire
        Inventaire.instance.compteurCle(); // Affichage UI
        Inventaire.instance.afficheInfoText(type); 

        Destroy(this.gameObject);
    }
}
