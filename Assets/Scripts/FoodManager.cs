using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodManager : MonoBehaviour
{
    private GameObject foodList;

    public string foodId;
    public string foodName;
    public string foodLayer;
    public Sprite foodImage;
    public string foodIntroduction;
    public bool stackable;//食物是否可叠加
    public int stackNum;//叠加个数
    public LayerMask place;//可以放在什么地方
    public bool finshCooking;//是否完成烹饪
    public bool serveCorrectly;//是否正确
    public List<string> canMakeWhatDishes;//可以做成什么菜
    public List<string> canBeMakeByWhat;//由什么合成
    // Start is called before the first frame update
    void Start()
    {
        foodList = GameObject.Find("FoodList");
        foodName = transform.name;
        List<foodDetail> fdList= foodList.GetComponent<FoodListManager>().questList[0].foodDetailDataList;
        foreach(foodDetail fd in fdList)
        {
            if(foodName==fd.foodName)
            {
                foodId = fd.foodId;
                foodLayer = fd.foodLayer;
                canMakeWhatDishes = fd.canMakeWhatDishes;
                canBeMakeByWhat = fd.canBeMakeByWhat;
                stackable = fd.stackable;
            }
        }


    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
