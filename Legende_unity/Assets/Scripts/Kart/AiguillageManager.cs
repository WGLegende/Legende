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

    public GameObject[] rail_map_ui;
    public Battlehub.MeshDeformer2.SplineBase[] spline_rails;
    
    void Start(){

        if(instance == null){
            instance = this;
        }

        SplineFollow = GameObject.Find("Chariot_Container").GetComponent<Battlehub.MeshDeformer2.SplineFollow>();
        List_spline_rails.Add(SplineFollow.Spline);  

        StartCoroutine(refresk_ui_position()); 
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


    IEnumerator refresk_ui_position(){

        int i;

        while(true){

            for (i = 0; i < spline_rails.Length; i++){

                if (spline_rails[i] == SplineFollow.Spline){

                    rail_map_ui[i].SetActive(true);
                    rail_map_ui[i].GetComponentInChildren<Slider>().value =  Mathf.Round(SplineFollow.T* 100f)/100f;
                }
            }

            yield return new WaitForSeconds(0.1f);
        }
    }

    
}
