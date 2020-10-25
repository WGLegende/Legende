using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class object_event : MonoBehaviour
{
    public string event_description;
    public int event_quantity_max = -1; // -1 = unlimited effect

    public inventory_objects_events.eventTrigger _eventTrigger;

    public int chanceOfEffect = 100;
    public inventory_objects_events.eventStartsWhen _eventStartsWhen;

    public inventory_objects_events.eventEffect _eventEffect;

    public float effect_delay_before;
    public float effect_duration;
    public float effect_delay_after;

    public Animation eventAnimation;

    public enum_manager.Enemy_type _enemy_type;
    public enum_manager.Weapon_type _weapon_type;
    public enum_manager.Attack_type _attack_type;
    public enum_manager.Comparateur _comparateur;
    public enum_manager.type_effets _damage_type;
    public enum_manager.type_effets _armor_type;
    public enum_manager.type_object _target_object_type;
    public enum_manager.equipement _target_equipement_type;

    public int event_condition_quantity_life;
    public int event_condition_quantity_armor;

    public float effect_value;
    public enum_manager.type_effets event_effect_type_armor;

    public enum_manager.type_object _target_effect_on_object_type;
    public enum_manager.equipement _target_effect_on_equipement_type;
    public string target_effect_on_object_id;

    public enum_manager.type_caracteristiques _target_effect_on_type_caracteristiques;
    public enum_manager.type_effets _target_effect_on_type_effets;

    public string otherEffect;

    public inventory_objects_events.eventEnding _eventEnding;

    public enum_manager.Enemy_type _ending_enemy_type;
    public enum_manager.Weapon_type _ending_weapon_type;
    public enum_manager.Attack_type _ending_attack_type;
    public enum_manager.type_effets _ending_damage_type;

    public enum_manager.Comparateur _ending_comparateur;
    public enum_manager.type_effets _ending_armor_type;
    public enum_manager.type_object _ending_target_object_type;
    public enum_manager.equipement _ending_target_equipement_type;
    public int _ending_event_quantity_life;
    public int _ending_event_quantity_armor;
    public float _ending_event_countdown;
    public string _ending_event_name_of_custom_trigger;
    public Animation _ending_event_animation_trigger;
}
