using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Barre_de_Vie : MonoBehaviour
{
 public static Barre_de_Vie instance;

 public Slider slider;
 public Text TX_PvText;
 

	void Start(){ 

        instance = this;  
        RefreshPvPlayerUI(player_main.instance.player_current_pv);   
    }


    public void RefreshPvPlayerUI(float val){

        slider.value = val;
        TX_PvText.text = val.ToString("f0");
        //TX_PvText.color = val < 20 ? TX_PvText.color = Color.red : TX_PvText.color = Color.black;
    }


}
