
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testMoveCircle : MonoBehaviour
{

    float timercount;
    Vector3 targetPosition;
    public float speedRotation;
    public float rayonCircle;
    public Transform target;
    public GameObject ame_target;
    public GameObject ame_target_back;
    public ParticleSystem particule_ame;

    float x;
    float z;


    void Start(){
        
    }

    void Update(){

        // if(Input.GetKeyDown("r")){
        //     StartCoroutine(CircleAttack());
        // }

        // if(Input.GetKeyDown("t")){
        // StartCoroutine(PlayerInKart(this.gameObject,ame_target.transform.position, 0.5f));
        // particule_ame.Play();
        // }

        // if(Input.GetKeyDown("y")){
        // StartCoroutine(AmeBack(this.gameObject, ame_target_back.transform.position, 0.5f));
        // }

    }
        


    
    IEnumerator CircleAttack(){


        speedRotation = 1f;
        targetPosition = new Vector3(target.position.x, target.position.y, target.position.z); // on stocke la position intiale

        while(speedRotation < 2.85f){

            timercount += Time.deltaTime * speedRotation;

            x = Mathf.Cos(timercount) * rayonCircle;
            z = Mathf.Sin(timercount) * rayonCircle;
            transform.position = new Vector3(targetPosition.x + x, targetPosition.y, targetPosition.z + z);
           

            transform.RotateAround(targetPosition, Vector3.up, speedRotation * Time.deltaTime * 100);
            speedRotation += Time.deltaTime;
            
            yield return null;
        
        }
    }


    IEnumerator PlayerInKart (GameObject objectToMove, Vector3 end, float seconds){

        float elapsedTime = 0;
        Vector3 startingPos = objectToMove.transform.position;

        while (elapsedTime < seconds){
            objectToMove.transform.position = Vector3.Lerp(startingPos, end, (elapsedTime / seconds));
            elapsedTime += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        objectToMove.transform.position = end;
    }

    IEnumerator AmeBack(GameObject objectToMove, Vector3 end, float seconds){

        float elapsedTime = 0;
        Vector3 startingPos = objectToMove.transform.position;

        while (elapsedTime < seconds){
            objectToMove.transform.position = Vector3.Lerp(startingPos, end, (elapsedTime / seconds));
            elapsedTime += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }

        objectToMove.transform.position = end;
        particule_ame.Stop();
        
    }
}
