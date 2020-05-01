using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class inventory_shortcuts : MonoBehaviour
{
    public static inventory_shortcuts instance;

    public enum shortcut_direction {up, right,down,left};

    public GameObject[] shortcut_slot = new GameObject[4];
    public Image[] shortcut_image = new Image[4];
    public Text[] shortcut_quantity = new Text[4];

    public inventory_object[] shortcut_slot_object = new inventory_object[4];

    void Start()
    {
        if(instance == null){
            instance = this;
        }
    }




    public void create_shortcut(int direction, inventory_object obj){
        if(obj == null){
            Debug.Log("objet null");
            return;
        }
        
        // check if a shortcut already exist and delete it, or permut it
        for(int i = 0; i < shortcut_slot_object.Length; i++){
            if(shortcut_slot_object[i] == obj){
                removeShortcut(i);
            }
        }

        shortcut_slot_object[direction] = obj;
        shortcut_slot[direction].SetActive(true);
        shortcut_image[direction].sprite =  Sprite.Create(obj.image, new Rect(0.0f, 0.0f, obj.image.width, obj.image.height), new Vector2(0.5f, 0.5f), 100.0f);
        shortcut_quantity[direction].gameObject.SetActive(obj.quantite > 1);
        shortcut_quantity[direction].text = obj.quantite.ToString();

        check_if_any(direction);
    }


    public void removeShortcut(int direction){
        shortcut_slot_object[direction] = null;
        shortcut_image[direction].sprite =  null;
        shortcut_slot[direction].SetActive(false);
    }


    public void use_shortcut(int direction){
        if(shortcut_slot_object[direction] == null || !check_if_any(direction)){
            return;
        }
        Debug.Log("use_shortcut " + direction);

        if(shortcut_slot_object[direction]._type_object == inventory_main.type_object.consommable_player || shortcut_slot_object[direction]._type_object == inventory_main.type_object.consommable_ressources){
            player_utilisables.instance.utilise_un_objet(shortcut_slot_object[direction]);
        }else{
            player_equipement.instance.equipe_un_objet(shortcut_slot_object[direction]);
        }

        shortcut_quantity[direction].text = shortcut_slot_object[direction].quantite.ToString();
        check_if_any(direction);
    }

    public bool check_if_any(int direction){
        bool is_any = shortcut_slot_object[direction].quantite > 0;
        shortcut_image[direction].color = is_any ? Color.white : new Color(0.5f, 0.5f, 0.5f, 0.5f);
        return is_any;
    }



}
