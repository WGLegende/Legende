using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VapeurBar : MonoBehaviour
{

    public Slider slider;
    public Gradient gradient;
    public Image fill;

    public float JaugeVapeur;
    public const float timerBar = 10f;  //temps 

    public float AjoutVapeur;
    public bool startVapeur;
    public bool startVapeurBoost;
    public bool GainVapeur;


    void Start() {
             
        startVapeur = false;
        startVapeurBoost = false;
    }


   void Update(){

        slider.value = JaugeVapeur/timerBar;
        fill.color = gradient.Evaluate(slider.normalizedValue);
       
        // Pour test
        if (Input.GetKeyDown(KeyCode.Space)){   // on ajoute
            JaugeVapeur += AjoutVapeur;
            if(JaugeVapeur >= 0 && JaugeVapeur <= 10){
                JaugeVapeur = 10;
                ChariotPlayer.hasVapeur = true;
            }
        }

        
   }

    public void FunctionstartVapeur(int facteur){

         if(JaugeVapeur <= 10){
                JaugeVapeur -= Time.deltaTime *facteur / 2f ;  // on divise par 2f pour la duree 
            }
               if (JaugeVapeur <= 0){
            JaugeVapeur = 0;
            ChariotPlayer.hasVapeur = false;
        }

        if (JaugeVapeur > 0){ 
            ChariotPlayer.hasVapeur = true;
        }
    }
}
