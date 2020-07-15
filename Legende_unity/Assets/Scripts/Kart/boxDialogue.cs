using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class boxDialogue : MonoBehaviour
{



[Serializable]
public struct maStruct{
    
    [TextArea(3,10)]
    public string phrase;
    public int[] id_reponses;

    
    [TextArea(3,10)]
    public string[] texte_reponses;
}

public maStruct[] maStructure;



Dictionary<string, int> superTest= new Dictionary<string, int>();





[TextArea(3,10)]
public string[] mesTexts ;

public Text BoxDialogue;
public Text Button;
float vitesse_defilement = 0.03f;

int increment = 0;
bool check = true;
private string str;


    void Start(){

        Debug.Log(maStructure[0].phrase);



        for(int i = 0; i <= mesTexts.Length-1;i++){
           superTest.Add(mesTexts[i], i); 
        }
         
    }

  
    void Update(){

        if(Input.GetKeyDown("space") && !check){
            vitesse_defilement = 0.008f;
        }

        if(Input.GetKeyDown("space") && check && increment <= mesTexts.Length-1){
      
            vitesse_defilement = 0.05f;
            StartCoroutine(TypeEffect(mesTexts[increment]));
            check = false;  
            increment++;
        }
    }



    IEnumerator TypeEffect(string phrase){

        int i = 0;
        str = "";
        Button.text = "Passer";

        while( i < phrase.Length ){
            str += phrase[i++];
            BoxDialogue.text = str;
        
            if(i == phrase.Length){
                Button.text = "Suivant";
                check = true;
            }
            yield return new WaitForSeconds(vitesse_defilement);
        }
    }


}
