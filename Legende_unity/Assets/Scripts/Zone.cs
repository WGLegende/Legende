using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Zone : MonoBehaviour

{
     public GameObject PanelZoneText;
     public string nom_du_lieu;


    void OnTriggerEnter(Collider collider){

        if (collider.gameObject.tag==("Player")){
        PanelZoneText.SetActive (true); 


        GameObject.Find("zoneText").GetComponent<Text>().text = nom_du_lieu;

            print("collision");
         StartCoroutine(MyCoroutine());
        }


    }

    void OnTriggerExit(){

    }


    IEnumerator MyCoroutine(){



        yield return new WaitForSeconds(7);
        PanelZoneText.SetActive (false); 

    }
 
    
    







}
