using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

using System.Reflection;
using System.ComponentModel.Design;

public class inventory_objects_manager : MonoBehaviour
{
    public static inventory_objects_manager instance;
    List<string> RAW_ObjectList = new List<string>();

    public List<inventory_object> object_list = new List<inventory_object>();

    public GameObject Object_List_Container;


    void Awake()
    {
        if(instance == null){
            instance = this;
        }
        StartCoroutine(preloadPreviewsInventoryValues());

        // foreach( FieldInfo variable in new inventory_object().GetType().GetFields())
        // {
        //     System.Object obj = (System.Object)new inventory_object();
        //     Debug.Log(variable.Name + " - value: " + variable.GetValue(obj));
        // }
    }

    void Update(){
        if(Input.GetKeyDown(KeyCode.S)){
            Debug.Log("simulate quit game");
            OnQuitGame();
        }
    }



    void OnQuitGame(){
        setInventoryValuesInPlayerPref();
    }
    

    
    public void add_new_object(inventory_object new_obj){
        inventory_object existing_object = object_list.LastOrDefault(o => o.nom == new_obj.nom);

        if(existing_object != null){
            if(new_obj.quantite + existing_object.quantite > new_obj.max_stack){
                int new_stack_quantity = new_obj.quantite + existing_object.quantite-new_obj.max_stack;
                existing_object.quantite = existing_object.max_stack;
                new_obj.quantite = new_stack_quantity;

                inventory_object new_object = Object_List_Container.AddComponent(typeof(inventory_object)) as inventory_object;
                new_object = new_obj;

                object_list.Add(new_obj);
            }else{
                existing_object.quantite += new_obj.quantite;
            }
        }else{
            object_list.Add(new_obj);
        }

        setInventoryValuesInPlayerPref();
        // loadPreviewsInventoryValues();
        new_obj.gameObject.SetActive(false);
          // Destroy(new_obj.gameObject);

    }

    IEnumerator preloadPreviewsInventoryValues(){
        yield return new WaitForSeconds(0.3f);
        loadPreviewsInventoryValues();
    }

    public void loadPreviewsInventoryValues(){

        string rawInventoryValue = PlayerPrefs.GetString("inventory");

        foreach(string obj in rawInventoryValue.Split("&&"[0])){
            if(string.IsNullOrEmpty(obj)){
                continue;
            }
            inventory_object new_object = Object_List_Container.AddComponent(typeof(inventory_object)) as inventory_object;

            foreach(string v in obj.Split("|"[0]))
            {
                if(string.IsNullOrEmpty(v)){
                    continue;
                }
                string var_name = v.Split("#"[0])[0];
                string var_value = v.Split("#"[0])[1];

                foreach( FieldInfo var in new_object.GetType().GetFields())
                {
                    Type var_Type = var.FieldType;
                    if(var.Name == var_name){
                        // Debug.Log("Fill variable " + var_name + " with value " + var_value + " ---- type is " + var_Type);
                        if(var_Type.ToString() == "UnityEngine.Texture2D"){
                            Texture2D texture = (Texture2D)AssetDatabase.LoadAssetAtPath(var_value, typeof(Texture2D));
                            var.SetValue((System.Object)new_object, (Texture2D)texture);
                        }else{
                            if(var_Type.ToString().Contains("+")){ // when enum
                                var.SetValue((System.Object)new_object, int.Parse(var_value));
                            }else{
                                var.SetValue((System.Object)new_object, Convert.ChangeType(var_value, var_Type));
                            }
                        }
                    }
                }
            }

            inventory_objects_manager.instance.object_list.Add(new_object);

            foreach (inventory_object o in GameObject.FindObjectsOfType<inventory_object>())
            {
                if(o.gameObject != Object_List_Container && new_object.state_id == o.state_id){
                    Destroy(o.gameObject);
                }
            }
        }
    }

    public void setInventoryValuesInPlayerPref(){
        string rawInventoryValue = "";
        foreach(inventory_object obj in inventory_objects_manager.instance.object_list){
            foreach( FieldInfo variable in obj.GetType().GetFields())
            {
                string var_value = variable.GetValue((System.Object)obj).ToString();
                if(var_value.Contains("UnityEngine.Texture2D")){
                    var_value = AssetDatabase.GetAssetPath(obj.image);
                }

                if(variable.FieldType.ToString().Contains("+")){
                    var_value = ((int)variable.GetValue((System.Object)obj)).ToString();
                }
                rawInventoryValue += variable.Name + "#" + var_value + "|";
            }
            rawInventoryValue += "&&";
        }
        PlayerPrefs.SetString("inventory", rawInventoryValue);
    }


}
