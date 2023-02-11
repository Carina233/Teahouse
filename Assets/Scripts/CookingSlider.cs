using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CookingSlider : SliderManager
{
    public GameObject cooker;

    public int overcookedTime=0;
    /// <summary>
    /// ���ý����������ֵ,��ʼ��
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
    /// ������--
    /// </summary>
    public override void setValue()
    {
        if (GetComponent<Slider>().value <= 0)
        {
            //�����
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
    /// ���ͷ��ʱ����㣬����һ��ʱ��food��״̬����overcooked����Ҫ��
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
    /// ��cooker���±�գ����������ｻ��������������/����Ͱʱ,��������CookingSlider
    /// </summary>
    public void resetCookingSlider()
    {
       overcookedTime = 0;
       stopOverCookedTimeCount();
       stopValue();
    }
}
