 using UnityEditor;
 using UnityEngine;
 
 [CustomEditor(typeof(EnemyAttack)), CanEditMultipleObjects]
 public class enemy_attack_editor : Editor {
     
    public SerializedProperty 

    degatMin,
    degatMax,
    cadence_de_frappe,

    _type_attack,
    
    distance_C_a_C,
    distance_shoot,

    Pcent_attackSecondaire,
    
    Pcent_attackSpecial,
    Pcent_gain_Supp,
    Pcent_gain_attackSpecial,

    power_projectile,
    Projectile,
    OriginProjectile,
    
    force_aspiration,
    duree_aspiration,
    distance_aspiration;

        
    void OnEnable () {

        degatMin = serializedObject.FindProperty ("degatMin");
        degatMax = serializedObject.FindProperty ("degatMax");
        cadence_de_frappe = serializedObject.FindProperty ("cadence_de_frappe");

        _type_attack = serializedObject.FindProperty ("_type_attack");

        distance_C_a_C = serializedObject.FindProperty ("distance_C_a_C");
        distance_shoot = serializedObject.FindProperty ("distance_shoot");

        Pcent_attackSecondaire = serializedObject.FindProperty ("Pcent_attackSecondaire");
        Pcent_attackSpecial = serializedObject.FindProperty ("Pcent_attackSpecial");
        Pcent_gain_Supp = serializedObject.FindProperty ("Pcent_gain_Supp");
        Pcent_gain_attackSpecial = serializedObject.FindProperty ("Pcent_gain_attackSpecial");

        power_projectile = serializedObject.FindProperty ("power_projectile");
        Projectile = serializedObject.FindProperty ("Projectile");
        OriginProjectile = serializedObject.FindProperty ("OriginProjectile");

        force_aspiration = serializedObject.FindProperty ("force_aspiration");
        duree_aspiration = serializedObject.FindProperty ("duree_aspiration");
        distance_aspiration = serializedObject.FindProperty ("distance_aspiration");       

    }
     
    public override void OnInspectorGUI() {

        serializedObject.Update ();

        EditorGUILayout.PropertyField( degatMin, new GUIContent("Degat Min : "));
        EditorGUILayout.PropertyField( degatMax, new GUIContent("Degat Max : "));
        EditorGUILayout.PropertyField( cadence_de_frappe, new GUIContent("Cadence de Frappe : ") );

        EditorGUILayout.LabelField("", GUI.skin.horizontalSlider); 

        EditorGUILayout.LabelField("Type d'Attaque",EditorStyles.boldLabel);
        EditorGUILayout.PropertyField(_type_attack, new GUIContent("Type :"));

        switch((enum_manager.typeAttack)_type_attack.enumValueIndex){

            case enum_manager.typeAttack.Cac : 

                EditorGUILayout.PropertyField( distance_C_a_C, new GUIContent("Distance Attaque : "));
                EditorGUILayout.PropertyField( Pcent_attackSecondaire, new GUIContent("% Attack Secondaire : ") );
                EditorGUILayout.PropertyField( Pcent_attackSpecial, new GUIContent("% Attack Special : ") );

                if(Pcent_attackSpecial.floatValue > 0){
                    EditorGUILayout.PropertyField(force_aspiration, new GUIContent("Force Aspiration : "));
                    EditorGUILayout.PropertyField(duree_aspiration, new GUIContent("Duree Aspiration : "));
                    EditorGUILayout.PropertyField(distance_aspiration, new GUIContent("Distance Aspiration : "));
                    EditorGUILayout.PropertyField(Pcent_gain_Supp, new GUIContent("Gain Supp. : ")); 
                }    
            break;




            case enum_manager.typeAttack.distance : 
                EditorGUILayout.PropertyField( distance_shoot, new GUIContent("Distance Attaque : "));
                EditorGUILayout.PropertyField( power_projectile, new GUIContent("Force de Tir : "));
                EditorGUILayout.PropertyField( Projectile, new GUIContent("Projectile : "));
                EditorGUILayout.PropertyField( OriginProjectile, new GUIContent("Origin Projectile : "));
            break;



            case enum_manager.typeAttack.Cac_et_distance : 

                EditorGUILayout.PropertyField( distance_C_a_C, new GUIContent("Dist. Attack Cac : "));
                EditorGUILayout.PropertyField( distance_shoot, new GUIContent("Dist. Attack Shoot : "));
                EditorGUILayout.PropertyField( Pcent_attackSecondaire, new GUIContent("% Attack Secondaire : "));
                EditorGUILayout.PropertyField( Pcent_attackSpecial, new GUIContent("% Attack Special : "));

                if(Pcent_attackSpecial.floatValue > 0){
                    EditorGUILayout.PropertyField(force_aspiration, new GUIContent("Force Aspiration : "));
                    EditorGUILayout.PropertyField(duree_aspiration, new GUIContent("Duree Aspiration : "));
                    EditorGUILayout.PropertyField(distance_aspiration, new GUIContent("Distance Aspiration : ")); 
                    EditorGUILayout.PropertyField(Pcent_gain_Supp, new GUIContent("Gain Supp. : ")); 

                }
                EditorGUILayout.LabelField("", GUI.skin.horizontalSlider); 
                EditorGUILayout.PropertyField(Projectile, new GUIContent("Projectile : "));
                EditorGUILayout.PropertyField(OriginProjectile, new GUIContent("Origin Projectile : "));   
            break;
        }

        
        serializedObject.ApplyModifiedProperties ();
    }








 }