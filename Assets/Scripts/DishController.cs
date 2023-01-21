using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DishController : MonoBehaviour
{
    // Start is called before the first frame update
    public string dishLayer;
    private List<string> list;
    private List<foodDetail> foodDetailDataList;
    void Start()
    {
        dishLayer = "Dish";
        foodDetailDataList = GameObject.Find("FoodList").transform.GetComponent<FoodListManager>().questList[0].foodDetailDataList;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void checkMixture()
    {
        //通过对食材的特殊Id命名，直接计算得合成的物品

        /*for(int i=1;i<transform.childCount;i++)
        {
            GameObject go = transform.GetChild(i).gameObject;
            if(go.GetComponent<FoodManager>())
            {
                list = go.GetComponent<FoodManager>().canMakeWhatDishes;
                return;
            }

        }

        foreach(string foodId in list)
        {
            foreach(foodDetail fd in foodDetailDataList)
            {
                if(fd.foodId==foodId)
                {

                }
            }
        }*/
       
    }
}
