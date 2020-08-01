using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//[ExecuteInEditMode]

public class script_test : MonoBehaviour{

    [SerializeField]
    public Transform start;
    [SerializeField]
    public Transform end;
    public Transform other;
    [SerializeField]
   
    public float vitessekart = 10f;

    public float speed = 0;


    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       
        // transform.position = Vector3.Lerp(start.position,end.position,test);
        // transform.rotation = Quaternion.Lerp(start.rotation,end.rotation,test);
       // speed = Mathf.Lerp(speed,vitessekart,Time.deltaTime);
         speed += speed < vitessekart ? 
                                    Time.deltaTime* 5 : 
                                   -(Time.deltaTime* 5);
        
     
        
    }
}
