using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class _Slot : MonoBehaviour
{

    // public inventory_slots_container children_slots_navigation;
    public Image[] IMG_slot;
    public inventory_object object_in_slot;


    void Awake(){
        IMG_slot = GetComponentsInChildren<Image>();
        //0 empty
        //1 extra
        //2 full
        //3 quantity
        //4 hovered
        //5 image objet
    }


    public void set_slot(inventory_object obj)
    {
        IMG_slot[4].gameObject.SetActive(false);
        object_in_slot = obj;

        if(obj != null){
            IMG_slot[0].gameObject.SetActive(false);
            IMG_slot[1].gameObject.SetActive(false);
            IMG_slot[2].gameObject.SetActive(true);
            IMG_slot[5].gameObject.SetActive(true);

            if(obj.quantite > 1){
                IMG_slot[3].gameObject.SetActive(true);
                IMG_slot[3].GetComponentInChildren<Text>().text = "x" + obj.quantite;
            }else{
                IMG_slot[3].gameObject.SetActive(false);
            }

            IMG_slot[5].sprite =  Sprite.Create(obj.image, new Rect(0.0f, 0.0f, obj.image.width, obj.image.height), new Vector2(0.5f, 0.5f), 100.0f);
        }else{

            IMG_slot[0].gameObject.SetActive(true);
            IMG_slot[1].gameObject.SetActive(true);
            IMG_slot[2].gameObject.SetActive(false);
            IMG_slot[3].gameObject.SetActive(false);
            IMG_slot[5].gameObject.SetActive(false);
        }
    }

    public void hover_slot(bool toHover){

        IMG_slot[4].gameObject.SetActive(toHover);
    }

}
