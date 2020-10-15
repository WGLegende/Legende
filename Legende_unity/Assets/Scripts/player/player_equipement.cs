using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player_equipement : MonoBehaviour
{
    public static player_equipement instance;
    Animator anim_player;
    [HideInInspector] public int mode_player;


    public GameObject epee_dos;
    bool epee_dos_toggle;

    public GameObject bow_dos;
    bool bow_dos_toggle;

    public GameObject bow_hand;
    public GameObject sword_hand;


    void Start(){

        if(instance == null){
            instance = this;
        }
        anim_player = GameObject.Find("Player").GetComponent<Animator>();

        bow_hand.SetActive(false);
        sword_hand.SetActive(false);
    }

    public void equipe_un_objet(inventory_object obj){
        if(helpers.i.isObjectAnArmor(obj)){
           player_armor.instance.equipe_armor(obj);
        }

        if(obj._type_equipement == enum_manager.equipement.arme_CaC){ 
            StartCoroutine(equip_player_cac());   
        }
        else if(obj._type_equipement == enum_manager.equipement.arme_Distance){
            StartCoroutine(equip_player_arc());
        }
    }

    public IEnumerator equip_player_noweapon(){

        if(mode_player != 0){

            if(mode_player == 1){
                anim_player.SetTrigger("disarm_bow");
            }
            else if(mode_player == 2){
                epee_dos_toggle = true;
                anim_player.SetTrigger("disarm_sword");
            }

            player_gamePad_manager.instance.canAttack = false;

            yield return new WaitForSecondsRealtime(0.4f);
            Animator_overrider.instance.Player_animator.Set(0); // noweapon
            mode_player = 0;
            bow_hand.SetActive(false);
           // player_gamePad_manager.instance.Arrow.SetActive(false);
            sword_hand.SetActive(false);
           
            player_gamePad_manager.instance.canAttack = true;
        }
    }

    public IEnumerator equip_player_arc(){

        if(mode_player != 1){

            anim_player.SetTrigger("equip_bow");
            player_gamePad_manager.instance.canAttack = false;

            yield return new WaitForSecondsRealtime(0.4f);
            Animator_overrider.instance.Player_animator.Set(1); // bow
            mode_player = 1; 
           
            player_gamePad_manager.instance.canAttack = true;
        }
    }

    public IEnumerator equip_player_cac(){

        if(mode_player != 2){

            epee_dos_toggle = false;
       
            anim_player.SetTrigger("equip_sword");
            player_gamePad_manager.instance.canAttack = false;

            yield return new WaitForSecondsRealtime(0.4f);
            Animator_overrider.instance.Player_animator.Set(2); // sword
            mode_player = 2; 
           
            player_gamePad_manager.instance.canAttack = true;
        }
    }



    void display_sword(){

        epee_dos.SetActive(epee_dos_toggle);
        sword_hand.SetActive(!epee_dos_toggle);

        bow_hand.SetActive(false);
        bow_dos.SetActive(true);
     
        Player_sound.instance.PlayMusicEventPlayer(Player_sound.instance.MusicEventPlayer[6]);
    }


    void anim_active_bow(){

        bow_dos.SetActive(false);
        bow_hand.SetActive(true);

        sword_hand.SetActive(false);
        epee_dos.SetActive(true);

        Player_sound.instance.PlayMusicEventPlayer(Player_sound.instance.MusicEventPlayer[6]);
    }


    void anim_desactive_bow(){

        bow_dos.SetActive(true);
        bow_hand.SetActive(false);
        Player_sound.instance.PlayMusicEventPlayer(Player_sound.instance.MusicEventPlayer[6]);
    }

   
}
