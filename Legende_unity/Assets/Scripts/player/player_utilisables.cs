using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player_utilisables : MonoBehaviour
{
   
    public static player_utilisables instance;


    void Start()
    {
        if(instance == null){
            instance = this;
        }


    }

    public void utilise_un_objet(inventory_object obj){
        Debug.Log("j'utilise " + obj.nom);


              /// potion 

    }
}
