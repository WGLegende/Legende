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
    int save_last_rail_ui = 1;
    CanvasGroup display;
    Scrollbar scroll_up_down;
    Scrollbar scroll_right_left;
    public Battlehub.MeshDeformer2.SplineBase[] spline_rails;
    
    void Start(){

        if(instance == null){
            instance = this;
        }

        SplineFollow = GameObject.Find("Chariot_Container").GetComponent<Battlehub.MeshDeformer2.SplineFollow>();
        List_spline_rails.Add(SplineFollow.Spline);  


        foreach(GameObject cg in rail_map_ui){ // on cache toute la map ui
            CanvasGroup display = cg.GetComponent<CanvasGroup>();
            display.alpha = 0;
        }

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

        int i = 0;
      
        while(true){

            for (i = 0; i < spline_rails.Length; i++){

                if (spline_rails[i] == SplineFollow.Spline){

                    if(save_last_rail_ui != i){
                        rail_map_ui[save_last_rail_ui].transform.GetChild(2).gameObject.SetActive(false);
                        save_last_rail_ui = i;
                    }

                    rail_map_ui[i].transform.GetChild(2).gameObject.SetActive(true);
                    display = rail_map_ui[i].GetComponent<CanvasGroup>();
                   //scroll_up_down = rail_map_ui[i].GetComponentInChildren<Scrollbar>();
                   // scroll_right_left = scroll_up_down.GetComponentInChildren<Scrollbar>();
                    
                    if(display.alpha == 0){
                        StartCoroutine(fadein());
                    }


                   // scroll_up_down.value = Mathf.Round(SplineFollow.T* 100f)/100f;
                    rail_map_ui[i].GetComponentInChildren<Slider>().value = Mathf.Round(SplineFollow.T* 100f)/100f;
                }
            }

            yield return new WaitForSeconds(0.1f);
        }
    }


    IEnumerator fadein(){

        float timer = 0;
        float duree = 1f;
    
        while (timer < duree){

            timer += Time.deltaTime;
            float newAlpha = Mathf.Lerp(0, 1, timer / duree);
            display.alpha = newAlpha;
            yield return null;
        }
    }

    IEnumerator moveleftRight(){

        float timer = 0;
        float duree = 1f;
    
        while (timer < duree){

            timer += Time.deltaTime;
            float newAlpha = Mathf.Lerp(0, 1, timer / duree);
            display.alpha = newAlpha;
            yield return null;
        }
    }

        
    
}
