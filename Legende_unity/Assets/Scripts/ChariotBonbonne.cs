using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChariotBonbonne : MonoBehaviour
{
   
   ChariotPlayer ScriptChariotPlayer ;


    void Start()
    {
        
       ScriptChariotPlayer = GameObject.Find("player_chariot").GetComponent<ChariotPlayer>();
    
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(){
        print("BonBonne ramasee");
        Destroy(gameObject);
        ScriptChariotPlayer.equipement_Bouteille = true; 
               
    }
}
