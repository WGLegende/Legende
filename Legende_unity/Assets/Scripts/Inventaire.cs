using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventaire : MonoBehaviour
{
    public static Inventaire instance;

    public int cleTrouve;
    public string InfoText;

    void Start()
    {
        if(instance == null){
            instance = this;
        }

      cleTrouve = 0;
      InfoText = "";

        GameObject.Find("TextInfo").GetComponent<Text>().text = "SUPER TEST";


    }

    public void compteurCle(){

        if(cleTrouve > 0){
            // GameObject.Find("nbrKey").GetComponent<Text>().text = cleTrouve.ToString(); // maj du text
            GameObject.Find ("PanelKey").GetComponent<Animator>().SetBool("panelKeyIsOpen", true); // affichage ui clé
            GameObject.Find ("PanelInfo").GetComponent<Animator>().SetTrigger("panelInfo");
        }
        else{
           GameObject.Find ("PanelKey").GetComponent<Animator>().SetBool("panelKeyIsOpen", false); // affichage ui clé
        }
        
    }

    public void afficheInfoText(string Montext){
        GameObject.Find("TextInfo").GetComponent<Text>().text = Montext;
        GameObject.Find("PanelInfo").GetComponent<Animator>().SetTrigger("panelInfo");
        
    }

}