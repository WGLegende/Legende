using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fluent;

public class GamePad_manager : MonoBehaviour
{
    public static GamePad_manager instance;

    float right_stick_x;
    float right_stick_y;
    float left_stick_x;
    float left_stick_y;
    float left_trigger;

    public enum game_pad_attribution{
        player,
        inventory,
        kart,
        dialogue,
        actionDisplay,
        actionDisplayKart,
        startConversationNavy,
        actionNavy,
        nothing
    }

    public enum_manager.game_pad_attribution _game_pad_attribution = enum_manager.game_pad_attribution.player;
    public enum_manager.game_pad_attribution _last_game_pad_attribution = enum_manager.game_pad_attribution.player;

    bool currently_navigate_in_inventory;

    bool gameIsPaused;
    GameObject interaction;

    void Start()
    {
        if(instance == null){
            instance = this;
        }
    }

    public bool keyBoardTouchPressed(string touch){
        return Input.GetKeyDown(touch);
    }

    public void open_close_inventory(bool is_open){
        if(is_open){
            if(_game_pad_attribution != enum_manager.game_pad_attribution.inventory){
                _last_game_pad_attribution = _game_pad_attribution;
                Time.timeScale = 0; // gamePaused
                Player_sound.instance.PlayMusicEventPlayer(Player_sound.instance.Inventory[1]); 

            }
            _game_pad_attribution = enum_manager.game_pad_attribution.inventory;
        }else{
            _game_pad_attribution = _last_game_pad_attribution;
            Time.timeScale = 1; // Back to game
             
        }
    }


