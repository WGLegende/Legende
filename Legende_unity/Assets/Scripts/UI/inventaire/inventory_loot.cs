using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class inventory_loot : MonoBehaviour
{
    public inventory_object[] loot_objects;
    public int[] loot_percentage = new int[100];
    public int[] loot_quantite_min = new int[100];
    public int[] loot_quantite_max = new int[100];

    void Awake(){
        loot_objects = GetComponentsInChildren<inventory_object>();
        foreach (inventory_object obj in GetComponentsInChildren<inventory_object>())
        {
            obj.gameObject.SetActive(false);   
        }

        // Remove 3D if loot is on an enemy
        if(GetComponentsInParent<enemy>().Length > 0){
            Destroy (GetComponent<MeshRenderer>());
            Destroy (GetComponent<MeshFilter>());
        }
    }


    void OnTriggerEnter(Collider collider){
        player_actions.instance.display_actions(this,collider); 
    }

    void OnTriggerExit(Collider collider){  
        player_actions.instance.clear_action(collider.tag == "Player");  
    }



}
