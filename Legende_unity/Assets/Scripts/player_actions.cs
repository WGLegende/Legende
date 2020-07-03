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

   
    // On affiche l'action a faire
    public void display_actions(object action_init, Collider collider){

        if(collider.tag == "Player"){
            currently_displayed_action = action_init;

            if(action_init.GetType() == typeof(inventory_loot)){
                ButtonAction.instance.Action("Ramasser"); 
            }

            if(action_init.GetType() == typeof(Porte)){
                ButtonAction.instance.Action("Ouvrir"); 
            }
            
            if(action_init.GetType() == typeof(Coffre)){
                ButtonAction.instance.Action("Ouvrir"); 
            }

            if(action_init.GetType() == typeof(Ascenseur)){
                ButtonAction.instance.Action("Activer"); 
            }

            if(action_init.GetType() == typeof(EnterChariot)){
                ButtonAction.instance.Action("Monter A Bord");   
            }
        }
        
        else if(collider.tag == "PlayerKart"){
            currently_displayed_action = action_init;

            if(action_init.GetType() == typeof(GareKart)){
                ButtonAction.instance.Action("Descendre");
               
            }
        }

        GamePad_manager.instance._game_pad_attribution = GamePad_manager.game_pad_attribution.actionDisplay;
    }


    // on declenche l'action avec bouton A via gamepad manager
    public void do_action(){
       
        if(currently_displayed_action.GetType() == typeof(Porte)){ 
            do_action_porte((Porte)currently_displayed_action);
            clear_action(true);
        } 

        else if(currently_displayed_action.GetType() == typeof(Coffre)){ 
            do_action_coffre((Coffre)currently_displayed_action);
            clear_action(true);
        }

        else if(currently_displayed_action.GetType() == typeof(Ascenseur)){ 
            do_action_activer((Ascenseur)currently_displayed_action);
            clear_action(true);
        }  

        else if(currently_displayed_action.GetType() == typeof(inventory_loot)){ 
            do_action_loot((inventory_loot)currently_displayed_action);
            clear_action(true);
        }

        else if(currently_displayed_action.GetType() == typeof(EnterChariot)){ 
            do_action_enter_kart((EnterChariot)currently_displayed_action);
            clear_action(false);
        }

        else if(currently_displayed_action.GetType() == typeof(GareKart)){ 
            do_action_exit_kart((GareKart)currently_displayed_action);
            clear_action(true);
        } 

       
    }

   
    public void clear_action(bool isPlayer){

        if(isPlayer){
            ButtonAction.instance.Hide(); 
            GamePad_manager.instance._game_pad_attribution = GamePad_manager.game_pad_attribution.player; // TODO attention avec kart
            currently_displayed_action = null; 
        }
        else if(!isPlayer){
            ButtonAction.instance.Hide(); 
            GamePad_manager.instance._game_pad_attribution = GamePad_manager.game_pad_attribution.kart; // TODO attention avec kart
            currently_displayed_action = null; 
        }
    }



    public void do_action_enter_kart(EnterChariot _enter_kart){

        Player_sound.instance.StopMove();

        Camera_control.instance.player_kart_camera.m_XAxis.Value = 0f;// recentre la cam
        Camera_control.instance.player_kart_camera.m_YAxis.Value = 0.5f;
        Camera_control.instance.player_kart_camera.Priority = 11;

        player_gamePad_manager.instance.PlayerCanMove(false);
        _enter_kart.player_foot.SetActive(false); 
        _enter_kart.player_kart.SetActive(true);
    
        _enter_kart.ui_chariot.scaleFactor = 0.8f; // affichage ui kart
        _enter_kart.script_kart_manager.enabled = true; 
        _enter_kart.chariot_siege.transform.localRotation = Quaternion.Euler(270,90,-90); // on recentre le player dans le kart
    
        GamePad_manager.instance._game_pad_attribution = GamePad_manager.game_pad_attribution.kart;    
    }

    public void do_action_exit_kart(GareKart _exit_kart){
   
        _exit_kart.ExitKart();   
    }

    public void do_action_loot(inventory_loot _inventory_loot){

       Debug.Log("Loot ramasse");
    }

    public void do_action_activer(Ascenseur _ascenseur){

       _ascenseur.isPositionUp = _ascenseur.isPositionUp ?  _ascenseur.anim.Play("elevator_down") : _ascenseur.anim.Play("elevator_up");
    }
      


    public void do_action_coffre(Coffre _coffre){

        if(!_coffre.isOpen){

            _coffre.isOpen = true;
          

            if(!_coffre.petit_coffre){

                _coffre.anim.SetTrigger("OpenCoffre");
                player_gamePad_manager.instance.PlayerCanMove(false);
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
