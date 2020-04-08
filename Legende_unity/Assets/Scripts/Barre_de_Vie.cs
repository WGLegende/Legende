using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Barre_de_Vie : MonoBehaviour
{

 public Slider slider;
 public int player_currentPv;
 public int player_maxPv = 100;
 public Text TX_PvText;
 

	void Start() { 

        player_currentPv  = player_maxPv;
        slider.value = player_currentPv;
    
    }


	void Update () {  // POUR TEST

        if (Input.GetKeyDown(KeyCode.Space)){ 
           PvPlayer(1);
        }

        if (Input.GetKeyDown(KeyCode.K)){
           PvPlayer(-1);
        }   
    }

    public void PvPlayer(int value){

        player_currentPv += value;
        player_currentPv = Mathf.Clamp( player_currentPv, 0, 100);
        slider.value = player_currentPv;

        TX_PvText.text = player_currentPv.ToString();
        TX_PvText.color = player_currentPv < 20 ? TX_PvText.color = Color.red : TX_PvText.color = Color.black;

        if (player_currentPv == 0){
            GameObject.Find ("TextInfo").GetComponent<Text>().text = "Vous etes une quiche";
            GameObject.Find ("PanelInfo").GetComponent<Animator>().SetTrigger("panelInfo");
        }

    }


}
