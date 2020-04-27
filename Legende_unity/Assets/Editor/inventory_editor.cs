 using System;
 using UnityEditor;
 using UnityEngine;
 
 [CustomEditor(typeof(inventory_main)), CanEditMultipleObjects]
 public class inventory_editor : Editor {
     
     public SerializedProperty 
         TR_Inventaire, 
         TR_Inventaire_equipement, 
         TR_Inventaire_consommables, 
         TR_Inventaire_relique,
         type_object
         ;
     
// SerializedProperty _Base_sous_objects;
//      public void OnEnable()
//      {
//          _Base_sous_objects = this.serializedObject.FindProperty("_Base_sous_objects");
//      }
//      public override void OnInspectorGUI(){
//          serializedObject.Update ();
//          for (int x = 0; x < _Base_sous_objects.arraySize; x++) {
//              SerializedProperty property = _Base_sous_objects.GetArrayElementAtIndex (x); // get array element at x
//              property.floatValue = Mathf.Max (0,property.floatValue); // Edit this element's value, in this case limit the float's value to a positive value.
//          }
//          EditorGUILayout.PropertyField(_Base_sous_objects,true); // draw property with it's children
//          serializedObject.ApplyModifiedProperties ();
//      }

     void OnEnable () {
         // Setup the SerializedProperties
        TR_Inventaire = serializedObject.FindProperty ("TR_Inventaire");
        TR_Inventaire_equipement = serializedObject.FindProperty ("TR_Inventaire_equipement");
        TR_Inventaire_consommables = serializedObject.FindProperty ("TR_Inventaire_consommables");
        TR_Inventaire_relique = serializedObject.FindProperty ("TR_Inventaire_relique");
        type_object = serializedObject.FindProperty ("type_object");

     }
     
    public override void OnInspectorGUI() {
        serializedObject.Update ();
        EditorGUILayout.PropertyField(TR_Inventaire, new GUIContent("Inventaire Container :"));
        EditorGUILayout.PropertyField(TR_Inventaire_equipement, new GUIContent("Equipement Container :"));
        EditorGUILayout.PropertyField(TR_Inventaire_consommables, new GUIContent("Consommables Container :"));
        EditorGUILayout.PropertyField(TR_Inventaire_relique, new GUIContent("Reliques Container :"));
        

        // for (int x = 1; x < _Base_sous_objects.arraySize; x++) {
        //     SerializedProperty property = _Base_sous_objects.GetArrayElementAtIndex (x); // get array element at x
        //     EditorGUILayout.PropertyField(property, new GUIContent("Conteneur " + ((inventory_main.type_object)x) + " :")); // draw property with it's children
        // }

        // EditorGUILayout.PropertyField(_Base_sous_objects,true); // draw property with it's children

            // foreach(inventory_main.type_object type_object in Enum.GetValues(typeof(inventory_main.type_object)))
            // {
            //     EditorGUILayout.PropertyField(TR_Inventaire_relique, new GUIContent(type_object.ToString()));
            // }

            // foreach(inventory_main.type_object type_object in Enum.GetValues(typeof(inventory_main.type_object)))
            // {
            //     // EditorGUILayout.LabelField(new GUIContent("Type " + type_object + ":"));
            //     // EditorGUILayout.PropertyField(type_object_container, new GUIContent("type_object_container Container :"));
            // }

        serializedObject.ApplyModifiedProperties ();


    }


         
         
 }