 using UnityEditor;
 using UnityEngine;
 
 [CustomEditor(typeof(object_event)), CanEditMultipleObjects]
 public class inventory_event_editor : Editor {
     
     public SerializedProperty 
            event_description,
            event_quantity_max,
            _eventTrigger,
            chanceOfEffect,
            _eventStartsWhen,
            _eventEffect,
            effect_delay_before,
            effect_duration,
            effect_delay_after,
            event_animation_trigger,
            eventAnimation,
            _enemy_type,
            _weapon_type,
            _attack_type,
            _comparateur,
            _damage_type,
            _target_object_type,
            _target_equipement_type,
            _armor_type,
            event_name_of_custom_trigger,
            event_condition_quantity_life,
            event_condition_quantity_armor,
            event_condition_countdown,
            effect_value,
            event_effect_type_armor,
            _target_effect_on_object_type,
            _target_effect_on_equipement_type,
            target_effect_on_object_id,
            _target_effect_on_type_caracteristiques,
            _target_effect_on_type_effets,
            otherEffect,

            _eventEnding,
            _ending_enemy_type,
            _ending_weapon_type,
            _ending_attack_type,
            _ending_damage_type,
            _ending_comparateur,
            _ending_armor_type,
            _ending_target_object_type,
            _ending_target_equipement_type,
            _ending_event_quantity_life,
            _ending_event_quantity_armor,
            _ending_event_countdown,
            _ending_event_name_of_custom_trigger,
            _ending_event_animation_trigger;
            
        
     void OnEnable () {
        
        event_description = serializedObject.FindProperty("event_description");
        event_quantity_max = serializedObject.FindProperty("event_quantity_max");
        _eventTrigger = serializedObject.FindProperty("_eventTrigger");
        chanceOfEffect = serializedObject.FindProperty("chanceOfEffect");
        _eventStartsWhen = serializedObject.FindProperty("_eventStartsWhen");
        _eventEffect = serializedObject.FindProperty("_eventEffect");
        effect_delay_before = serializedObject.FindProperty("effect_delay_before");
        effect_duration = serializedObject.FindProperty("effect_duration");
        effect_delay_after = serializedObject.FindProperty("effect_delay_after");

        event_animation_trigger = serializedObject.FindProperty("event_animation_trigger");
        eventAnimation = serializedObject.FindProperty("eventAnimation");
        
        _enemy_type = serializedObject.FindProperty("_enemy_type");
        _weapon_type = serializedObject.FindProperty("_weapon_type");
        _attack_type = serializedObject.FindProperty("_attack_type");
        _comparateur = serializedObject.FindProperty("_comparateur");
        _damage_type = serializedObject.FindProperty("_damage_type");
        _target_object_type = serializedObject.FindProperty("_target_object_type");
        _target_equipement_type = serializedObject.FindProperty("_target_equipement_type");
        _armor_type = serializedObject.FindProperty("_armor_type");

        event_name_of_custom_trigger = serializedObject.FindProperty("event_name_of_custom_trigger");
        event_condition_quantity_life = serializedObject.FindProperty("event_condition_quantity_life");
        event_condition_quantity_armor = serializedObject.FindProperty("event_condition_quantity_armor");
        event_condition_countdown = serializedObject.FindProperty("event_condition_countdown");
        
        effect_value = serializedObject.FindProperty("effect_value");
        event_effect_type_armor = serializedObject.FindProperty("event_effect_type_armor");

        _target_effect_on_object_type = serializedObject.FindProperty("_target_effect_on_object_type");
        _target_effect_on_equipement_type = serializedObject.FindProperty("_target_effect_on_equipement_type");
        target_effect_on_object_id = serializedObject.FindProperty("target_effect_on_object_id");
        _target_effect_on_type_caracteristiques = serializedObject.FindProperty("_target_effect_on_type_caracteristiques");
        _target_effect_on_type_effets = serializedObject.FindProperty("_target_effect_on_type_effets");
        otherEffect = serializedObject.FindProperty("otherEffect");

        _eventEnding = serializedObject.FindProperty("_eventEnding");
        _ending_enemy_type = serializedObject.FindProperty("_ending_enemy_type");
        _ending_weapon_type = serializedObject.FindProperty("_ending_weapon_type");
        _ending_attack_type = serializedObject.FindProperty("_ending_attack_type");
        _ending_damage_type = serializedObject.FindProperty("_ending_damage_type");
        _ending_comparateur = serializedObject.FindProperty("_ending_comparateur");
        _ending_armor_type = serializedObject.FindProperty("_ending_armor_type");
        _ending_target_object_type = serializedObject.FindProperty("_ending_target_object_type");
        _ending_target_equipement_type = serializedObject.FindProperty("_ending_target_equipement_type");
        _ending_event_quantity_life = serializedObject.FindProperty("_ending_event_quantity_life");
        _ending_event_quantity_armor = serializedObject.FindProperty("_ending_event_quantity_armor");
        _ending_event_countdown = serializedObject.FindProperty("_ending_event_countdown");
        _ending_event_name_of_custom_trigger = serializedObject.FindProperty("_ending_event_name_of_custom_trigger");
        _ending_event_animation_trigger = serializedObject.FindProperty("_ending_event_animation_trigger");
     }
     
    public override void OnInspectorGUI() {
        serializedObject.Update ();

        EditorGUILayout.PropertyField( event_description, new GUIContent("Description de l'évenement :") );  
        EditorGUILayout.PropertyField( event_quantity_max, new GUIContent("Evenements limités à (-1 = illimité):") );  
        EditorGUILayout.PropertyField(_eventTrigger, new GUIContent("Déclencheur de l'évenement :"));


        switch((inventory_objects_events.eventTrigger)_eventTrigger.enumValueIndex) {
            case inventory_objects_events.eventTrigger.none :
                EditorGUILayout.LabelField("L'évènement n'a aucun déclencheur pour l'instant.");
                break;
            case inventory_objects_events.eventTrigger.whenWalk :
                // nothing
                break;
            case inventory_objects_events.eventTrigger.whenTake :
                EditorGUILayout.PropertyField(_eventStartsWhen, new GUIContent("L'effet démarre quand ?"));
                break;
            case inventory_objects_events.eventTrigger.whenJump :
                EditorGUILayout.PropertyField(_eventStartsWhen, new GUIContent("L'effet démarre quand ?"));

                break;
            case inventory_objects_events.eventTrigger.whenSwim :
                EditorGUILayout.PropertyField(_eventStartsWhen, new GUIContent("L'effet démarre quand ?"));

                break;
            case inventory_objects_events.eventTrigger.whenKillEnemy :
                EditorGUILayout.PropertyField(_enemy_type, new GUIContent("Quel type d'ennemi ?"));
                EditorGUILayout.PropertyField(_weapon_type, new GUIContent("Avec quel type d'arme ?"));

                break;
            case inventory_objects_events.eventTrigger.whenDie :

                break;
            case inventory_objects_events.eventTrigger.whenBorn :

                break;
            case inventory_objects_events.eventTrigger.whenUse :

                break;
            case inventory_objects_events.eventTrigger.whenEquiped :

                break;
            case inventory_objects_events.eventTrigger.whenAttack :
                EditorGUILayout.PropertyField(_enemy_type, new GUIContent("Quel type d'ennemi ?"));
                EditorGUILayout.PropertyField(_weapon_type, new GUIContent("Avec quel type d'arme ?"));
                EditorGUILayout.PropertyField(_attack_type, new GUIContent("Quel type d'attaque ?"));
                EditorGUILayout.PropertyField(_eventStartsWhen, new GUIContent("L'effet démarre quand ?"));

                break;
            case inventory_objects_events.eventTrigger.whenPlayerGotDamage :

                EditorGUILayout.PropertyField(_enemy_type, new GUIContent("Quel type d'ennemi ?"));
                EditorGUILayout.PropertyField(_damage_type, new GUIContent("Avec quel type d'arme ?"));
                EditorGUILayout.PropertyField(_attack_type, new GUIContent("Quel type d'attaque ?"));
                break;
            case inventory_objects_events.eventTrigger.whenOtherObjectsEquiped :

                EditorGUILayout.PropertyField(_target_object_type, new GUIContent("Quel type d'objet ?"));
                EditorGUILayout.PropertyField(_target_equipement_type, new GUIContent("Quel type d'equipement ?"));
                break;
            case inventory_objects_events.eventTrigger.whenQuantityLifeIs :
                EditorGUILayout.PropertyField(_comparateur, new GUIContent("Comparateur :"));
                EditorGUILayout.PropertyField(event_condition_quantity_life, new GUIContent("Quantité ?"));

                break;
            case inventory_objects_events.eventTrigger.whenQuantityArmorIs :
                EditorGUILayout.PropertyField(_armor_type, new GUIContent("Type d'armure :"));
                EditorGUILayout.PropertyField(_comparateur, new GUIContent("Comparateur :"));
                EditorGUILayout.PropertyField(event_condition_quantity_armor, new GUIContent("Quantité ?"));
                

                break;
          
        }

        if((inventory_objects_events.eventTrigger)_eventTrigger.enumValueIndex != inventory_objects_events.eventTrigger.none){

            EditorGUILayout.IntSlider (chanceOfEffect, 0, 100, new GUIContent("Chance d'effet (%):"));

            EditorGUILayout.PropertyField(_eventEffect, new GUIContent("Effet :"));


            EditorGUILayout.PropertyField(eventAnimation, new GUIContent("Animation a jouer ?"));
            // EditorGUILayout.PropertyField(effect_delay_before, new GUIContent("Délai avant effet :"));
            // EditorGUILayout.PropertyField(effect_duration, new GUIContent("Durée de l'effet :"));

                
            switch((inventory_objects_events.eventEffect)_eventEffect.enumValueIndex) {
                case inventory_objects_events.eventEffect.none :
                    EditorGUILayout.LabelField("L'évènement n'a aucun effet pour l'instant.");

                break;
                case inventory_objects_events.eventEffect.openAme :
                    EditorGUILayout.LabelField("L'évènement appelle l'âme et lui envoie des infos a dire. TODO"); // todo 

                break;
                case inventory_objects_events.eventEffect.changeSpeed :
                    EditorGUILayout.PropertyField(effect_value, new GUIContent("Valeur de l'effet :"));
                break;
                case inventory_objects_events.eventEffect.changeLife :
                    EditorGUILayout.PropertyField(effect_value, new GUIContent("Valeur de l'effet :"));

                break;
                case inventory_objects_events.eventEffect.changeArmor :

                    EditorGUILayout.PropertyField(event_effect_type_armor, new GUIContent("Type d'armure :"));
                    EditorGUILayout.PropertyField(effect_value, new GUIContent("Valeur de l'effet :"));

                break;
                case inventory_objects_events.eventEffect.changeObjectCarac :
                    EditorGUILayout.PropertyField(_target_effect_on_object_type, new GUIContent("type d'objet :"));
                    EditorGUILayout.PropertyField(_target_effect_on_equipement_type, new GUIContent("type d'equipement :"));
                    EditorGUILayout.PropertyField(target_effect_on_object_id, new GUIContent("id objet (si unique, sinon laisser vide) :"));
                    EditorGUILayout.PropertyField(_target_effect_on_type_caracteristiques, new GUIContent("type de characteristique a changer :"));
                    EditorGUILayout.PropertyField(_target_effect_on_type_effets, new GUIContent("type d'effets de characteristique a changer :"));

                    EditorGUILayout.PropertyField(effect_value, new GUIContent("Valeur de l'effet :"));

                break;
                case inventory_objects_events.eventEffect.otherEffect :
                    EditorGUILayout.PropertyField(otherEffect, new GUIContent("Indiquez le nom de la fonction :"));
                break;
            }

            EditorGUILayout.PropertyField(_eventEnding, new GUIContent("Doit s'arrêter quand..."));


            switch((inventory_objects_events.eventEnding)_eventEnding.enumValueIndex) {
                case inventory_objects_events.eventEnding.none :
                    
                break;
                case inventory_objects_events.eventEnding.whenSwim :
                    
                break;
                case inventory_objects_events.eventEnding.whenKillEnemy :

                break;
                case inventory_objects_events.eventEnding.whenDie :
                    
                break;
                case inventory_objects_events.eventEnding.whenUse :

                break;
                case inventory_objects_events.eventEnding.whenAttack :
                    EditorGUILayout.PropertyField(_ending_enemy_type, new GUIContent("Quel type d'ennemi ?"));
                    EditorGUILayout.PropertyField(_ending_weapon_type, new GUIContent("Avec quel type d'arme ?"));
                    EditorGUILayout.PropertyField(_ending_attack_type, new GUIContent("Quel type d'attaque ?"));
                break;
                case inventory_objects_events.eventEnding.whenPlayerGotDamage :
                    EditorGUILayout.PropertyField(_ending_enemy_type, new GUIContent("Quel type d'ennemi ?"));
                    EditorGUILayout.PropertyField(_ending_damage_type, new GUIContent("Avec quel type d'arme ?"));
                    EditorGUILayout.PropertyField(_ending_attack_type, new GUIContent("Quel type d'attaque ?"));
                break;
                case inventory_objects_events.eventEnding.whenOtherObjectsEquiped :
                    EditorGUILayout.PropertyField(_ending_target_object_type, new GUIContent("Quel type d'objet ?"));
                    EditorGUILayout.PropertyField(_ending_target_equipement_type, new GUIContent("Quel type d'equipement ?"));
                break;
                case inventory_objects_events.eventEnding.whenQuantityLifeIs :
                    EditorGUILayout.PropertyField(_ending_comparateur, new GUIContent("Comparateur :"));
                    EditorGUILayout.PropertyField(_ending_event_quantity_life, new GUIContent("Quantité ?"));
                break;
                case inventory_objects_events.eventEnding.whenQuantityArmorIs :
                    EditorGUILayout.PropertyField(_ending_armor_type, new GUIContent("Type d'armure :"));
                    EditorGUILayout.PropertyField(_ending_comparateur, new GUIContent("Comparateur :"));
                    EditorGUILayout.PropertyField(_ending_event_quantity_armor, new GUIContent("Quantité ?"));
                break;
                case inventory_objects_events.eventEnding.whenACountDownReachesValue :
                    EditorGUILayout.PropertyField(_ending_event_countdown, new GUIContent("Valeur compte a rebours ?"));
                break;
                case inventory_objects_events.eventEnding.whenEventIsDone :
                    EditorGUILayout.PropertyField(_ending_event_name_of_custom_trigger, new GUIContent("Nom de l'evenement ?"));
                break;
                case inventory_objects_events.eventEnding.whenAnimationIsDone :
                    EditorGUILayout.PropertyField(_ending_event_animation_trigger, new GUIContent("Animation déclencheuse ?"));
                break;
                case inventory_objects_events.eventEnding.otherTrigger :
                    EditorGUILayout.PropertyField(_ending_event_name_of_custom_trigger, new GUIContent("Nom de l'evenement ?"));
                break;
            }
        }

        serializedObject.ApplyModifiedProperties ();
    }
 }