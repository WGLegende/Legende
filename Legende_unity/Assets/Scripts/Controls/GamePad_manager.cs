using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePad_manager : MonoBehaviour
{
    public static GamePad_manager instance;



    float right_stick_x;
    float right_stick_y;
    float left_stick_x;
    float left_stick_y;


    public enum game_pad_attribution{
        player,
        inventory,
        kart
    }
    public game_pad_attribution _game_pad_attribution = game_pad_attribution.player;
    public game_pad_attribution _last_game_pad_attribution = game_pad_attribution.player;

    bool currently_navigate_in_inventory;

    void Start()
    {
        if(instance == null){
            instance = this;
        }
        // inventory_main_structure.instance.StartCoroutine(inventory_main_structure.instance.initialize_inventory());
    }

    public void open_close_inventory(bool is_open){
        Debug.Log("open_close_inventory " + is_open);
        if(is_open){
            if(_game_pad_attribution != game_pad_attribution.inventory){
                _last_game_pad_attribution = _game_pad_attribution;
            }
            _game_pad_attribution = game_pad_attribution.inventory;
        }else{
            _game_pad_attribution = _last_game_pad_attribution;
        }
    }

    void Update()
    {
        if(hinput.anyGamepad.back.justPressed){
            inventory_navigation.instance.navigateInMainMenus(0);
        }

        right_stick_x = hinput.anyGamepad.rightStick.position.x;
        right_stick_y = hinput.anyGamepad.rightStick.position.y;

        left_stick_x = hinput.anyGamepad.leftStick.position.x;
        left_stick_y = hinput.anyGamepad.leftStick.position.y;

        switch(_game_pad_attribution){

            case game_pad_attribution.player :

                player_gamePad_manager.instance.player_is_moving = left_stick_x < 0 || left_stick_x > 0 || left_stick_y < 0 || left_stick_y > 0;
                player_gamePad_manager.instance.camera_is_turning = right_stick_x < 0 || right_stick_x > 0 || right_stick_y < 0 || right_stick_y > 0;

                player_gamePad_manager.instance.player_velocity_calculation();

                // Gestion du mouvement du player
                if(player_gamePad_manager.instance.player_is_moving){ 
                    player_gamePad_manager.instance.player_movement(left_stick_x, left_stick_y);
                }

                // Gestion du mouvement de la camera autour du player
                if(player_gamePad_manager.instance.camera_is_turning){ 
                    player_gamePad_manager.instance.player_camera(right_stick_x, right_stick_y);
                }

                // Met la camera derriere le joueur
                if(hinput.anyGamepad.leftTrigger.justPressed){
                    player_gamePad_manager.instance.put_camera_behind_player();
                }

                // Gestion du saut du player
                if(hinput.anyGamepad.Y.justPressed){
                    player_gamePad_manager.instance.player_jump();
                }

                // Gestion du saut du player
                if(hinput.anyGamepad.B.justPressed){
                    player_gamePad_manager.instance.player_attack();
                }

                // Utilise des shortcuts
                if(hinput.anyGamepad.dPad.up.justPressed){
                    inventory_shortcuts.instance.use_shortcut(0);
                }else if(hinput.anyGamepad.dPad.right.justPressed){
                    inventory_shortcuts.instance.use_shortcut(1);
                }else if(hinput.anyGamepad.dPad.down.justPressed){
                    inventory_shortcuts.instance.use_shortcut(2);
                }else if(hinput.anyGamepad.dPad.left.justPressed){
                    inventory_shortcuts.instance.use_shortcut(3);
                }


            break;
            case game_pad_attribution.inventory :
                if(hinput.anyGamepad.A.justPressed){
                    Debug.Log("Test A");
                }

                // Navigue dans les menus principaux
                if(hinput.anyGamepad.leftTrigger.justPressed){
                    inventory_navigation.instance.navigateInMainMenus(-1);
                }else if(hinput.anyGamepad.rightTrigger.justPressed){
                    inventory_navigation.instance.navigateInMainMenus(1);
                }

                // Navigue dans les slots
                if(!currently_navigate_in_inventory){
                    if(hinput.anyGamepad.leftStick.up){
                        StartCoroutine(navigate_in_inventory("up"));
                    }else if(hinput.anyGamepad.leftStick.right){
                        StartCoroutine(navigate_in_inventory("right"));
                    }else if(hinput.anyGamepad.leftStick.down){
                        StartCoroutine(navigate_in_inventory("down"));
                    }else if(hinput.anyGamepad.leftStick.left){
                        StartCoroutine(navigate_in_inventory("left"));
                    } 
                }

                // Creer des shortcuts
                // if(hinput.anyGamepad.dPad.up.justPressed){
                //     inventory_navigation.instance.go_to_shortcut(0);
                // }else if(hinput.anyGamepad.dPad.right.justPressed){
                //     inventory_navigation.instance.go_to_shortcut(1);
                // }else if(hinput.anyGamepad.dPad.down.justPressed){
                //     inventory_navigation.instance.go_to_shortcut(2);
                // }else if(hinput.anyGamepad.dPad.left.justPressed){
                //     inventory_navigation.instance.go_to_shortcut(3);
                // }


                // Actions sur les objets
                if(hinput.anyGamepad.A.justPressed){
                    inventory_navigation.instance.action_A();
                }
                if(hinput.anyGamepad.Y.justPressed){
                    inventory_navigation.instance.action_Y();
                }
                if(hinput.anyGamepad.B.justPressed){
                    inventory_navigation.instance.action_Back();
                }


            break;
            case game_pad_attribution.kart :
                    // Gere le mouvement de camera et la rotation du siege
                    if(right_stick_x != 0 || right_stick_y != 0){ 
                        kart_manager.instance.kart_movement(right_stick_x, right_stick_y, left_stick_x, left_stick_y);
                    }

                    // GERE LE FREINAGE DU VEHICULE
                    kart_manager.instance.frein(hinput.anyGamepad.leftTrigger.pressed);

                    // Gestion de la vitesse basique avec le joystick
                    kart_manager.instance.calcul_vitesse_basique(left_stick_y);

                    // Gestion du boost // Fonctionne seulement s'il y a encore de la vapeur
                    kart_manager.instance.boost(hinput.anyGamepad.rightTrigger.pressed);

                    // Gestion du saut du kart
                    if(hinput.anyGamepad.Y.justPressed){
                        kart_manager.instance.kart_jump();
                    }

                    // Gestion attaque du kart
                    if(hinput.anyGamepad.B.justPressed){
                        kart_manager.instance.kart_attaque();
                    }

                    // Allume lumiere du kart
                    if(hinput.anyGamepad.X.justPressed){
                        kart_manager.instance.kart_light();
                    }
            break;

        }

    }


    IEnumerator navigate_in_inventory(string direction){
        currently_navigate_in_inventory = true;
        inventory_navigation.instance.navigate_in_slots(direction);
        yield return new WaitForSeconds(0.2f);
        currently_navigate_in_inventory = false;
    }

}
