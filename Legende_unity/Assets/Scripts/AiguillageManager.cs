using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AiguillageManager : MonoBehaviour
{

    public static AiguillageManager instance;

    Battlehub.MeshDeformer2.SplineFollow SplineFollow;
    public Battlehub.MeshDeformer2.SplineBase next_rails;

    public List<Battlehub.MeshDeformer2.SplineBase> SaveTrajetKart = new List<Battlehub.MeshDeformer2.SplineBase>();
    public int position_trajet;

    public Transform kart;

    float pos;


    void Awake(){

        if(instance == null){
            instance = this;
        }
        SplineFollow = GameObject.Find("Chariot_Container").GetComponent<Battlehub.MeshDeformer2.SplineFollow>();
        kart = GameObject.Find("kart").GetComponent<Transform>();

        SaveTrajetKart.Add(SplineFollow.Spline);   
    }


    // appelee par kart manager
    public void switchRails(){ 
        position_trajet++; 
        SplineFollow.Spline = next_rails; // on load le prochain circuit
        SplineFollow.Restart(); 
    }


    // appelee par kart manager
    public void switchRailsBack(){
        SplineFollow.Spline = SaveTrajetKart[position_trajet-1];
        position_trajet--;
        SplineFollow.Restart();
        SplineFollow.m_t = 0.9999999f;  // on replace le kart
    }
}
