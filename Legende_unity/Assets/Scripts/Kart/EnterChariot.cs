using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnterChariot : MonoBehaviour
{
  public static EnterChariot instance;

  public GameObject player_foot;
  public GameObject player_kart;
  public Transform chariot_siege;
 
  [HideInInspector] public kart_manager script_kart_manager;
  [HideInInspector] public CanvasScaler ui_chariot;
  
  
  void Start(){

    instance = this;
    ui_chariot = GameObject.Find("UI_Chariot").GetComponent<CanvasScaler>();
    script_kart_manager = GetComponentInChildren<kart_manager>(); 
    player_foot = GameObject.Find("Player");
   
  }


  void OnTriggerEnter(Collider collider){ 

    if(collider.gameObject.tag == "Player"){
      player_actions.instance.display_actions(this,collider); 
    }
  }

  
  void OnTriggerExit(Collider collider) {  

    if(collider.gameObject.tag == "Player"){
      player_actions.instance.clear_action(collider.tag == "Player");  
    }
  } 

    //  IEnumerator PlayerInKart (GameObject objectToMove, Vector3 end, float seconds){

    //   player_gamePad_manager.instance.player_jump();
    //   yield return new WaitForSeconds(0.2f);
    //   float elapsedTime = 0;
    //   Vector3 startingPos = objectToMove.transform.position;

    //   while (elapsedTime < seconds){

    //     objectToMove.transform.position = Vector3.Lerp(startingPos, end, (elapsedTime / seconds));
    //     elapsedTime += Time.deltaTime;
    //     yield return new WaitForEndOfFrame();
    //   }
    //   //objectToMove.transform.position = end;
    //   EnterKart();
    // }




    // IEnumerator PlayerOutKart (GameObject objectToMove, Vector3 end, float seconds){

    //   camKart.Priority = 9;
    //   player_gamePad_manager.instance.canJump = true;
    //   GamePad_manager.instance._game_pad_attribution = GamePad_manager.game_pad_attribution.player; 

    //   player_foot.SetActive(true);
    //   player_kart.SetActive(false);
    //   player_gamePad_manager.instance.player_jump();

    //   yield return new WaitForSeconds(0.15f);


    //   float elapsedTime = 0;
    //   Vector3 startingPos = objectToMove.transform.position;

    //   while (elapsedTime < seconds){

    //    // objectToMove.transform.position = Vector3.Lerp(startingPos, end, (elapsedTime / seconds));
    //     objectToMove.transform.Translate(Vector3.back * Time.deltaTime * 8, Space.World);

    //     elapsedTime += Time.deltaTime;
    //     yield return new WaitForEndOfFrame();
    //   }
    //  // objectToMove.transform.position = end;
    //   ExitKart();
    // }
}
