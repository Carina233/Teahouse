using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CookerController : MonoBehaviour
{
    public Slider cookingSlider;
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
    }

    // Update is called once per frame
    void Update()
    {

        checkCookerTable();

        if(readyCooking==true&&cooking==false)
        {
            if(cookingSlider.transform.GetComponent<Slider>().maxValue== 0)
            {
                Debug.Log("readyCooking==true&&cooking==false");
                foodNeedTime = getFoodNeedTime();//计算煮食物所需要的时间
                //needCookingTime = foodNeedTime;
                cookingSlider.transform.GetComponent<CookingSlider>().setMaxValue(foodNeedTime);//给slider赋值
                cookingSlider.GetComponent<CookingSlider>().repeatFunction();
               
            }
            else
            {
                cookingSlider.GetComponent<CookingSlider>().repeatFunction();
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

        //没炉,不能煮东西哇
        if (transform.parent.name!="CookerTable")
        {
            Debug.Log("transform.parent.name!=CookerTable");
            cookingSlider.GetComponent<CookingSlider>().stopValue();
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

    

}


//灶台通知有厨具有东西，开火
//第一次开火，初始化烹饪的时间，计算
//关火，停止--
//没煮完拿起厨具，进度条停止进度，记录进度
//第二次开火，以进度来烹饪。