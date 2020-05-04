 using UnityEditor;
 using UnityEngine;
 
 [CustomEditor(typeof(enemy)), CanEditMultipleObjects]
 public class enemy_editor : Editor {
     
    public SerializedProperty 

    current_comportement,
     
    _deplacement,
    maxPv,
    degatMin,
    degatMax,
    rayon_d_attaque ,
    move_speed_attack ,
    move_speed_walk ,
    poids,
    rayon_d_actionMax ,
    courage,
    cadence_de_frappe,
    distance_attack,

        speedSentinel,
        speedPatrouille,
        _parcours,
        WayPoint,

    isFlying,
    altitude,

        isBowman,
        Projectile,
        OriginProjectile,
        power_projectile,
       

    nbrEnemy,
    cadence_enemy,
    emplacement_caserne,
    newEnemy,
    groupe_soutien;




        
     void OnEnable () {

        current_comportement = serializedObject.FindProperty ("current_comportement");

        _deplacement = serializedObject.FindProperty ("_deplacement");
        maxPv = serializedObject.FindProperty ("maxPv");
        degatMin = serializedObject.FindProperty ("degatMin");
        degatMax = serializedObject.FindProperty ("degatMax");
        rayon_d_attaque = serializedObject.FindProperty ("rayon_d_attaque");
        move_speed_attack = serializedObject.FindProperty ("move_speed_attack");
        move_speed_walk = serializedObject.FindProperty ("move_speed_walk");
        poids = serializedObject.FindProperty ("poids");
        rayon_d_actionMax = serializedObject.FindProperty ("rayon_d_actionMax");
        courage = serializedObject.FindProperty ("courage");
        cadence_de_frappe = serializedObject.FindProperty ("cadence_de_frappe");
        distance_attack = serializedObject.FindProperty ("distance_attack");


        speedSentinel = serializedObject.FindProperty ("speed_sentinelle");
        speedPatrouille = serializedObject.FindProperty ("speed_patrouille");

        _parcours = serializedObject.FindProperty ("_parcours");
        WayPoint = serializedObject.FindProperty ("WayPoint");

        isFlying = serializedObject.FindProperty ("isFlying");
        altitude = serializedObject.FindProperty ("altitude");

        isBowman = serializedObject.FindProperty ("isBowman");
        Projectile = serializedObject.FindProperty ("Projectile");
        OriginProjectile = serializedObject.FindProperty ("OriginProjectile");
        power_projectile = serializedObject.FindProperty ("power_projectile");
       
        nbrEnemy = serializedObject.FindProperty ("nbrEnemy");
        cadence_enemy = serializedObject.FindProperty ("cadence_enemy");
        emplacement_caserne = serializedObject.FindProperty ("emplacement_caserne");
        newEnemy = serializedObject.FindProperty ("newEnemy");
        groupe_soutien = serializedObject.FindProperty ("groupe_soutien");

    }
     
    public override void OnInspectorGUI() {

        serializedObject.Update ();
        EditorGUILayout.LabelField("Comportement actuel",EditorStyles.boldLabel);
        EditorGUILayout.PropertyField(current_comportement, new GUIContent("") ); 

        EditorGUILayout.LabelField("", GUI.skin.horizontalSlider); 

        EditorGUILayout.PropertyField( maxPv, new GUIContent("Maximum de PV : ") );  
        EditorGUILayout.PropertyField( degatMin, new GUIContent("Degat Min : "));  
        EditorGUILayout.PropertyField( degatMax, new GUIContent("Degat Max : "));  
        EditorGUILayout.PropertyField( rayon_d_attaque, new GUIContent("Rayon d'Attaque: "));  
        EditorGUILayout.PropertyField( move_speed_attack, new GUIContent("Vitesse d'Attaque : "));  
        EditorGUILayout.PropertyField( move_speed_walk, new GUIContent("Vitesse de Marche : "));  
        EditorGUILayout.PropertyField( rayon_d_actionMax, new GUIContent("Deplacement Max : "));  
        EditorGUILayout.PropertyField( courage, new GUIContent("Courage : "));  
        EditorGUILayout.PropertyField( cadence_de_frappe, new GUIContent("Cadence de Frappe : "));
        EditorGUILayout.PropertyField( distance_attack, new GUIContent("Distance d'Attaque : "));

        EditorGUILayout.LabelField("", GUI.skin.horizontalSlider);

        EditorGUILayout.PropertyField(isFlying, new GUIContent("Volant : "));
        if(isFlying.boolValue){
            EditorGUILayout.PropertyField(altitude, new GUIContent("Altitude : "));
        }

        EditorGUILayout.LabelField("", GUI.skin.horizontalSlider);

        EditorGUILayout.PropertyField(isBowman, new GUIContent("Tireur: "));
        if(isBowman.boolValue){
            EditorGUILayout.PropertyField(Projectile, new GUIContent("Projectile : "));
            EditorGUILayout.PropertyField(OriginProjectile, new GUIContent("Origine du Tir : "));
            EditorGUILayout.PropertyField(power_projectile, new GUIContent("Puissance de Tir : "));
        }

        EditorGUILayout.LabelField("", GUI.skin.horizontalSlider);

        EditorGUILayout.PropertyField(_deplacement, new GUIContent("Type :"));
        switch((enemy.deplacement)_deplacement.enumValueIndex){

            case enemy.deplacement.Garde :      
            break;
            case enemy.deplacement.Sentinel : 
            EditorGUILayout.PropertyField(speedSentinel, new GUIContent("Vitesse : "));
            break;
            case enemy.deplacement.Patrouille : 
            EditorGUILayout.PropertyField( _parcours, new GUIContent("Mode :"));
            EditorGUILayout.PropertyField(speedPatrouille, new GUIContent("Vitesse : "));
            EditorGUILayout.PropertyField( WayPoint, new GUIContent("Trajet :"));
            break;
        }

        EditorGUILayout.LabelField("", GUI.skin.horizontalSlider);

        EditorGUILayout.PropertyField(newEnemy, new GUIContent("Enemy : "));
        EditorGUILayout.PropertyField(nbrEnemy, new GUIContent("Nbr Enemy : "));
        EditorGUILayout.PropertyField(cadence_enemy, new GUIContent("Cadence Apparition : "));
        EditorGUILayout.PropertyField(emplacement_caserne, new GUIContent("Lieu Apparition : "));

        EditorGUILayout.LabelField("", GUI.skin.horizontalSlider);
        
        EditorGUILayout.PropertyField(groupe_soutien, new GUIContent("Groupe De Soutien : "));



          
        serializedObject.ApplyModifiedProperties ();
    }








 }