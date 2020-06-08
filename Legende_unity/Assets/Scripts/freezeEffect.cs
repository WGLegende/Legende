using System.Collections;
using UnityEngine;

public class freezeEffect : MonoBehaviour
{ 
    public static freezeEffect instance;

    [Range(0,1f)]
    public float duration;
    bool is_frozen = false;
    float pending_duration;

    public float slowDownFactor;
    public float slowDownFactorLenght;

    void Start(){

        instance = this;
    }

   
    void Update()
    {
        if(pending_duration > 0 && !is_frozen){

            StartCoroutine(doFreeze()); 
        }

        // Time.timeScale +=  (1f / slowDownFactorLenght) * Time.unscaledDeltaTime;
        // Time.timeScale = Mathf.Clamp(Time.timeScale, 0f,1f);
    }

    public void Freeze(){

        pending_duration = duration;
    }

    IEnumerator doFreeze(){

        is_frozen = true;  
        float original = Time.timeScale;

         yield return new WaitForSecondsRealtime(0.1f);

        Time.timeScale = 0;

        yield return new WaitForSecondsRealtime(duration);

        Time.timeScale = original;
        pending_duration = 0;
        is_frozen = false;
    }



    public void SlowMotion(){

        Time.timeScale = slowDownFactor;
        Time.fixedDeltaTime = Time.timeScale * .02f;
    }
    
}
