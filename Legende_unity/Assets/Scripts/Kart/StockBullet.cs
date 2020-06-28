using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StockBullet : MonoBehaviour
{
    public static StockBullet instance;
    public int bullet_stock;
    public int bullet_stock_max;

    public Text TX_stock_bullet;
    public Text TX_stock_bullet_max;


    void Start()
    {
        instance = this;
        TX_stock_bullet.text =  bullet_stock.ToString();
        TX_stock_bullet_max.text =  bullet_stock_max.ToString();
    }

   
    void Update()
    {
        
    }

    public void update_stock_bullet(int value){

        bullet_stock += value;
        bullet_stock = Mathf.Clamp(bullet_stock, 0, bullet_stock_max);
        TX_stock_bullet.text =  bullet_stock.ToString();
    }

     public void update_stock_bullet_max(int value){ // Si Besoin pour augmenter la quantite max

        bullet_stock_max += value;
        TX_stock_bullet_max.text =  bullet_stock_max.ToString();
    }
}
