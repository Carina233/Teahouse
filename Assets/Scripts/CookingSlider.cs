using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CookingSlider : SliderManager
{
    public GameObject cooker;

    public int overcookedTime=0;
    /// <summary>
    /// 设置进度条的最大值,初始化
    /// </summary>
    /// <param name="value"></param>
    public override void setMaxValue(int value)
    {
        if(value>0)
        {
            gameObject.SetActive(true);
        }
        
        GetComponent<Slider>().maxValue = value;
        GetComponent<Slider>().value = value;
    }

    public void repeatFunction()
    {
        InvokeRepeating("setValue", 0, 1);
    }

    /// <summary>
    /// 进度条--
    /// </summary>
    public override void setValue()
    {
        if (GetComponent<Slider>().value <= 0)
        {
            //煮好了
            gameObject.SetActive(false);


            cooker.GetComponent<CookerController>().setCookerFoodState(1);
            InvokeRepeating("overCookedTimeCount", 0, 1);
            CancelInvoke("setValue");

        }
        GetComponent<Slider>().value = GetComponent<Slider>().value - 1;
    }

    public void stopValue()
    {

        CancelInvoke("setValue");
    }

    public void repeatOverCookedTimeCount()
    {
        InvokeRepeating("overCookedTimeCount", 0, 1);
    }

    /// <summary>
    /// 煮过头的时间计算，超过一定时间food的状态就是overcooked不能要了
    /// </summary>
    public void overCookedTimeCount()
    {
        if(overcookedTime>8)
        {
            ///
            cooker.GetComponent<CookerController>().setCookerFoodState(-1);
            CancelInvoke("overCookedTimeCount");
        }
        else
        {
            overcookedTime = overcookedTime + 1;
        }
       
    }

    public void stopOverCookedTimeCount()
    {

        CancelInvoke("overCookedTimeCount");
    }

    /// <summary>
    /// 当cooker重新变空，即将内容物交付给了其他容器/垃圾桶时,重新设置CookingSlider
    /// </summary>
    public void resetCookingSlider()
    {
       overcookedTime = 0;
       stopOverCookedTimeCount();
       stopValue();
    }
}
