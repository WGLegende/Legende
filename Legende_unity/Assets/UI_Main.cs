using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Main : MonoBehaviour
{
    public static UI_Main instance;

    public RectTransform Player_Life_Container;
    public RectTransform Player_Armor_Container;

    void Awake(){
        if(instance == null){
            instance = this;
        }
    }
}
