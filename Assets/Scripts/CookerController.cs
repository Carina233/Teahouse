using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CookerController : MonoBehaviour
{
    public Slider cookingSlider;
    public CookerFoodState cookerFoodState;
    public bool readyCooking=false;//未收到开火通知
    public bool cooking = false;

    /// <summary>
    /// 没煮过的食物需要的总时间
    /// </summary>
    private int foodNeedTime;

    /// <summary>
    /// 现在食物还需要烹饪的时间
    /// </summary>
    private int needCookingTime;

    // Start is called before the first frame update
    void Start()
    {
        //初始化进度条
        cookingSlider.transform.GetComponent<CookingSlider>().setMaxValue(0);
        cookingSlider.transform.GetComponent<Slider>().value = 0;

        cookerFoodState = CookerFoodState.none;
    }

    // Update is called once per frame
    void Update()
    {

        checkCookerTable();

        if(readyCooking==true&&cooking==false)
        {
            //从没被煮过的，初始化
            if(cookingSlider.transform.GetComponent<Slider>().maxValue== 0)
            {
                Debug.Log("readyCooking==true&&cooking==false");
                foodNeedTime = getFoodNeedTime();//计算煮食物所需要的时间
                //needCookingTime = foodNeedTime;
                cookingSlider.transform.GetComponent<CookingSlider>().setMaxValue(foodNeedTime);//给slider赋值
                cookerFoodState = CookerFoodState.Cooking;
                cookingSlider.GetComponent<CookingSlider>().repeatFunction();
            }
            else
            {
                //煮过的继续煮
                if (cookerFoodState == CookerFoodState.Cooking)
                {
                    cookingSlider.GetComponent<CookingSlider>().repeatFunction();
                }
                //煮好的继续煮
                else if (cookerFoodState == CookerFoodState.Finshed)
                {
                    cookingSlider.GetComponent<CookingSlider>().repeatOverCookedTimeCount();
                }
            }
            cooking = true;
            readyCooking = false;
        }
       

    }
    /// <summary>
    /// 放在炉子上了吗
    /// </summary>
    public void checkCookerTable()
    {
        if (transform.Find("Food").childCount == 0)
        {
            resetCookController();
            cookingSlider.GetComponent<CookingSlider>().resetCookingSlider();
        }
        //没炉,不能煮东西哇
        if (transform.parent.name!="CookerTable")
        {
            Debug.Log("transform.parent.name!=CookerTable");
            cookingSlider.GetComponent<CookingSlider>().stopValue();
            cookingSlider.GetComponent<CookingSlider>().stopOverCookedTimeCount();
            cooking = false;
            //transform.Find("Cooker").GetComponent<CookerController>().readyCooking = false;

            return;
        }
        //有炉 有东西放在上面 被煮
        else if (transform.Find("Food").childCount > 0 && transform.parent.name == "CookerTable")
        {
            Debug.Log("readyCooking = true");
            readyCooking = true;
        }
        
        
    }

   
    public int getFoodNeedTime()
    {
        int cookTime = 20;
        /*if (transform.GetChild(0).childCount>0)
        {
            cookTime = 20;
            return cookTime;
            //计算时间
            //Food计算的时间time， 先默认为10
        }
        else
        {
            return cookTime;
        }*/
        return cookTime;
    }

    public enum CookerFoodState
    {
        none,
        StopCooking,
        Cooking,
        Finshed,
        OverCooked,
    }
    /// <summary>
    /// 设置锅里食物的状态
    /// </summary>
    /// <param name="state">1煮好，0没煮好，-1煮过头了</param>
    public void setCookerFoodState(int state)
    {
        if(state==1)
        {
            cookerFoodState = CookerFoodState.Finshed;
        }
        else if(state==-1)
        {
            cookerFoodState = CookerFoodState.OverCooked;
            
        }
        else
        {
            cookerFoodState = CookerFoodState.Cooking;
        }
        

    }

    public int getCookerFoodState()
    {
        if (cookerFoodState == CookerFoodState.Finshed)
        {
            return 1;
        }
        else if (cookerFoodState == CookerFoodState.OverCooked)
        {
            return -1;
        }
        else
        {
            return 0;
        }


    }

    /// <summary>
    /// 当cooker重新变空，即将内容物交付给了其他容器/垃圾桶时，重新设置CookController
    /// </summary>
    public void resetCookController()
    {
        readyCooking = false;//未收到开火通知
        cooking = false;

        cookingSlider.transform.GetComponent<CookingSlider>().setMaxValue(0);
        cookingSlider.transform.GetComponent<Slider>().value = 0;
        cookerFoodState = CookerFoodState.none;
    }
    //灶台通知有厨具有东西，开火
    //第一次开火，初始化烹饪的时间，计算
    //关火，停止--
    //没煮完拿起厨具，进度条停止进度，记录进度
    //第二次开火，以进度来烹饪。


    


}





