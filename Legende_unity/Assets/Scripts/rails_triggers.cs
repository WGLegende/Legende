using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rails_triggers : MonoBehaviour
{

    public enum type_collision{

        stop,
        chute
    };

    public type_collision _type_collision;
    public Battlehub.MeshDeformer2.SplineFollow SplineFollow;
    

    
    public void touching_chariot(ChariotPlayer chariot){

        if(_type_collision == type_collision.stop){

           print("COllisionRailStop");
        }
          if(_type_collision == type_collision.chute){

           print("Oooouche");
        }
    }
 


}
