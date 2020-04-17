using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;


public class enemy : MonoBehaviour
 
{
 public static enemy instance;
 public Slider slider;
 public int currentPv;
 public int maxPv;
 public Text TX_Pv;
 public Text TX_PvMax;


    void Start(){

        instance = this;
    }
  
    void Update(){} 



    public void EnemyCharacteristic(int valueMax){

        maxPv = valueMax;
        currentPv  = maxPv;
        slider.value = currentPv;
        TX_Pv.text = currentPv.ToString();
        TX_PvMax.text = maxPv.ToString();
        slider.maxValue = maxPv;
        print("new");
    }


}
  