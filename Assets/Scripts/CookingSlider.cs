using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CookingSlider : SliderManager
{
    /// <summary>
    /// ���ý����������ֵ,��ʼ��
    /// </summary>
    /// <param name="value"></param>
    public override void setMaxValue(int value)
    {
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
            CancelInvoke("setValue");

        }
        GetComponent<Slider>().value = GetComponent<Slider>().value - 1;
    }

    public void stopValue()
    {
        CancelInvoke("setValue");
    }


}
