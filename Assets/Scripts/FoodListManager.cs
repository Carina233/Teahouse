using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodListManager : MonoBehaviour
{
    private static FoodListManager instance;
   
    public static FoodListManager Instance
    {
        get
        {
            if (instance == null || !instance.gameObject)
                instance = FindObjectOfType<FoodListManager>();
            return instance;
        }
    }
    [SerializeField]
    public List<DishesDataList_SO> questList;
}
