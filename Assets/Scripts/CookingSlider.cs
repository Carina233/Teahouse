using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CookingSlider : SliderManager
{
    public int sliderValue;
    
    /// <summary>
    /// 设置进度条的最大值,初始化
    /// </summary>
    /// <param name="value"></param>
    public override void setMaxValue(int value)
    {
       
        GetComponent<Slider>().maxValue = value;
        //GetComponent<Slider>().value = value;
        sliderValue = value;
        //repeatSetValue();
        InvokeRepeating("setValue", 0, 1);
    }

    /// <summary>
    /// 进度条--
    /// </summary>
    public override void setValue()
    {
        Debug.Log("setValue:" + sliderValue);
       
        if (sliderValue <= 0)
        {
            CancelInvoke("setValue");

        }
        sliderValue = sliderValue - 1;
        GetComponent<Slider>().value = sliderValue;
    }

    public void stopValue()
    {

        CancelInvoke("setValue");

    }


}