    void Update()
    {
        if(Hinput.anyGamepad.back.justPressed || keyBoardTouchPressed("i")){
            inventory_navigation.instance.navigateInMainMenus(0);
        }

        right_stick_x = Hinput.anyGamepad.rightStick.position.x;
        right_stick_y = Hinput.anyGamepad.rightStick.position.y;

        left_stick_x = Hinput.anyGamepad.leftStick.position.x;
        left_stick_y = Hinput.anyGamepad.leftStick.position.y;

        left_trigger = Hinput.anyGamepad.leftTrigger.position;

        if(Input.GetKey("w")){    left_stick_y = 1f;  }
        if(Input.GetKey("s")){    left_stick_y = -1f; }
        if(Input.GetKey("d")){    left_stick_x = 1f;  }
        if(Input.GetKey("a")){    left_stick_x = -1f; }
        
        switch(_game_pad_attribution){

            case enum_manager.game_pad_attribution.player : 
            case enum_manager.game_pad_attribution.actionDisplay : 
            case enum_manager.game_pad_attribution.actionNavy : 
            case enum_manager.game_pad_attribution.startConversationNavy :

                player_gamePad_manager.instance.player_is_moving = left_stick_x < 0 || left_stick_x > 0 || left_stick_y < 0 || left_stick_y > 0;
              
                player_gamePad_manager.instance.player_velocity_calculation();

                // Gestion du mouvement du player
                if(player_gamePad_manager.instance.player_is_moving){ 
                    player_gamePad_manager.instance.player_movement(left_stick_x, left_stick_y);
                }

                // Met la camera derriere le joueur
               if(Hinput.anyGamepad.leftTrigger.justPressed || Input.GetMouseButtonDown(2)){
                   player_gamePad_manager.instance.put_camera_behind_player();
                   lockTarget.instance.detection_collider.enabled = true;
                }

                // lock Target
                if(Hinput.anyGamepad.leftTrigger.pressed || Input.GetKeyDown(KeyCode.LeftControl)){
                    lockTarget.instance.PlayerFaceToTarget();
                    lockTarget.instance.ChangeTarget();
                }

                //Unlock Target
                if(Hinput.anyGamepad.leftTrigger.released || Input.GetKeyUp(KeyCode.LeftControl)){
                    lockTarget.instance.EndTargetLock();
                }

                // Gestion du saut du player
                if(Hinput.anyGamepad.Y.justPressed || Input.GetKeyDown("space")){
                    player_gamePad_manager.instance.player_jump();
                }

                // Attack Player
                if(Hinput.anyGamepad.B.justPressed || Input.GetMouseButtonDown(1)){ 
                    player_gamePad_manager.instance.player_attack();
                }
                // Position Bow
                if(Hinput.anyGamepad.rightTrigger.justPressed || Input.GetKeyDown("1")){ 
                    player_gamePad_manager.instance.position_bowman(true);
                }
                // fin position
                if(Hinput.anyGamepad.rightTrigger.justReleased || Input.GetKeyUp("1")){ 
                    player_gamePad_manager.instance.position_bowman(false);
                }
               

                if(Hinput.anyGamepad.A.justPressed || Input.GetKeyDown("q")){

                    if(ame_player.instance.navy_en_attente){
                        ame_player.instance.navy_en_attente =false;
                        ame_player.instance.anim_button_navy.SetBool("display_navy_button",false);
                        ame_player.instance.CancelInvoke();
                    }

                    if(_game_pad_attribution == enum_manager.game_pad_attribution.actionDisplay){
                        player_actions.instance.do_action();
                    }

                    else if(_game_pad_attribution == enum_manager.game_pad_attribution.startConversationNavy){
                       StartCoroutine(ame_player.instance.navy_start_speak((0f)));
                    }

                    else if(_game_pad_attribution == enum_manager.game_pad_attribution.actionNavy){
                        ame_player.instance.nextTextNavySpeak();
                    }   
                }

                
                // Utilise des shortcuts
                if(Hinput.anyGamepad.dPad.up.justPressed || Input.GetKeyDown("up")){
                    inventory_shortcuts.instance.use_shortcut(0);
                }else if(Hinput.anyGamepad.dPad.right.justPressed || Input.GetKeyDown("right")){
                    inventory_shortcuts.instance.use_shortcut(1);
                    StartCoroutine(player_equipement.instance.equip_player_arc()); // pour test todo
                }else if(Hinput.anyGamepad.dPad.down.justPressed || Input.GetKeyDown("down")){
                    inventory_shortcuts.instance.use_shortcut(2);
                    StartCoroutine(player_equipement.instance.equip_player_noweapon()); // pour test todo
                }else if(Hinput.anyGamepad.dPad.left.justPressed || Input.GetKeyDown("left")){
                    inventory_shortcuts.instance.use_shortcut(3);
                    StartCoroutine(player_equipement.instance.equip_player_cac()); // pour test todo
                }

            break;


            case enum_manager.game_pad_attribution.inventory :

                // Navigue dans les menus principaux
                if(Hinput.anyGamepad.leftTrigger.justPressed || Input.GetKeyDown("delete")){
                    inventory_navigation.instance.navigateInMainMenus(-1);
                }else if(Hinput.anyGamepad.rightTrigger.justPressed || Input.GetKeyDown("end")){
                    inventory_navigation.instance.navigateInMainMenus(1);
                }

                // Navigue dans les slots
                if(!currently_navigate_in_inventory){

                    if(Hinput.anyGamepad.leftStick.up || Input.GetKeyDown(KeyCode.W)){
                        StartCoroutine(navigate_in_inventory("up"));
                        Player_sound.instance.PlayMusicEventPlayer(Player_sound.instance.Inventory[0]); 

                    }else if(Hinput.anyGamepad.leftStick.right || Input.GetKeyDown(KeyCode.D)){
                        StartCoroutine(navigate_in_inventory("right"));
                        Player_sound.instance.PlayMusicEventPlayer(Player_sound.instance.Inventory[0]); 

                    }else if(Hinput.anyGamepad.leftStick.down || Input.GetKeyDown(KeyCode.S)){
                        StartCoroutine(navigate_in_inventory("down"));
                        Player_sound.instance.PlayMusicEventPlayer(Player_sound.instance.Inventory[0]); 

                    }else if(Hinput.anyGamepad.leftStick.left || Input.GetKeyDown(KeyCode.A)){
                        StartCoroutine(navigate_in_inventory("left"));
                        Player_sound.instance.PlayMusicEventPlayer(Player_sound.instance.Inventory[0]); 

                    } 
                }

                // Creer des shortcuts
                if(Hinput.anyGamepad.dPad.up.justPressed || Input.GetKey("up")){
                    inventory_navigation.instance.go_to_shortcut(0);
                }else if(Hinput.anyGamepad.dPad.right.justPressed || Input.GetKey("right")){
                    inventory_navigation.instance.go_to_shortcut(1);
                }else if(Hinput.anyGamepad.dPad.down.justPressed || Input.GetKey("down")){
                    inventory_navigation.instance.go_to_shortcut(2);
                }else if(Hinput.anyGamepad.dPad.left.justPressed || Input.GetKey("left")){
                    inventory_navigation.instance.go_to_shortcut(3);
                }


                // Actions sur les objets
                if(Hinput.anyGamepad.A.justPressed || Input.GetKeyDown("q")){
                    inventory_navigation.instance.action_A();
                }
                if(Hinput.anyGamepad.Y.justPressed || Input.GetKeyDown("y")){
                    inventory_navigation.instance.action_Y();
                }
                if(Hinput.anyGamepad.B.justPressed || Input.GetKeyDown("escape")){
                    inventory_navigation.instance.action_Back();
                }
            break;

            case enum_manager.game_pad_attribution.kart :
            case enum_manager.game_pad_attribution.actionDisplayKart :
            
                // Gere le mouvement de camera et la rotation du siege
                if(right_stick_x != 0 || right_stick_y != 0){ 
                    kart_manager.instance.kart_movement(right_stick_x, right_stick_y, left_stick_x, left_stick_y);
                }

                // GERE LE FREINAGE DU VEHICULE
                if(Hinput.anyGamepad.B.pressed || Input.GetKeyDown("s"))
                kart_manager.instance.frein(true);

                if(Hinput.anyGamepad.B.released || Input.GetKeyUp("s"))
                kart_manager.instance.frein(false);

                // Gestion de la vitesse basique avec le joystick
                kart_manager.instance.calcul_vitesse_basique(left_stick_y);

                // Gestion Hauteur Kart
                kart_manager.instance.up_kart(left_trigger);

                // Gestion du boost // Fonctionne seulement s'il y a encore de la vapeur
                if(left_stick_y != 0 && (Hinput.anyGamepad.rightTrigger.pressed || Input.GetKey("q"))){
                    kart_manager.instance.boost(true);
                }
                if(left_stick_y == 0 || (Hinput.anyGamepad.rightTrigger.released || !Input.GetKey("q"))){
                    kart_manager.instance.boost(false);
                }

                // Gestion attaque du kart
                if(Hinput.anyGamepad.Y.justPressed || Input.GetKey("e")){ 
                    kart_manager.instance.kart_attaque();
                }

                // Allume lumiere du kart
                if(Hinput.anyGamepad.X.justPressed || Input.GetKey("r")){ 
                    kart_manager.instance.kart_light();
                }

                if(Hinput.anyGamepad.A.justPressed || Input.GetKeyDown("z")){
                    if(_game_pad_attribution == enum_manager.game_pad_attribution.actionDisplayKart){
                        player_actions.instance.do_action_kart();
                    }  
                }

            break;


            case enum_manager.game_pad_attribution.dialogue :
            break;

        }

    }

    public void gamePad_setVibration(bool setVibrate, float qty = 0.4f, bool VibrateAdvanced = false){
        if(setVibrate){
            if(VibrateAdvanced){

            }else{
                Hinput.gamepad[0].Vibrate(qty);
            }
        }else{
            Hinput.gamepad[0].StopVibration();
        }
    }

    IEnumerator navigate_in_inventory(string direction){

        currently_navigate_in_inventory = true;
        inventory_navigation.instance.navigate_in_slots(direction);
        yield return new WaitForSecondsRealtime(0.2f);
        currently_navigate_in_inventory = false;
    }


}
