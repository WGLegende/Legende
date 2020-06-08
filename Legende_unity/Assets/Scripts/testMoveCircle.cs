
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

    float x;
    float z;


    void Start()
    {
        
    }

    void Update(){

        if(Input.GetKeyDown("r")){
            StartCoroutine(CircleAttack());
        }

        if(Input.GetKeyDown("t")){
            StopAllCoroutines();
        }

    }
        


    
    IEnumerator CircleAttack(){


        speedRotation = 1f;
        targetPosition = new Vector3(target.position.x,target.position.y,target.position.z); // on stocke la position intiale

        while(speedRotation < 2.85){

            timercount += Time.deltaTime * speedRotation;

            //x = Mathf.Cos(timercount) * rayonCircle;
            //z = Mathf.Sin(timercount) * rayonCircle;
            // transform.position = new Vector3(targetPosition.x+x,targetPosition.y,targetPosition.z+z);


            transform.RotateAround(targetPosition, Vector3.up, speedRotation * Time.deltaTime * 100);
            speedRotation += Time.deltaTime;
            
            yield return null;
        
        }
    }
}
