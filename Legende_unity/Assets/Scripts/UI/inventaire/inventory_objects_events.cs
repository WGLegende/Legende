using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class inventory_objects_events : MonoBehaviour
{
    public static inventory_objects_events instance;
// what happens to trigger the effect
    public enum eventTrigger{ 
        none,
        whenTake,
        whenWalk,
        whenJump,
        whenSwim,
        whenKillEnemy, // Enemy_type // Weapon_type
        whenDie,
        whenBorn,
        whenUse, 
        whenEquiped,
        whenAttack, // Enemy_type // Weapon_type // Attack_type
        whenPlayerGotDamage, // Enemy_type // Damage_type // Attack_type
        whenOtherObjectsEquiped, // Object_type // Equipement_type
        whenQuantityLifeIs, // lower, lower or equal, equal, higher or equal, higher
        whenQuantityArmorIs // lower, lower or equal, equal, higher or equal, higher
    }

// When, precisally, must the effect takes place
    public enum eventStartsWhen{ 
        none,
        immediately,
        continuously,
        before,
        during,
        after
    }
  

// what happens when the effect occure
    public enum eventEffect{
        none,
        openAme,
        changeSpeed, // define walk, swim, attaque, kart...
        changeLife, // max life
        changeArmor, // define type(s)
        changeObjectCarac, // define type(s)
        otherEffect
    }


// what happens when the effect occure
    public enum eventEnding{
        none,
        whenSwim,
        whenKillEnemy, // Enemy_type // Weapon_type
        whenDie,
        whenUse, 
        whenAttack, // Enemy_type // Weapon_type // Attack_type
        whenPlayerGotDamage, // Enemy_type // Damage_type // Attack_type
        whenOtherObjectsEquiped, // Object_type // Equipement_type
        whenQuantityLifeIs, // lower, lower or equal, equal, higher or equal, higher
        whenQuantityArmorIs, // lower, lower or equal, equal, higher or equal, higher
        whenACountDownReachesValue,
        whenEventIsDone, // event name
        whenAnimationIsDone,   // animation id
        otherTrigger // name of the function (maybe DDL ?)
    }

        float testal = 4f;

    void Start(){
        if(instance == null){
            instance = this;
        }
    }



    void FixedUpdate(){

        Debug.Log(testal);

    }


    public bool check_a_trigger(
                                    object_event obj_event,
                                    enum_manager.Enemy_type?  _enemy_type = null,
                                    enum_manager.Weapon_type? _weapon_type = null,
                                    enum_manager.Attack_type? _attack_type = null,
                                    enum_manager.type_effets? _damage_type = null,
                                    enum_manager.type_object? _object_type = null,
                                    enum_manager.equipement?  _equipement_type = null,
                                    enum_manager.Comparateur? _comparateur = null,
                                    float? _value_to_compare = null

                                )
    {

        inventory_object obj = obj_event.GetComponent<inventory_object>();

        switch(obj_event._eventTrigger){
            case eventTrigger.whenKillEnemy : return obj_event._enemy_type == _enemy_type && obj_event._weapon_type == _weapon_type;

            case eventTrigger.whenAttack : return obj_event._enemy_type == _enemy_type && obj_event._weapon_type == _weapon_type && obj_event._attack_type == _attack_type;

            case eventTrigger.whenPlayerGotDamage : return obj_event._enemy_type == _enemy_type && obj_event._damage_type == _damage_type && obj_event._attack_type == _attack_type;

            case eventTrigger.whenOtherObjectsEquiped : return obj_event._target_object_type == _object_type &&  obj_event._target_equipement_type == _equipement_type;

            case eventTrigger.whenQuantityLifeIs : 

                bool validQtyLife = false;
                switch(obj_event._comparateur){
                    case enum_manager.Comparateur.lower :          validQtyLife = _value_to_compare <  obj_event.event_condition_quantity_life; break;
                    case enum_manager.Comparateur.lowerOrEqual :   validQtyLife = _value_to_compare <= obj_event.event_condition_quantity_life; break;
                    case enum_manager.Comparateur.equal :          validQtyLife = _value_to_compare == obj_event.event_condition_quantity_life; break;
                    case enum_manager.Comparateur.higherOrEquals : validQtyLife = _value_to_compare >= obj_event.event_condition_quantity_life; break;
                    case enum_manager.Comparateur.higher :         validQtyLife = _value_to_compare >  obj_event.event_condition_quantity_life; break;
                }
                return validQtyLife;

            case eventTrigger.whenQuantityArmorIs :
                bool validQtyArmor = false;
                switch(obj_event._comparateur){
                    case enum_manager.Comparateur.lower :          validQtyArmor = _value_to_compare <  obj_event.event_condition_quantity_armor; break;
                    case enum_manager.Comparateur.lowerOrEqual :   validQtyArmor = _value_to_compare <= obj_event.event_condition_quantity_armor; break;
                    case enum_manager.Comparateur.equal :          validQtyArmor = _value_to_compare == obj_event.event_condition_quantity_armor; break;
                    case enum_manager.Comparateur.higherOrEquals : validQtyArmor = _value_to_compare >= obj_event.event_condition_quantity_armor; break;
                    case enum_manager.Comparateur.higher :         validQtyArmor = _value_to_compare >  obj_event.event_condition_quantity_armor; break;
                }
                return validQtyArmor;
        }

        return true;

    }


    public void trigger_an_event(object_event obj_event){
        inventory_object obj = obj_event.GetComponent<inventory_object>();

        Debug.Log(obj.nom + " lance un evenement : " + obj_event.event_description);


        switch(obj_event._eventEffect){
            case eventEffect.none :break;


            case eventEffect.openAme :
                // todo
            break;
            case eventEffect.changeSpeed :
                // StartCoroutine(effect_countdown((x) => testal = x, testal, 4f, 15f));

            break;
            case eventEffect.changeLife :

            break;
            case eventEffect.changeArmor :

            break;
            case eventEffect.changeObjectCarac :

            break;
            case eventEffect.otherEffect :

            break;
        }





    IEnumerator effect_countdown(System.Action<float> saveValue, float originalValue, float countdown, float addValue){
        saveValue(originalValue + addValue);
        yield return new WaitForSeconds(countdown);
        saveValue(originalValue);
    }





    }




















}
