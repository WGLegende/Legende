using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AiguillageManager : MonoBehaviour
{

    public static AiguillageManager instance;

    Battlehub.MeshDeformer2.SplineFollow SplineFollow;
    public Battlehub.MeshDeformer2.SplineBase next_rails;

    public bool next_rails_isInverse;
    public Transform kart;

    public List<Battlehub.MeshDeformer2.SplineBase> List_spline_rails = new List<Battlehub.MeshDeformer2.SplineBase>();
    public int id_rails;

    
    void Awake(){

        if(instance == null){
            instance = this;
        }

        SplineFollow = GameObject.Find("Chariot_Container").GetComponent<Battlehub.MeshDeformer2.SplineFollow>();
        List_spline_rails.Add(SplineFollow.Spline); // on ajoute le rail de depart affecte au kart
    }

    void Update(){
        //Gestion aiguillage
        if(SplineFollow.T == 1 && kart_manager.instance.vitesse_actuelle > 0 ){// fin circuit;
            switchRails();
          
        }
        if(SplineFollow.T <= 0.001 && kart_manager.instance.vitesse_actuelle < 0 && AiguillageManager.instance.id_rails > 0){ // fin back circuit
            switchRailsBack();
        }
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
        SplineFollow.m_t = 0.9999f;  // on replace le kart
    }


    // // appelee par kart manager
    // public void switchRails(){ 
    //     id_rails++; 
    //     SplineFollow.Spline = next_rails; // on load le prochain circuit
    //     SplineFollow.Restart(); 
    // }


    // // appelee par kart manager
    // public void switchRailsBack(){
    //     SplineFollow.Spline = List_spline_rails[id_rails-1];
    //     id_rails--;
    //     SplineFollow.Restart();
    //     SplineFollow.m_t = 0.9999f;  // on replace le kart
    // }


   





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




// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;
// using UnityEngine.UI;

// public class AiguillageManager : MonoBehaviour
// {

//     public static AiguillageManager instance;

//     Battlehub.MeshDeformer2.SplineFollow SplineFollow;

//     public Battlehub.MeshDeformer2.SplineBase next_rails;
//     public bool next_rails_isInverse;


//     public List<Battlehub.MeshDeformer2.SplineBase> List_spline_rails = new List<Battlehub.MeshDeformer2.SplineBase>();
//     public int id_rails;

    
//     void Awake(){

//         if(instance == null){
//             instance = this;
//         }

//         SplineFollow = GameObject.Find("Chariot_Container").GetComponent<Battlehub.MeshDeformer2.SplineFollow>();
//         List_spline_rails.Add(SplineFollow.Spline); // on ajoute le rail de depart affecte au kart
//     }

//     void Update()
//     {
//        // print(SplineFollow.T);

//          if(SplineFollow.T > 0.999  && kart_manager.instance.vitesse_actuelle > 0){// fin circuit;
//             switchRails();
//             print("chg rail next");
           
    
//         }
//         if(SplineFollow.T <= 0.001f && kart_manager.instance.vitesse_actuelle > 0 && id_rails > 0){ // fin back circuit
//             switchRailsBack();
//             print("chg rail back");
            
//         }
//     }


//     // appelee par kart manager
//     public void switchRails(){ 
//         id_rails++; 
//         SplineFollow.Spline = next_rails; // on load le prochain circuit
//         SplineFollow.Restart();

//         if(next_rails_isInverse){
//             kart_manager.instance.reverse_pad = -1f;
//             SplineFollow.m_t = 0.9999f;  // on replace le kart

//         }
//         else{
           
//             kart_manager.instance.reverse_pad = 1f;
//             SplineFollow.m_t = 0.001f;
//         }

//     }


//     // appelee par kart manager
//     public void switchRailsBack(){
//         SplineFollow.Spline = List_spline_rails[id_rails-1];
//         id_rails--;
//         SplineFollow.Restart();

//         if(!next_rails_isInverse){
//             SplineFollow.m_t = 0.999f;  // on replace le kart
//             kart_manager.instance.reverse_pad = 1f;
//         }
//         else{
//             SplineFollow.m_t = 0.001f;
//             kart_manager.instance.reverse_pad = -1f;
//         }
//     }
    


   
//     // IEnumerator moveleftRight(){

//     //     float timer = 0;
//     //     float duree = 1f;
    
//     //     while (timer < duree){

//     //         timer += Time.deltaTime;
//     //         float newAlpha = Mathf.Lerp(0, 1, timer / duree);
//     //         display.alpha = newAlpha;
//     //         yield return null;
//     //     }
//     // }

        
    
// }

