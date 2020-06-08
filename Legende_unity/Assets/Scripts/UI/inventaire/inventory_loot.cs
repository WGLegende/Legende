using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class inventory_loot : MonoBehaviour
{
    [Serializable]
    public struct RandomLoot{
        public GameObject loot_object;
        public int loot_percentage;
    };
    public RandomLoot[] _RandomLoot;






}
