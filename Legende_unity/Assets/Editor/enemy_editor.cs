 using UnityEditor;
 using UnityEngine;
 
 [CustomEditor(typeof(enemy)), CanEditMultipleObjects]
 public class enemy_editor : Editor {
     
    public SerializedProperty 
     
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
    distance_attack_bowman;

                


        
     void OnEnable () {

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
            distance_attack_bowman = serializedObject.FindProperty ("distance_attack_bowman");



     }
     
    public override void OnInspectorGUI() {

        serializedObject.Update ();
      
        EditorGUILayout.PropertyField( maxPv, new GUIContent("Maximum de PV : ") );  
        EditorGUILayout.PropertyField( degatMin, new GUIContent("Degat Min : ") );  
        EditorGUILayout.PropertyField( degatMax, new GUIContent("Degat Max : ") );  
        EditorGUILayout.PropertyField( rayon_d_attaque, new GUIContent("Rayon d'Attaque: ") );  
        EditorGUILayout.PropertyField( move_speed_attack, new GUIContent("Vitesse d'Attaque : ") );  
        EditorGUILayout.PropertyField( move_speed_walk, new GUIContent("Vitesse de Marche : ") );  
        EditorGUILayout.PropertyField( rayon_d_actionMax, new GUIContent("Deplacement Max : ") );  
        EditorGUILayout.PropertyField( courage, new GUIContent("Courage : ") );  
        EditorGUILayout.PropertyField( cadence_de_frappe, new GUIContent("Cadence de Frappe : ") );

        EditorGUILayout.Space(10);

        EditorGUILayout.PropertyField(isFlying, new GUIContent("Volant : ") );
        if(isFlying.boolValue){
            EditorGUILayout.PropertyField(altitude, new GUIContent("Altitude : ") );
        }

        EditorGUILayout.PropertyField(isBowman, new GUIContent("Tireur: ") );
        if(isBowman.boolValue){
            EditorGUILayout.PropertyField(Projectile, new GUIContent("Projectile : ") );
            EditorGUILayout.PropertyField(OriginProjectile, new GUIContent("Origine du Tir : ") );
            EditorGUILayout.PropertyField(power_projectile, new GUIContent("Puissance de Tir : ") );
            EditorGUILayout.PropertyField(distance_attack_bowman, new GUIContent("Distance d'Attaque : ") );
        }

      
        EditorGUILayout.PropertyField(_deplacement, new GUIContent("Type :"));

        switch((enemy.deplacement)_deplacement.enumValueIndex){

            case enemy.deplacement.Garde :      
            break;

            case enemy.deplacement.Sentinel : 
            EditorGUILayout.PropertyField(speedSentinel, new GUIContent("Vitesse : "));
            break;

            case enemy.deplacement.Patrouille : 
            EditorGUILayout.PropertyField( _parcours, new GUIContent("Mode :"));
            EditorGUILayout.PropertyField(speedPatrouille, new GUIContent("Vitesse : ") );
            EditorGUILayout.PropertyField( WayPoint, new GUIContent("Trajet :"));
            break;
        }
       




        EditorGUILayout.Space(60);

        // EditorGUILayout.PropertyField( nom, new GUIContent("Nom et Description de l'objet :") );  
        
        // description.stringValue = EditorGUILayout.TextArea(description.stringValue, GUILayout.Height(60));
        // EditorGUILayout.PropertyField(image, new GUIContent("Image dans l'inventaire :"));
        // Texture2D texture = AssetPreview.GetAssetPreview(image.objectReferenceValue);
        // GUI.Button(new Rect(200, 85, 75, 75), new GUIContent(texture));
        
        // EditorGUILayout.Space(60);

        // EditorGUILayout.PropertyField(_type_object, new GUIContent("Type d'objet :"));
         
        // inventory_main.type_effets type_deg = (inventory_main.type_effets)_type_effets_secondaire.enumValueIndex;

        // switch( st ) {

        // // ARMES
        //     case inventory_main.type_object.arme_CaC : case inventory_main.type_object.arme_Projectile : case inventory_main.type_object.arme_Distance :


        //         EditorGUILayout.PropertyField(vitesseCoup, new GUIContent("Vitesse du coup (en seconde) :"));
        //         EditorGUILayout.PropertyField(puissanceDeRecul, new GUIContent("Puissance de recul :"));

        //         EditorGUILayout.PropertyField(degatsInfligesMin, new GUIContent("Dégats minimum :"));
        //         EditorGUILayout.PropertyField(degatsInfligesMax, new GUIContent("Dégats maximum :"));
        //         EditorGUILayout.IntSlider ( pourcentageCritique, 0, 100, new GUIContent("Pourcentage coup critique : " + pourcentageCritique.intValue + "%"));

        //         if(st == inventory_main.type_object.arme_Distance){
        //             EditorGUILayout.PropertyField(portee, new GUIContent("Portée flèche :"));  
        //         }else if(st == inventory_main.type_object.arme_Projectile){
        //             EditorGUILayout.PropertyField(portee, new GUIContent("Portée projectile :"));  
        //             EditorGUILayout.IntSlider (quantite, 0, 100, new GUIContent("Quantitée projectile :") );
        //         }

        //         EditorGUILayout.PropertyField(_type_effets_secondaire, new GUIContent("Type de dégats secondaires infligés :") );
        //             switch( type_deg ) {
        //                 case inventory_main.type_effets.aucun:  break;
        //                 default :  
        //                     EditorGUILayout.PropertyField( degatsSecondairesInfligesMin, new GUIContent("Dégats secondaires minimum :") );            
        //                     EditorGUILayout.PropertyField( degatsSecondairesInfligesMax, new GUIContent("Dégats force brute maximum :") );            
        //                     break;
        //             }
        //         break;
                

        // // ARMURES
        //     case inventory_main.type_object.armure_Tete : case inventory_main.type_object.armure_Corps : case inventory_main.type_object.armure_Mains : case inventory_main.type_object.armure_Pieds :

        //         EditorGUILayout.PropertyField( montantArmure_min, new GUIContent("Protection armure minimum :") );             
        //         EditorGUILayout.PropertyField( montantArmure_max, new GUIContent("Protection armure maximum :") );             
        //         EditorGUILayout.PropertyField(_type_effets_secondaire, new GUIContent("Protection secondaire :") );
        //             switch( type_deg ) {
        //                 case inventory_main.type_effets.aucun:  break;
        //                 default :  
        //                     EditorGUILayout.PropertyField( armureSecondaireMin, new GUIContent("Protection secondaire minimum :") );            
        //                     EditorGUILayout.PropertyField( armureSecondaireMax, new GUIContent("Protection secondaire maximum :") );        
        //                     break;
        //             }
        //         break;



        //     case inventory_main.type_object.consommable_player:  case inventory_main.type_object.consommable_ressources:       

        //         EditorGUILayout.IntSlider ( quantite, 0, 100, new GUIContent("Quantitée trouvée :") );
        //         break;

        //     case inventory_main.type_object.relique_relique:  case inventory_main.type_object.relique_composant:    
            
        //         break;

        // }
         
         
        serializedObject.ApplyModifiedProperties ();
    }








 }