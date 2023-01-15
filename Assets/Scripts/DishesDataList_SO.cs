using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DishesData", menuName = "Inventory/DishesData")]
public class DishesDataList_SO : ScriptableObject
{
    public List<foodDetail> foodDetailDataList;
}
