 using UnityEditor;
 using UnityEngine;
 
 [CustomEditor(typeof(inventory_object)), CanEditMultipleObjects]
 public class inventory_object_editor : Editor {
     
     public SerializedProperty 
         state_id,
         nom,
         description,
         image,
         quantite,
         jetable,
         max_stack,
         _type_object,
            _type_equipement,
            _type_consommable,
            _type_ressource,
            _type_plan,
            _type_carte,
            _type_quete,
            _type_savoir,
            _type_relique,

                vitesseCoup,
                degatsInfligesMin,
                degatsInfligesMax,
                pourcentageCritique,
                puissanceDeRecul,
                _type_effets_secondaire,
                    degatsSecondairesInfligesMin,
                    degatsSecondairesInfligesMax,
                montantArmure_min,
                montantArmure_max,
                    armureSecondaireMin,
                    armureSecondaireMax,

            portee;
                

        
     void OnEnable () {
         // Setup the SerializedProperties
         description = serializedObject.FindProperty ("description");

         state_id = serializedObject.FindProperty ("state_id");
         max_stack = serializedObject.FindProperty ("max_stack");
         nom = serializedObject.FindProperty ("nom");
         image = serializedObject.FindProperty ("image");
         _type_object = serializedObject.FindProperty ("_type_object");
         _type_equipement = serializedObject.FindProperty ("_type_equipement");
         _type_consommable = serializedObject.FindProperty ("_type_consommable");
         _type_ressource = serializedObject.FindProperty ("_type_ressource");
         _type_plan = serializedObject.FindProperty ("_type_plan");
         _type_carte = serializedObject.FindProperty ("_type_carte");
         _type_quete = serializedObject.FindProperty ("_type_quete");
         _type_savoir = serializedObject.FindProperty ("_type_savoir");
         _type_relique = serializedObject.FindProperty ("_type_relique");

             vitesseCoup = serializedObject.FindProperty ("vitesseCoup");
             degatsInfligesMin = serializedObject.FindProperty ("degatsInfligesMin");
             degatsInfligesMax = serializedObject.FindProperty ("degatsInfligesMax");
             pourcentageCritique = serializedObject.FindProperty ("pourcentageCritique");
             puissanceDeRecul = serializedObject.FindProperty ("puissanceDeRecul");
             _type_effets_secondaire = serializedObject.FindProperty ("_type_effets_secondaire");
                degatsSecondairesInfligesMin = serializedObject.FindProperty ("degatsSecondairesInfligesMin");
                degatsSecondairesInfligesMax = serializedObject.FindProperty ("degatsSecondairesInfligesMax");

             portee = serializedObject.FindProperty ("portee");


            montantArmure_min = serializedObject.FindProperty ("montantArmure_min");
            montantArmure_max = serializedObject.FindProperty ("montantArmure_max");
            armureSecondaireMin = serializedObject.FindProperty ("armureSecondaireMin");
            armureSecondaireMax = serializedObject.FindProperty ("armureSecondaireMax");

         quantite = serializedObject.FindProperty("quantite");
         jetable = serializedObject.FindProperty("jetable");

     }
     
    public string generate_random_id(){
        string alphabet = "qwertyuiopasdfghjklzxcvbnmQWERTYUIOPASDFGHJKLZXCVBNM1234567890";
        string new_id = "";
        for(int i = 0; i < 25; i++){
            new_id += alphabet.Substring(Random.Range(0, alphabet.Length-1), 1);
        }
        return new_id;
    }

    public override void OnInspectorGUI() {
        serializedObject.Update ();

        respectValueConstistancy();

        if(string.IsNullOrEmpty(state_id.stringValue) && state_id.isInstantiatedPrefab ){
            state_id.stringValue = generate_random_id();
        }
        EditorGUILayout.PropertyField( nom, new GUIContent("Nom et Description de l'objet :") );  
        serializedObject.targetObject.name = "obj_" + nom.stringValue;

        description.stringValue = EditorGUILayout.TextArea(description.stringValue, GUILayout.Height(60));
        EditorGUILayout.PropertyField(image, new GUIContent("Image dans l'inventaire :"));
        Texture2D texture = AssetPreview.GetAssetPreview(image.objectReferenceValue);
        GUI.Button(new Rect(20, 110, 75, 75), new GUIContent(texture));
        
        EditorGUILayout.Space(90);
        EditorGUILayout.PropertyField(jetable, new GUIContent("L'Objet peut-il être jeté ? "));

        EditorGUILayout.IntSlider (max_stack, 0, 100, new GUIContent("Nombre max dans un slot pour cet objet : ") );

        EditorGUILayout.PropertyField(_type_object, new GUIContent("Type d'objet :"));

        inventory_main.type_effets type_deg = (inventory_main.type_effets)_type_effets_secondaire.enumValueIndex;
        inventory_main.type_object type_object = (inventory_main.type_object)_type_object.enumValueIndex;

        switch( type_object ) {


            case inventory_main.type_object.equipement :

            EditorGUILayout.PropertyField(_type_equipement, new GUIContent("Type d'equipement :"));
            inventory_main.equipement equipement = (inventory_main.equipement)_type_equipement.enumValueIndex;

            switch( equipement ) {
                case inventory_main.equipement.arme_CaC : case inventory_main.equipement.arme_Projectile : case inventory_main.equipement.arme_Distance :

                    // ARMES
                    EditorGUILayout.PropertyField(vitesseCoup, new GUIContent("Vitesse du coup (en seconde) :"));
                    EditorGUILayout.PropertyField(puissanceDeRecul, new GUIContent("Puissance de recul :"));

                    EditorGUILayout.PropertyField(degatsInfligesMin, new GUIContent("Dégats minimum :"));
                    EditorGUILayout.PropertyField(degatsInfligesMax, new GUIContent("Dégats maximum :"));
                    EditorGUILayout.IntSlider ( pourcentageCritique, 0, 100, new GUIContent("Pourcentage coup critique : " + pourcentageCritique.intValue + "%"));

                    if(equipement == inventory_main.equipement.arme_Distance){
                        EditorGUILayout.PropertyField(portee, new GUIContent("Portée flèche :"));  
                    }else if(equipement == inventory_main.equipement.arme_Projectile){
                        EditorGUILayout.PropertyField(portee, new GUIContent("Portée projectile :"));  
                        EditorGUILayout.IntSlider (quantite, 0, 100, new GUIContent("Quantitée projectile :") );
                    }

                    EditorGUILayout.PropertyField(_type_effets_secondaire, new GUIContent("Type de dégats secondaires infligés :") );
                        switch( type_deg ) {
                            case inventory_main.type_effets.aucun:  break;
                            default :  
                                EditorGUILayout.PropertyField( degatsSecondairesInfligesMin, new GUIContent("Dégats secondaires minimum :") );            
                                EditorGUILayout.PropertyField( degatsSecondairesInfligesMax, new GUIContent("Dégats force brute maximum :") );            
                                break;
                        }
                    break;
                case inventory_main.equipement.armure_Tete : case inventory_main.equipement.armure_Corps : case inventory_main.equipement.armure_Mains : case inventory_main.equipement.armure_Pieds :
                    // ARMURES

                    EditorGUILayout.PropertyField( montantArmure_min, new GUIContent("Protection armure minimum :") );             
                    EditorGUILayout.PropertyField( montantArmure_max, new GUIContent("Protection armure maximum :") );             
                    EditorGUILayout.PropertyField(_type_effets_secondaire, new GUIContent("Protection secondaire :") );
                        switch( type_deg ) {
                            case inventory_main.type_effets.aucun:  break;
                            default :  
                                EditorGUILayout.PropertyField( armureSecondaireMin, new GUIContent("Protection secondaire minimum :") );            
                                EditorGUILayout.PropertyField( armureSecondaireMax, new GUIContent("Protection secondaire maximum :") );        
                                break;
                        }
                    break;
                }

            break;


            case inventory_main.type_object.consommable :
                EditorGUILayout.PropertyField(_type_consommable, new GUIContent("Type de consommable :"));
                inventory_main.consommable consommable = (inventory_main.consommable)_type_consommable.enumValueIndex;
                switch(consommable){
                    case inventory_main.consommable.change_player_caracteristique : case inventory_main.consommable.change_equipement_caracteristique :
                        // instruction ici
                    break;
                }
                
                EditorGUILayout.IntSlider ( quantite, 0, 100, new GUIContent("Quantitée trouvée :") );
            break;

            case inventory_main.type_object.ressource :

                EditorGUILayout.PropertyField(_type_ressource, new GUIContent("Type de ressource :"));
                inventory_main.ressource ressource = (inventory_main.ressource)_type_ressource.enumValueIndex;
                switch(ressource){
                    case inventory_main.ressource.bois:
                        // instruction ici ...
                    break;
                }
                
                EditorGUILayout.IntSlider ( quantite, 0, 100, new GUIContent("Quantitée trouvée :") );
            break;

            case inventory_main.type_object.plan :
                EditorGUILayout.PropertyField(_type_plan, new GUIContent("Type de plan :"));
                inventory_main.plan plan = (inventory_main.plan)_type_plan.enumValueIndex;
                switch(plan){
                    case inventory_main.plan.plan:
                        // instruction ici
                    break;
                }
            break;

            case inventory_main.type_object.carte :
                EditorGUILayout.PropertyField(_type_carte, new GUIContent("Type de carte :"));
                inventory_main.carte carte = (inventory_main.carte)_type_carte.enumValueIndex;
                switch(carte){
                    case inventory_main.carte.carte:
                        // instruction ici
                    break;
                }
            break;

            case inventory_main.type_object.quete :
                EditorGUILayout.PropertyField(_type_quete, new GUIContent("Type de quete :"));
                inventory_main.quete quete = (inventory_main.quete)_type_quete.enumValueIndex;
                switch(quete){
                    case inventory_main.quete.quete:
                        // instruction ici
                    break;
                }
            break;

            case inventory_main.type_object.savoir :
                EditorGUILayout.PropertyField(_type_savoir, new GUIContent("Type de savoir :"));
                inventory_main.savoir savoir = (inventory_main.savoir)_type_savoir.enumValueIndex;
                switch(savoir){
                    case inventory_main.savoir.savoir:
                        // instruction ici
                    break;
                }
            break;

            case inventory_main.type_object.relique :
                EditorGUILayout.PropertyField(_type_relique, new GUIContent("Type de relique :"));
                inventory_main.relique relique = (inventory_main.relique)_type_relique.enumValueIndex;
                switch(relique){
                    case inventory_main.relique.relique:
                        // instruction ici
                    break;
                }
            break;
        }

        EditorGUILayout.Space(50);
        EditorGUILayout.LabelField("ID - " + state_id.stringValue);
         
        if(GUILayout.Button("Reset ID")) {
            state_id.stringValue = generate_random_id();
        }

        serializedObject.ApplyModifiedProperties ();
    }

    public void respectValueConstistancy(){
        if(degatsSecondairesInfligesMax.floatValue < degatsSecondairesInfligesMin.floatValue){
            degatsSecondairesInfligesMax.floatValue = degatsSecondairesInfligesMin.floatValue;
        }
        if(degatsSecondairesInfligesMin.floatValue > degatsSecondairesInfligesMax.floatValue){
            degatsSecondairesInfligesMin.floatValue = degatsSecondairesInfligesMax.floatValue;
        }

        if(degatsInfligesMax.floatValue < degatsInfligesMin.floatValue){
            degatsInfligesMax.floatValue = degatsInfligesMin.floatValue;
        }
        if(degatsInfligesMin.floatValue > degatsInfligesMax.floatValue){
            degatsInfligesMin.floatValue = degatsInfligesMax.floatValue;
        }



        if(armureSecondaireMax.floatValue < armureSecondaireMin.floatValue){
            armureSecondaireMax.floatValue = armureSecondaireMin.floatValue;
        }
        if(armureSecondaireMin.floatValue > armureSecondaireMax.floatValue){
            armureSecondaireMin.floatValue = armureSecondaireMax.floatValue;
        }

        if(vitesseCoup.floatValue < 0.2f){
            vitesseCoup.floatValue = 0.2f;
        }
    }







 }