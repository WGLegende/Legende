using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VapeurBar : MonoBehaviour
{

    public static VapeurBar instance;
    public Slider slider;
    public Gradient gradient;
    public Image fill;
    public CanvasGroup show_vapeur_bar;

    public float vapeur_stock;

    public float vapeur_stock_max;

    public void Awake(){
        instance = this;
        show_vapeur_bar = GetComponent<CanvasGroup>();
        fill_vapeur_stock();
        StartCoroutine(update_vapeur_UI());
    }
    public void fill_vapeur_stock(){
        vapeur_stock = vapeur_stock_max;
    } 

    public void addVapeur(float amount){
        vapeur_stock += amount;
    } 

    public bool useVapeur(float amount){
        vapeur_stock = vapeur_stock - amount < 0f ? 0f : vapeur_stock - amount;
        return vapeur_stock > 0; //  S'il n'y a plus de vapeur, return false, sinon return true
    } 


    public IEnumerator update_vapeur_UI (){
        yield return new WaitForSeconds(0.1f);
        
        slider.value = vapeur_stock/vapeur_stock_max;
        fill.color = gradient.Evaluate(slider.normalizedValue);

        StartCoroutine(update_vapeur_UI());
    }





}
