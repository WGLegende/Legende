 using UnityEditor;
 using UnityEngine;
 
 [CustomEditor(typeof(inventory_object)), CanEditMultipleObjects]
 public class inventory_object_editor : Editor {
     
     public SerializedProperty 
         nom,
         description,
         image,
         quantite,
         _type_object,

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

         nom = serializedObject.FindProperty ("nom");
         image = serializedObject.FindProperty ("image");
         _type_object = serializedObject.FindProperty ("_type_object");

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
     }
     
    public override void OnInspectorGUI() {
        serializedObject.Update ();
        inventory_main.type_object st = (inventory_main.type_object)_type_object.enumValueIndex;



        respectValueConstistancy();

        // GUIStyle titre = new GUIStyle();
        // titre.fontSize = 35;
        // GUIStyle sous_titre = new GUIStyle();
        // sous_titre.fontSize = 15;


        // EditorGUILayout.LabelField(string.IsNullOrEmpty(nom.stringValue) ? "Nouvel objet" : nom.stringValue, titre);
        // EditorGUILayout.Space(25);

        // EditorGUILayout.LabelField(st.ToString(), sous_titre);

        // EditorGUILayout.Space(50);


        EditorGUILayout.PropertyField( nom, new GUIContent("Nom et Description de l'objet :") );  
        
        description.stringValue = EditorGUILayout.TextArea(description.stringValue, GUILayout.Height(60));
        EditorGUILayout.PropertyField(image, new GUIContent("Image dans l'inventaire :"));
        Texture2D texture = AssetPreview.GetAssetPreview(image.objectReferenceValue);
        GUI.Button(new Rect(200, 85, 75, 75), new GUIContent(texture));
        
        EditorGUILayout.Space(60);

        EditorGUILayout.PropertyField(_type_object, new GUIContent("Type d'objet :"));
         
        inventory_main.type_effets type_deg = (inventory_main.type_effets)_type_effets_secondaire.enumValueIndex;

        switch( st ) {

        // ARMES
            case inventory_main.type_object.arme_CaC : case inventory_main.type_object.arme_Projectile : case inventory_main.type_object.arme_Distance :


                EditorGUILayout.PropertyField(vitesseCoup, new GUIContent("Vitesse du coup (en seconde) :"));
                EditorGUILayout.PropertyField(puissanceDeRecul, new GUIContent("Puissance de recul :"));

                EditorGUILayout.PropertyField(degatsInfligesMin, new GUIContent("Dégats minimum :"));
                EditorGUILayout.PropertyField(degatsInfligesMax, new GUIContent("Dégats maximum :"));
                EditorGUILayout.IntSlider ( pourcentageCritique, 0, 100, new GUIContent("Pourcentage coup critique : " + pourcentageCritique.intValue + "%"));

                if(st == inventory_main.type_object.arme_Distance){
                    EditorGUILayout.PropertyField(portee, new GUIContent("Portée flèche :"));  
                }else if(st == inventory_main.type_object.arme_Projectile){
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
                

        // ARMURES
            case inventory_main.type_object.armure_Tete : case inventory_main.type_object.armure_Corps : case inventory_main.type_object.armure_Mains : case inventory_main.type_object.armure_Pieds :

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



            case inventory_main.type_object.consommable_player:  case inventory_main.type_object.consommable_ressources:       

                EditorGUILayout.IntSlider ( quantite, 0, 100, new GUIContent("Quantitée trouvée :") );
                break;

            case inventory_main.type_object.relique_relique:  case inventory_main.type_object.relique_composant:    
            
                break;

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