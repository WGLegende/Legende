using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.Reflection;
using System.ComponentModel.Design;

public class inventory_objects_manager : MonoBehaviour
{

    List<string> RAW_ObjectList = new List<string>();



    void Awake()
    {
        loadPreviewsInventoryValues();

        foreach( FieldInfo variable in new inventory_object().GetType().GetFields())
        {
            Debug.Log("Variable :  "+ variable.Name);
        }
    }

    void Update(){
        if(Input.GetKeyDown(KeyCode.S)){
            Debug.Log("simulate quit game");
            OnQuitGame();
        }
    }



    void OnQuitGame(){
        setRawInventoryValues();
    }
    
    void loadPreviewsInventoryValues(){




    }


    
    void setRawInventoryValues(){
        string rawInventoryValue = "";


        Component[] cs = (Component[]) GameObject.FindObjectOfType<inventory_object>().GetComponents (typeof(Component));
        foreach (Component c in cs)
        {
            foreach( FieldInfo fi in c.GetType().GetFields() )
            {
                System.Object obj = (System.Object)c;
                Debug.Log("Variable :  "+fi.Name + " val "+fi.GetValue(obj));
            }
        }
    }


}
