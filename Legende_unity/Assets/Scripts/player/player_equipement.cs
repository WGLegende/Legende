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


    void Start(){

        if(instance == null){
            instance = this;
        }
        anim_player = GameObject.Find("Player").GetComponent<Animator>();
    }

    public void equipe_un_objet(inventory_object obj){

        print("Objet ramasse: "+ obj.nom);

        // switch(obj.nom){    
        // }

        if(obj._type_equipement == inventory_main.equipement.arme_CaC){ 
            StartCoroutine(equip_player_cac());   
        }

        else if(obj._type_equipement == inventory_main.equipement.arme_Distance){
            StartCoroutine(equip_player_arc());
        }
    }

    public IEnumerator equip_player_noweapon(){

        if(mode_player != 0){

            if(mode_player == 1){
                anim_player.SetTrigger("disarm_bow");
            }
            else if(mode_player == 2){
                anim_player.SetTrigger("disarm_sword");
            }

            //Player_sound.instance.PlayMusicEventPlayer(Player_sound.instance.MusicEventPlayer[6]); 
            player_gamePad_manager.instance.canAttack = false;

            yield return new WaitForSecondsRealtime(0.4f);
            Animator_overrider.instance.Player_animator.Set(0); // noweapon
            mode_player = 0;
            player_gamePad_manager.instance.Bow.SetActive(false);
            player_gamePad_manager.instance.Arrow.SetActive(false);
            player_gamePad_manager.instance.Sword.SetActive(false);
            //player_gamePad_manager.instance.Shield.SetActive(false); pas de shield de prevu pour le moment
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
            player_gamePad_manager.instance.Arrow.SetActive(true);
            player_gamePad_manager.instance.Sword.SetActive(false);
            //player_gamePad_manager.instance.Shield.SetActive(false); pas de shield de prevu pour le moment
            player_gamePad_manager.instance.canAttack = true;
        }
    }

    public IEnumerator equip_player_cac(){

        if(mode_player != 2){
       
            anim_player.SetTrigger("equip_sword");
            player_gamePad_manager.instance.canAttack = false;

            yield return new WaitForSecondsRealtime(0.4f);
            Animator_overrider.instance.Player_animator.Set(2); // sword
            mode_player = 2; 
            player_gamePad_manager.instance.Bow.SetActive(false);
            player_gamePad_manager.instance.Arrow.SetActive(false);
           
            // player_gamePad_manager.instance.Shield.SetActive(true); pas de shield de prevu pour le moment
            player_gamePad_manager.instance.canAttack = true;
        }
    }



    void display_sword(){

        epee_dos.SetActive(epee_dos_toggle);
        player_gamePad_manager.instance.Sword.SetActive(!epee_dos_toggle);
        Player_sound.instance.PlayMusicEventPlayer(Player_sound.instance.MusicEventPlayer[6]);

        epee_dos_toggle = !epee_dos_toggle;
    }

    void display_bow(){

        bow_dos.SetActive(bow_dos_toggle);
        player_gamePad_manager.instance.Bow.SetActive(!bow_dos_toggle);
        Player_sound.instance.PlayMusicEventPlayer(Player_sound.instance.MusicEventPlayer[6]);

        bow_dos_toggle = !bow_dos_toggle;
    }

   
}
