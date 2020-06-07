using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class inventory_main_structure : MonoBehaviour
{
    public static inventory_main_structure instance;
    public GameObject[] inventory_panel_list;
    int selected_panel_id = 0;

    bool inventory_is_open = false;

    void Awake(){
        if(instance == null){
            instance = this;
        }
        StartCoroutine(initialize_inventory());
    }

    public IEnumerator initialize_inventory(){
        yield return new WaitForSeconds(0.2f);
        
        foreach(GameObject panel in inventory_panel_list){
            panel.SetActive(false);
        }
        close_inventory();
    }



    public void open_inventory_panel(){
        GameObject opened_panel = inventory_panel_list[selected_panel_id];

        opened_panel.SetActive(true);

        foreach(inventory_slot_master slot_master in opened_panel.GetComponentsInChildren<inventory_slot_master>().Where(s => s.slot_master_of_this == null)){
            Debug.Log("Initialize slot_master");
            slot_master.Initialize();
        }
    }
    
    public void close_inventory_panel(){

        GameObject opened_panel = inventory_panel_list[selected_panel_id];

        opened_panel.SetActive(false);

        foreach(inventory_slot_master slot_master in opened_panel.GetComponentsInChildren<inventory_slot_master>()){
            slot_master.Close();
        }
    }

    public void close_inventory(){
        inventory_is_open = false;
        GetComponent<RectTransform>().anchoredPosition = new Vector2(-40000f,-40000f);
    }
    public void open_inventory(){
        inventory_is_open = true;
        GetComponent<RectTransform>().anchoredPosition = new Vector2(0f,0f);
    }

    public void navigate_inventory_panel(int direction){
        if(direction == 0){
            if(!inventory_is_open){
                open_inventory();
            }else{
                close_inventory();
            }
            GamePad_manager.instance.open_close_inventory(inventory_is_open);
        }

        if(gameObject.activeSelf){
            close_inventory_panel();
            selected_panel_id = Mathf.Clamp(selected_panel_id + direction, 0, inventory_panel_list.Length-1);
            open_inventory_panel();
        }

    }

}
