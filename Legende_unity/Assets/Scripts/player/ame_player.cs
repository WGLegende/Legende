using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.UI;


public class ame_player : MonoBehaviour
{
public static ame_player instance;
    
   CinemachineDollyCart dollyCart;
   ParticleSystem ame_particule;

   [HideInInspector] public GameObject navy_panel;
   [HideInInspector] public Animator panel_navy_anim;
   [HideInInspector] public Text text_navy_UI;
   [HideInInspector] public Text text_button_navy_UI;

    [HideInInspector] public string[] text_de_navy_container;
    [HideInInspector] public int id_text;
    bool animTextRunning;
    float speed_anim_text = 0.04f;


    public string[] text_switch_no_vapeur_elevator = new string[]  {"Cette ascenseur fonctionne à la vapeur.",
                                                                    "Il doit y avoir une vanne quelque part qui permettrait de l'alimenter.",
                                                                    "Allez, trouvons là !"};
    
    public string[] text_bouteille_kart = new string[] {"Il nous manque de puissance, on pourrait aller plus vite avec une bouteille remplie de vapeur."};
  
    
    void Start(){

        if(instance == null){
            instance = this;
        }
          
        dollyCart = GameObject.Find("DollyCart1").GetComponent<CinemachineDollyCart>();
        ame_particule = GameObject.Find("ame").GetComponent<ParticleSystem>();

        panel_navy_anim = navy_panel.GetComponent<Animator>();
        text_navy_UI = GameObject.Find("text_navy_panel").GetComponent<Text>();
        text_button_navy_UI = GameObject.Find("text_button_navy").GetComponent<Text>();
    }


   

     
    // declenche par player_action
    public IEnumerator navy_start_speak(string[] value,float delai){ // declenche par player_action

        text_de_navy_container = value; // on recupere l'array string envoyee
        text_navy_UI.text = "";

        yield return new WaitForSeconds(delai);
        player_gamePad_manager.instance.PlayerCanMove(false);
        dollyCart.m_Speed = 15f; // out navy
        ame_particule.Play(); // out navy
        Player_sound.instance.PlayMusicEventPlayer(Player_sound.instance.MusicEventPlayer[3]); 

        yield return new WaitForSeconds(0.5f);
        Camera_control.instance.cam_ame.Priority = 12; 

        yield return new WaitForSeconds(0.5f);
        text_button_navy_UI.text = text_de_navy_container.Length > 1 ? text_button_navy_UI.text = "Passer" : text_button_navy_UI.text = "Terminer";
        panel_navy_anim.SetBool("show_navy_ui",true);

        yield return new WaitForSeconds(0.5f);
        StartCoroutine(AnimateText());
        GamePad_manager.instance._game_pad_attribution = GamePad_manager.game_pad_attribution.actionNavy;

        yield return new WaitForSeconds(0.5f);// on recentre pour retour player en fin de conversation
        Camera_control.instance.CameraBehindPlayer();
    }




    // declenche par gamepad manager bouton A
    public void nextTextNavySpeak(){ 

        if(animTextRunning){ // si on reappuie on accelere
           speed_anim_text = 0.01f;
           return;
        }

        id_text++;
        text_navy_UI.text = "";
        text_button_navy_UI.text = id_text == text_de_navy_container.Length ? text_button_navy_UI.text = "Terminer" : text_button_navy_UI.text = "Passer";

        if(id_text < text_de_navy_container.Length){ 
            StartCoroutine(AnimateText());
            GamePad_manager.instance._game_pad_attribution = GamePad_manager.game_pad_attribution.actionNavy;     
        }
        else{
            id_text = 0;
            StartCoroutine(backNavy()); 
        }  
    }


    IEnumerator AnimateText(){

        animTextRunning = true;

        foreach(char c in text_de_navy_container[id_text]){
            text_navy_UI.text += c;
            yield return new WaitForSeconds(speed_anim_text);
        }

        animTextRunning = false;
        speed_anim_text = 0.04f;
        text_button_navy_UI.text = id_text == text_de_navy_container.Length -1 ? text_button_navy_UI.text = "Terminer" : text_button_navy_UI.text = "Suivant";
    }


    public IEnumerator backNavy(){ 

        player_actions.instance.clear_action(true);
        ame_particule.Stop(); 
        panel_navy_anim.SetBool("show_navy_ui",false);
        yield return new WaitForSeconds(0.5f);
        Camera_control.instance.cam_ame.Priority = 8;
        yield return new WaitForSeconds(0.7f);
        player_gamePad_manager.instance.PlayerCanMove(true);   
        yield return new WaitForSeconds(0.5f);
        dollyCart.m_Position = 0f;
        dollyCart.m_Speed = -30f;
        ame_particule.Stop(); 
    }

           
        
    
}
