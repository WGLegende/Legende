using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AiguillageManager : MonoBehaviour
{

    public static AiguillageManager instance;

    Battlehub.MeshDeformer2.SplineFollow SplineFollow;
    public Battlehub.MeshDeformer2.SplineBase next_rails;

    public List<Battlehub.MeshDeformer2.SplineBase> List_spline_rails = new List<Battlehub.MeshDeformer2.SplineBase>();
    public int id_rails;

    
    void Awake(){

        if(instance == null){
            instance = this;
        }

        SplineFollow = GameObject.Find("Chariot_Container").GetComponent<Battlehub.MeshDeformer2.SplineFollow>();
        List_spline_rails.Add(SplineFollow.Spline); // on ajoute le rail de depart affecte au kart
    }


    // appelee par kart manager
    public void switchRails(){ 
        id_rails++; 
        SplineFollow.Spline = next_rails; // on load le prochain circuit
        SplineFollow.Restart(); 
    }


    // appelee par kart manager
    public void switchRailsBack(){
        SplineFollow.Spline = List_spline_rails[id_rails-1];
        id_rails--;
        SplineFollow.Restart();
        SplineFollow.m_t = 0.9999999f;  // on replace le kart
    }


   
    // IEnumerator moveleftRight(){

    //     float timer = 0;
    //     float duree = 1f;
    
    //     while (timer < duree){

    //         timer += Time.deltaTime;
    //         float newAlpha = Mathf.Lerp(0, 1, timer / duree);
    //         display.alpha = newAlpha;
    //         yield return null;
    //     }
    // }

        
    
}
