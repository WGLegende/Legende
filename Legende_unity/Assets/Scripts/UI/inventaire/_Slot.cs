using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class _Slot : MonoBehaviour
{

    public inventory_slots_container children_slots_navigation;
    Image IMG_slot;

    void Awake(){
        IMG_slot = GetComponentsInChildren<Image>()[1];
    }


    public void set_slot(inventory_object obj )
    {
        if(obj != null){
            gameObject.name = "slot_" + obj.nom;
            IMG_slot.sprite =  Sprite.Create(obj.image, new Rect(0.0f, 0.0f, obj.image.width, obj.image.height), new Vector2(0.5f, 0.5f), 100.0f);
            IMG_slot.color = Color.white;
        }else{
            gameObject.name = "slot_vide";
            IMG_slot.color = new Color(0f,0f,0f,0f);
        }
    }

}
