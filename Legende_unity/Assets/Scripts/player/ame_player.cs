using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.UI;


public class ame_player : MonoBehaviour{

    public static ame_player instance;
    
    CinemachineDollyCart dollyCart;
    public CinemachineDollyCart dollyCartChariot;

    public ParticleSystem ame_particule_kart;
 
    ParticleSystem ame_particule;
  
    Transform playerPosition;
    Transform ame_container;
    public Transform playerKartPosition;
    public Transform ame_container_kart;
    public GameObject playerinKart;

    Vector3 startPosition;
    float distance_parcourue;

    [HideInInspector] public GameObject navy_panel;
    [HideInInspector] public Animator panel_navy_anim;
    [HideInInspector] public Text text_navy_UI;
    [HideInInspector] public Text text_button_navy_UI;
    [HideInInspector] public Animator anim_button_navy;

    [HideInInspector] public string[] text_de_navy_container;
    [HideInInspector] public int id_text;

    bool animTextRunning;
    float speed_anim_text = 0.04f;

    [HideInInspector]public bool navy_en_attente;
    [HideInInspector]public bool navy_speak;



    public string[] text_switch_no_vapeur_elevator = new string[]  {"Cette ascenseur fonctionne à la vapeur.",
                                                                    "Il doit y avoir une vanne quelque part qui permettrait de l'alimenter.",
                                                                    "Allez, trouvons là !"};
    
    public string[] text_bouteille_kart = new string[] {"Il nous manque de puissance, on pourrait aller plus vite avec une bouteille remplie de vapeur."};
  
    
    void Start(){

        if(instance == null){
            instance = this;
        }

        playerPosition = player_main.instance.player.transform;
        ame_container = GameObject.Find("ame_container").GetComponent<Transform>();
        
        dollyCart = GameObject.Find("DollyCart1").GetComponent<CinemachineDollyCart>();
        
        ame_particule = GameObject.Find("ame").GetComponent<ParticleSystem>();

        navy_panel = GameObject.Find("panel_navy");
        panel_navy_anim = navy_panel.GetComponent<Animator>();
        anim_button_navy = GameObject.Find("ButtonNavy").GetComponent<Animator>();
        text_navy_UI = GameObject.Find("text_navy_panel").GetComponent<Text>();
        text_button_navy_UI = GameObject.Find("text_button_navy").GetComponent<Text>();
    }

   
    public IEnumerator navy_want_speak(float delai){

        StartCoroutine("calculDistance");

        if(!navy_en_attente){
            
            navy_en_attente = true;
            yield return new  WaitForSecondsRealtime(delai);

            anim_button_navy.SetBool("display_navy_button",true);
            InvokeRepeating("anim_attente_button",0.5f,5f);

            yield return new  WaitForSecondsRealtime(1f);
          
            GamePad_manager.instance._game_pad_attribution = GamePad_manager.game_pad_attribution.startConversationNavy;
        }

    }

    public void anim_attente_button(){

        anim_button_navy.SetTrigger("anim_button");
        Player_sound.instance.PlayMusicEventPlayer(Player_sound.instance.MusicEventPlayer[5]); 
    }

   
    
