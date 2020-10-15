using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class _PLife : MonoBehaviour
{
    int id;
    Color[] color_type = new Color[]{
        new Color(0,0,0,0),                 // empty
        new Color(1,0,0,1),                 // life
        new Color(0.418f,0.376f,0.362f,1),   // armor
        new Color(1,0.596f,0,1),            // hot_armor
        new Color(0.071f,0.78f,1,1),        // cold_armor
        new Color(0.411f,0,1,1),            // ondes_armor
    };

    enum_manager.type_effets _type_effets;

    public GameObject full;

    public void initialize_life(int id, enum_manager.type_effets type, bool isActive){
        _type_effets = type;
        full.SetActive(isActive);
        full.GetComponent<Image>().color = color_type[(int)type];
    }
}
