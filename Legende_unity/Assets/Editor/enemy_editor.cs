 using UnityEditor;
 using UnityEngine;
 
 [CustomEditor(typeof(enemy)), CanEditMultipleObjects]
 public class enemy_editor : Editor {
     
    public SerializedProperty 

    current_comportement,
    _race,
     
    _deplacement,
    maxPv,
    rayon_d_attaque,
    angle_de_vison,
    move_speed_attack,
    move_speed_walk,
    poids,
    rayon_d_actionMax,
    courage,
   // distance_attack,

        speedSentinel,
        speedPatrouille,
        _parcours,
        WayPoint,

    isFlying,
    altitude,

    nbrEnemy,
    cadence_enemy,
    emplacement_caserne,
    newEnemy,
    groupe_soutien,
    bouclier;

        
     void OnEnable () {

        current_comportement = serializedObject.FindProperty ("current_comportement");
        _race = serializedObject.FindProperty ("_race");
        _deplacement = serializedObject.FindProperty ("_deplacement");
        maxPv = serializedObject.FindProperty ("maxPv");
        rayon_d_attaque = serializedObject.FindProperty ("rayon_d_attaque");
        angle_de_vison = serializedObject.FindProperty ("angle_de_vison");
        move_speed_attack = serializedObject.FindProperty ("move_speed_attack");
        move_speed_walk = serializedObject.FindProperty ("move_speed_walk");
        poids = serializedObject.FindProperty ("poids");
        rayon_d_actionMax = serializedObject.FindProperty ("rayon_d_actionMax");
        courage = serializedObject.FindProperty ("courage");
        groupe_soutien = serializedObject.FindProperty ("groupe_soutien");
        bouclier = serializedObject.FindProperty ("bouclier");
        
       //distance_attack = serializedObject.FindProperty ("distance_attack");


        speedSentinel = serializedObject.FindProperty ("speed_sentinelle");
        speedPatrouille = serializedObject.FindProperty ("speed_patrouille");

        _parcours = serializedObject.FindProperty ("_parcours");
        WayPoint = serializedObject.FindProperty ("WayPoint");

        isFlying = serializedObject.FindProperty ("isFlying");
        altitude = serializedObject.FindProperty ("altitude");
       
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


        EditorGUILayout.LabelField("Race",EditorStyles.boldLabel);
        EditorGUILayout.PropertyField(_race, new GUIContent("") ); 
        EditorGUILayout.LabelField("", GUI.skin.horizontalSlider); 

      
        EditorGUILayout.PropertyField( maxPv, new GUIContent("Maximum de PV : ") );
        EditorGUILayout.PropertyField( rayon_d_attaque, new GUIContent("Rayon d'Attaque : ")); 
        EditorGUILayout.PropertyField( angle_de_vison, new GUIContent("Angle de Vision : "));  
        EditorGUILayout.PropertyField( move_speed_attack, new GUIContent("Vitesse d'Attaque : "));  
        EditorGUILayout.PropertyField( move_speed_walk, new GUIContent("Vitesse de Marche : "));  
        EditorGUILayout.PropertyField( rayon_d_actionMax, new GUIContent("Deplacement Max : "));  
        EditorGUILayout.PropertyField( courage, new GUIContent("Courage : ")); 
        EditorGUILayout.PropertyField(bouclier, new GUIContent("bouclier"));    
       // EditorGUILayout.PropertyField( distance_attack, new GUIContent("Distance d'Attaque : "));

        EditorGUILayout.LabelField("", GUI.skin.horizontalSlider);

        EditorGUILayout.PropertyField(isFlying, new GUIContent("Volant : "));
        if(isFlying.boolValue){
            EditorGUILayout.PropertyField(altitude, new GUIContent("Altitude : "));
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