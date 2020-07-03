 using System;
 using System.Linq;
 using UnityEditor;
 using UnityEngine;
 
 [CustomEditor(typeof(inventory_loot)), CanEditMultipleObjects]
 public class inventory_loot_editor : Editor {
     
     public SerializedProperty 
         loot_objects,
         loot_quantite_min,
         loot_quantite_max,
         loot_percentage;
                

     inventory_loot _inventory_loot;
     inventory_object[] objects_to_loot;
     void OnEnable () {
        _inventory_loot = (inventory_loot) target;
        
        objects_to_loot = _inventory_loot.GetComponentsInChildren<inventory_object>();
        loot_objects = serializedObject.FindProperty ("loot_objects");
        loot_percentage = serializedObject.FindProperty ("loot_percentage");
        loot_quantite_min = serializedObject.FindProperty ("loot_quantite_min");
        loot_quantite_max = serializedObject.FindProperty ("loot_quantite_max");
        
        if(objects_to_loot.Length != _inventory_loot.loot_percentage.Length){
            ReinitializeArrays();
        }
     }

     public void ReinitializeArrays(){
            int[] save_loot_percentage = _inventory_loot.loot_percentage;
            int[] save_loot_quantite_min = _inventory_loot.loot_quantite_min;
            int[] save_loot_quantite_max = _inventory_loot.loot_quantite_max;
            _inventory_loot.loot_percentage = new int[objects_to_loot.Length];
            _inventory_loot.loot_quantite_min = new int[objects_to_loot.Length];
            _inventory_loot.loot_quantite_max = new int[objects_to_loot.Length];

            for(int i = 0; i < _inventory_loot.loot_percentage.Length; i++){
                try{
                    _inventory_loot.loot_percentage[i] = save_loot_percentage[i];
                    _inventory_loot.loot_quantite_min[i] = save_loot_quantite_min[i];
                    _inventory_loot.loot_quantite_max[i] = save_loot_quantite_max[i];
                }catch(Exception e){}

            }
     }
     
    public override void OnInspectorGUI() {
        serializedObject.Update ();

        if(loot_objects.isInstantiatedPrefab ){
            if(objects_to_loot.Length == 0){
                EditorGUILayout.LabelField("Il n'y a aucun loot sur cet objet. Pour en ajouter, glisses-en un ou plusieurs en child !");
                return;
            }

            EditorGUILayout.LabelField(objects_to_loot.Length + " objets a looter :");

            for(int i = 0; i < objects_to_loot.Length; i++){
                inventory_object obj = objects_to_loot[i];
                
                obj.gameObject.transform.localPosition = new Vector3(0, 1 + (i*0.2f), 0);
                
                EditorGUILayout.Space(20);
                EditorGUILayout.LabelField("                    " + obj.nom  + " : ");

                GUI.Button(new Rect(20, 30 + (114 * i), 45, 45), new GUIContent(obj.image));
                
                if(loot_quantite_max.GetArrayElementAtIndex(i).intValue < loot_quantite_min.GetArrayElementAtIndex(i).intValue){
                    loot_quantite_max.GetArrayElementAtIndex(i).intValue = loot_quantite_min.GetArrayElementAtIndex(i).intValue;
                }

                EditorGUILayout.Space(10);
                EditorGUILayout.IntSlider ( loot_percentage.GetArrayElementAtIndex(i), 0, 100, new GUIContent("     Chance loot : " + loot_percentage.GetArrayElementAtIndex(i).intValue + "%"));
                
                if(obj._type_object == inventory_main.type_object.consommable || obj._type_object == inventory_main.type_object.ressource){
                    EditorGUILayout.IntSlider ( loot_quantite_min.GetArrayElementAtIndex(i), 1, 30, new GUIContent("     Qty min : " + loot_quantite_min.GetArrayElementAtIndex(i).intValue));
                    EditorGUILayout.IntSlider ( loot_quantite_max.GetArrayElementAtIndex(i), 1, 30, new GUIContent("     Qty max : " + loot_quantite_max.GetArrayElementAtIndex(i).intValue));
                }else{
                    EditorGUILayout.Space(40);

                }
            }


            if(_inventory_loot.GetComponentsInParent<enemy>().Length > 0){
                _inventory_loot.transform.localPosition = new Vector3(0,3,0);
            }

        }
        serializedObject.ApplyModifiedProperties ();
    }
 }