    // declenche par player_action
    public IEnumerator navy_start_speak(float delai){ 

        if(!navy_speak){

            navy_speak = true;
               
            anim_button_navy.SetBool("display_navy_button",false);
            navy_en_attente = false;
            CancelInvoke();

            text_navy_UI.text = ""; // on vide le text precedent

            yield return new  WaitForSecondsRealtime(delai);
            player_gamePad_manager.instance.PlayerCanMove(false);
            kart_manager.instance.frein_auto = true;
          
        
            if(playerinKart.activeSelf){ // on check quel player est en jeu
                dollyCartChariot.m_Speed = 15f;
                ame_particule_kart.Play();
            }else{
                dollyCart.m_Speed = 15f; // out navy
                ame_particule.Play(); // out navy
            }

            Player_sound.instance.PlayMusicEventPlayer(Player_sound.instance.MusicEventPlayer[3]); 

            yield return new  WaitForSecondsRealtime(0.5f);

            if(playerinKart.activeSelf){ // on check quel player est en jeu
                Camera_control.instance.cam_ame.Follow = playerKartPosition;
                Camera_control.instance.cam_ame.LookAt = ame_container_kart;
            }else{
                Camera_control.instance.cam_ame.Follow = playerPosition;
                Camera_control.instance.cam_ame.LookAt = ame_container;
            }

            Camera_control.instance.cam_ame.Priority = 12; // on active la cam de navy

            yield return new  WaitForSecondsRealtime(0.5f);
            text_button_navy_UI.text = text_de_navy_container.Length > 1 ? text_button_navy_UI.text = "Passer" : text_button_navy_UI.text = "Terminer";
            panel_navy_anim.SetBool("show_navy_ui",true);

            Time.timeScale = 0.0f;  


            yield return new  WaitForSecondsRealtime(0.5f);
            StartCoroutine(AnimateText());
            GamePad_manager.instance._game_pad_attribution = GamePad_manager.game_pad_attribution.actionNavy;


            yield return new  WaitForSecondsRealtime(0.5f);// on recentre pour retour player en fin de conversation
            Camera_control.instance.CameraBehindPlayer();
        }
    }




    // declenche par gamepad manager bouton A
    public void nextTextNavySpeak(){ 
                
        if(animTextRunning){ // si on reappuie on accelere le text
            //speed_anim_text = 0f;
            allText();
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
            yield return new  WaitForSecondsRealtime(speed_anim_text);
        }
        animTextRunning = false;
        speed_anim_text = 0.04f;
        text_button_navy_UI.text = id_text == text_de_navy_container.Length -1 ? text_button_navy_UI.text = "Terminer" : text_button_navy_UI.text = "Suivant";
    }

    void allText(){

        StopAllCoroutines();
        text_navy_UI.text = "";
        foreach(char c in text_de_navy_container[id_text]){
            text_navy_UI.text += c;   
        }
        text_button_navy_UI.text = id_text == text_de_navy_container.Length -1 ? text_button_navy_UI.text = "Terminer" : text_button_navy_UI.text = "Suivant";
        animTextRunning = false;
    }


    public IEnumerator backNavy(){ 

        if(playerinKart.activeSelf){ // on check quel player est en jeu
            print("playerkart active");
            GamePad_manager.instance._game_pad_attribution = GamePad_manager.game_pad_attribution.kart;  
            ame_particule_kart.Stop(); 
        }else{
            GamePad_manager.instance._game_pad_attribution = GamePad_manager.instance._last_game_pad_attribution;
            ame_particule.Stop();   
        }

      
        Player_sound.instance.PlayMusicEventPlayer(Player_sound.instance.MusicEventPlayer[4]); 
        panel_navy_anim.SetBool("show_navy_ui",false);
        yield return new  WaitForSecondsRealtime(0.5f);
        Camera_control.instance.cam_ame.Priority = 8;
        yield return new  WaitForSecondsRealtime(0.7f);
        player_gamePad_manager.instance.PlayerCanMove(true);
        
        if(playerinKart.activeSelf){
            kart_manager.instance.frein_auto = false;
        }

        Time.timeScale = 1.0f;   
        navy_speak = false;
        
        yield return new  WaitForSecondsRealtime(2f);

        if(playerinKart.activeSelf){
            dollyCartChariot.m_Position = 0f;
            dollyCartChariot.m_Speed = -30f;
        }else{
        
            dollyCart.m_Position = 0f;
            dollyCart.m_Speed = -30f;
        }
    }



    public IEnumerator calculDistance(){

        startPosition = new Vector3(playerPosition.position.x,playerPosition.position.y,playerPosition.position.z); // on stocke la position
        
        while(distance_parcourue < 25f){

            distance_parcourue = Vector3.Distance(playerPosition.position, startPosition);
            yield return new  WaitForSecondsRealtime(0.02f);
        }

        distance_parcourue = 0;
        navy_speak = false;
        navy_en_attente = false;   
        anim_button_navy.SetBool("display_navy_button",false);
        CancelInvoke();
        GamePad_manager.instance._game_pad_attribution = GamePad_manager.game_pad_attribution.player;

    }

}
