using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class player_actions : MonoBehaviour
{
    public static player_actions instance;
    public object currently_displayed_action;
    public Animator animPlayer;

    void Start(){ 
        
        if(instance == null){
            instance = this;
        }   
        animPlayer = GameObject.Find("Player").GetComponent<Animator>();
    }

   
    // On affiche l'action a faire
    public void display_actions(object action_init, Collider collider){

        if(collider.tag == "Player"){
            currently_displayed_action = action_init;

            if(action_init.GetType() == typeof(inventory_loot)){
                ButtonAction.instance.Action("Ramasser"); 
            }

             if(action_init.GetType() == typeof(inventory_object)){
                ButtonAction.instance.Action("Prendre"); 
            }


            if(action_init.GetType() == typeof(Porte)){
                ButtonAction.instance.Action("Ouvrir"); 
            }
            
            if(action_init.GetType() == typeof(Coffre)){
                ButtonAction.instance.Action("Ouvrir"); 
            }

            if(action_init.GetType() == typeof(AscenseurSwitch)){
                ButtonAction.instance.Action("Activer"); 
            }

             if(action_init.GetType() == typeof(Switch_vanne)){
                ButtonAction.instance.Action("Activer"); 
            }

             if(action_init.GetType() == typeof(RotateKartSwitch)){
                ButtonAction.instance.Action("Tourner le Kart"); 
            }

            if(action_init.GetType() == typeof(EnterChariot)){
                ButtonAction.instance.Action("Monter A Bord");   
            }

            GamePad_manager.instance._game_pad_attribution = GamePad_manager.game_pad_attribution.actionDisplay;

        }
        
        else if(collider.tag == "PlayerKart"){

            currently_displayed_action = action_init;

            if(action_init.GetType() == typeof(GareKart)){
                ButtonActionKart.instance.Action("Descendre");  
            }
            if(action_init.GetType() == typeof(aiguillage_kart)){
                ButtonActionKart.instance.Action("Bifurquer");  
            }

            GamePad_manager.instance._game_pad_attribution = GamePad_manager.game_pad_attribution.actionDisplayKart;
        }

    }


    // on declenche l'action avec bouton A via gamepad manager pour le player
    public void do_action(){
       
        if(currently_displayed_action.GetType() == typeof(Porte)){ 
            do_action_porte((Porte)currently_displayed_action);
            clear_action(true);
        } 

        else if(currently_displayed_action.GetType() == typeof(Coffre)){
            do_action_coffre((Coffre)currently_displayed_action);
            clear_action(true);
        }

        else if(currently_displayed_action.GetType() == typeof(AscenseurSwitch)){ 
            do_action_ascenseur((AscenseurSwitch)currently_displayed_action);
            clear_action(true);
        } 

        else if(currently_displayed_action.GetType() == typeof(Switch_vanne)){ 
            do_action_vanne((Switch_vanne)currently_displayed_action);
            clear_action(true);
        } 

         else if(currently_displayed_action.GetType() == typeof(RotateKartSwitch)){ 
            do_action_turn_kart((RotateKartSwitch)currently_displayed_action);
            clear_action(true);
        }   

        else if(currently_displayed_action.GetType() == typeof(inventory_loot)){
            do_action_loot((inventory_loot)currently_displayed_action);
            clear_action(true);
        }

        else if(currently_displayed_action.GetType() == typeof(inventory_object)){ 
            StartCoroutine(do_action_objet((inventory_object)currently_displayed_action));
            clear_action(true);
        }

        else if(currently_displayed_action.GetType() == typeof(EnterChariot)){ 
            do_action_enter_kart((EnterChariot)currently_displayed_action);
            clear_action(false);
        }

       
    }


    // on declenche l'action avec bouton A via gamepad manager pour le kart
    public void do_action_kart(){

        if(currently_displayed_action.GetType() == typeof(aiguillage_kart)){ 
            do_action_switch_rails((aiguillage_kart)currently_displayed_action);
        } 

        if(currently_displayed_action.GetType() == typeof(GareKart)){ 
            do_action_exit_kart((GareKart)currently_displayed_action);
            clear_action_kart(false);
        } 
    }

   

    public void clear_action(bool isPlayer){

        if(isPlayer){
            ButtonAction.instance.Hide(); 
            GamePad_manager.instance._game_pad_attribution = GamePad_manager.game_pad_attribution.player; // TODO attention avec kart
            currently_displayed_action = null; 
        }
        else if(!isPlayer){
            GamePad_manager.instance._game_pad_attribution = GamePad_manager.game_pad_attribution.kart;
            currently_displayed_action = null; 
        }
    }

    public void clear_action_kart(bool isPlayerKart){

        if(isPlayerKart){
            ButtonActionKart.instance.Hide(); 
            GamePad_manager.instance._game_pad_attribution = GamePad_manager.game_pad_attribution.kart; 
            currently_displayed_action = null; 
        }
         if(!isPlayerKart){ // pour sortie de gare
            GamePad_manager.instance._game_pad_attribution = GamePad_manager.game_pad_attribution.player; 
            currently_displayed_action = null; 
        }
    }



    public void do_action_enter_kart(EnterChariot _enter_kart){

        Player_sound.instance.StopMove();

        Camera_control.instance.player_kart_camera.m_XAxis.Value = 0f;// recentre la cam
        Camera_control.instance.player_kart_camera.m_YAxis.Value = 0.3f;
        Camera_control.instance.player_kart_camera.Priority = 11;

        player_gamePad_manager.instance.PlayerCanMove(false);

        _enter_kart.chariot_siege.transform.localRotation = Quaternion.Euler(270,90,-90); // on recentre le player dans le kart
        player_main.instance.player.SetActive(false);
        player_main.instance.playerKart.SetActive(true);
       
        kart_manager.instance.frein_auto = false;
    
        _enter_kart.ui_chariot.scaleFactor = 0.8f; // affichage ui kart todo
    
        GamePad_manager.instance._game_pad_attribution = GamePad_manager.game_pad_attribution.kart;   
        _enter_kart.script_kart_manager.SplineFollow.IsRunning = true; 
    }



    public void do_action_exit_kart(GareKart _gare_kart){

        Camera_control.instance.CameraBehindPlayer();
        Camera_control.instance.player_kart_camera.Priority = 9;
       
        kart_manager.instance.SplineFollow.IsRunning = false;

        kart_manager.instance.particle_vapeur_front.Stop();
        kart_manager.instance.particle_vapeur_back.Stop();
        kart_manager.instance.audio_vapeur.Stop();
        Player_sound.instance.StopKart();// Gestion du son rails

        Vector3 rotationPlayer = new Vector3(0,player_main.instance.playerKart.transform.eulerAngles.y,0); // on le tourne dans le meme sens que player_kart
        player_main.instance.player.transform.rotation = Quaternion.Euler(rotationPlayer);
        player_main.instance.player.transform.localPosition = _gare_kart.chariot_container.transform.position;
        player_main.instance.playerKart.SetActive(false);
        player_main.instance.player.SetActive(true);

        GamePad_manager.instance._game_pad_attribution = GamePad_manager.game_pad_attribution.player; 


        _gare_kart.ui_chariot.scaleFactor = 0f; //todo hide ui kart

        player_gamePad_manager.instance.PlayerCanMove(true);
        player_gamePad_manager.instance.changeEquipement(); // maj de l'animator

        if(!kart_manager.instance.equipement_bouteille){
            ButtonAction.instance.Hide();
            ame_player.instance.StartCoroutine(ame_player.instance.navy_start_speak(ame_player.instance.text_bouteille_kart,3f));
        }
    }



    
    // Logique aiguillage
    public void do_action_switch_rails(aiguillage_kart _aiguillage_kart){
   
        _aiguillage_kart.toggle = ! _aiguillage_kart.toggle;
        _aiguillage_kart.aiguillage_audio.Play();

            if(_aiguillage_kart.toggle){
                _aiguillage_kart._choix_circuit = aiguillage_kart.ChoixCircuit.Gauche;
                AiguillageManager.instance.next_rails = _aiguillage_kart.left_rails;
                _aiguillage_kart.anim.SetBool("switch",true);
                _aiguillage_kart.switchLeft();
            }
            else{
                _aiguillage_kart._choix_circuit =  aiguillage_kart.ChoixCircuit.Droite;
                AiguillageManager.instance.next_rails = _aiguillage_kart.right_rails;
                _aiguillage_kart.anim.SetBool("switch",false);
                _aiguillage_kart.switchRight();
            }  
    }


    // Logique Loot
    public void do_action_loot(inventory_loot _inventory_loot){
       Debug.Log("Loot ramasse");
    }

   

    // Logique pick up objet
    public IEnumerator do_action_objet(inventory_object _inventory_loot){

        animPlayer.SetTrigger("pick_up");

        yield return new WaitForSeconds(0.4f);

        Debug.Log("add " + _inventory_loot.nom + " to inventory");
        Player_sound.instance.PlayMusicEventPlayer(Player_sound.instance.MusicEventPlayer[1]); 
        _inventory_loot.addObject();

        if(_inventory_loot.nom == "Bouteille"){
            kart_manager.instance.equipement_bouteille = true;
            Debug.Log("Equipement kart : bouteille");
        };
    }





    // Logique Ascenseur
    public void do_action_ascenseur(AscenseurSwitch _ascenseur_switch){

        _ascenseur_switch.sound_levier.Play();

        if(!_ascenseur_switch.elevator_script.has_vapeur){
            _ascenseur_switch.anim_levier.SetTrigger("active_no_vapeur");
            ame_player.instance.StartCoroutine(ame_player.instance.navy_start_speak(ame_player.instance.text_switch_no_vapeur_elevator,1.4f));
            return;
        }

        _ascenseur_switch.toggle_levier = !_ascenseur_switch.toggle_levier;
        _ascenseur_switch.anim_levier.SetBool("active_levier",_ascenseur_switch.toggle_levier);
       
        if(_ascenseur_switch.elevator_script.isPositionUp){
            _ascenseur_switch.anim_elevator.SetBool("position_up",false);   
        }
        else{
            _ascenseur_switch.anim_elevator.SetBool("position_up",true);
        } 
        
    }

     // Logique Vanne
    public void do_action_vanne(Switch_vanne _switch_vanne){

        _switch_vanne.sound_levier.Play();
        _switch_vanne.anim_levier.SetBool("active_levier",true);
        _switch_vanne.anim_vanne.SetTrigger("vanne_closed"); 
        _switch_vanne.elevator_script.has_vapeur = true;
        _switch_vanne.oneShot = true;     
    }


     // Logique turn Kart
    public void do_action_turn_kart(RotateKartSwitch _rotate_kart){

        _rotate_kart.toggle_levier = !_rotate_kart.toggle_levier;
        _rotate_kart.anim_levier.SetBool("active_levier",_rotate_kart.toggle_levier);
        _rotate_kart.sound.Play();
        kart_manager.instance.collider_enter_chariot.enabled = false;

        if(!kart_manager.instance.kart_is_reverse){
            _rotate_kart.anim_kart.SetBool("rotationKart",true);
            kart_manager.instance.reverse_pad = -1f;
            kart_manager.instance.kart_is_reverse = true;
        }
        else{
            _rotate_kart.anim_kart.SetBool("rotationKart",false);
            kart_manager.instance.reverse_pad = 1f;
            kart_manager.instance.kart_is_reverse = false;
        } 
    }
      




    // Logique Coffre
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
                _coffre.sound.clip = _coffre.audio_clip[Random.Range(0,_coffre.audio_clip.Length)];
                _coffre.sound.Play();
                _coffre.Invoke("activeObject",0.5f);
            }
        }
    }




    // Logique Porte
    public void do_action_porte(Porte _porte){

        if (_porte.Switch == null){
        
            if (_porte.keysList.Where(a => a != null).Count() == 0){
                _porte.animPorte.SetBool(_porte.typeAnimation, true);
                _porte.soundFx.Play(0);
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
