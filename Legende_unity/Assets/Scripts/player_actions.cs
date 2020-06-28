using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class player_actions : MonoBehaviour
{
    public static player_actions instance;
    public object currently_displayed_action;

    void Start(){ 
        
        if(instance == null){
            instance = this;
        }   
    }

   

    public void display_actions(object action_init, Collider collider){

        if(collider.tag != "Player")
        return;

        currently_displayed_action = action_init;

        if(action_init.GetType() == typeof(Porte)){
            ButtonAction.instance.Action("Ouvrir"); 
        }
        
        if(action_init.GetType() == typeof(Coffre)){
            ButtonAction.instance.Action("Ouvrir"); 
        }
         





        GamePad_manager.instance._game_pad_attribution = GamePad_manager.game_pad_attribution.actionDisplay;
    }

    public void do_action(){

        if(currently_displayed_action.GetType() == typeof(Porte)){ 
            do_action_porte((Porte)currently_displayed_action);
        } 

        if(currently_displayed_action.GetType() == typeof(Coffre)){ 
            do_action_coffre((Coffre)currently_displayed_action);
        } 


        clear_action(true);
    }



    public void clear_action(bool isPlayer){

        if(isPlayer){
            ButtonAction.instance.Hide(); 
            GamePad_manager.instance._game_pad_attribution = GamePad_manager.game_pad_attribution.player; // TODO attention avec kart
            currently_displayed_action = null;
        }

    }


    public void do_action_coffre(Coffre _coffre){

        if(!_coffre.isOpen){

            _coffre.isOpen = true;
          

            if(!_coffre.petit_coffre){

                _coffre.anim.SetTrigger("OpenCoffre");
                player_gamePad_manager.canMove = false;
                player_gamePad_manager.canAttack = false;
                player_gamePad_manager.canJump = false;
                Player_sound.instance.PlayMusicEventPlayer(Player_sound.instance.MusicEventPlayer[0]); 
                StartCoroutine(FadeMixer.StartFade(Music_sound.instance.MusicMaster, "musicMasterVolume", 2f , 0f)); // cut zic
            }
            else{
                _coffre.anim.SetTrigger("OpenPetitCoffre");
                _coffre.Invoke("activeObject",0.5f);
            }
        }
    }

    





    public void do_action_porte(Porte _porte){

        if (_porte.Switch == null){
        
            if (_porte.keysList.Where(a => a != null).Count() == 0){
                _porte.animPorte.SetBool(_porte.typeAnimation, true);
                _porte.isOpen = true;
                Inventaire.cleTrouve = 0;
                _porte.UIInventaire.compteurCle();
            }
            if(_porte.keysList.Where(a => a != null).Count() > 0){

                if (_porte.keysList.Length > 1){
                    _porte.UIInventaire.afficheInfoText("Il vous faut "+_porte.keysList.Length+" clés");
                }
                else{
                    _porte.UIInventaire.afficheInfoText("Il vous faut "+_porte.keysList.Length+" clé");
                }
            }  
        }

        if (_porte.Switch != null){

            if( _porte.SwitchScript.switchSolIsPressed == false){
                _porte.UIInventaire.afficheInfoText("Trouvez l'interrupteur !");  
            }
            if(_porte.SwitchScript.switchSolIsPressed == true){
                _porte.animPorte.SetBool(_porte.typeAnimation, true);

                if(_porte.OneShot){
                    _porte.UIInventaire.afficheInfoText("Vous avez déverrouillé la Porte !");
                    _porte.OneShot = false; 
                    _porte.isOpen = true;
                }
            
            }
        }
    }







}
