using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestSlider : SliderManager
{
    private int sliderValue;
    private GameObject qm;

    /// <summary>
    /// ���ý����������ֵ
    /// </summary>
    /// <param name="value"></param>
    public override void setMaxValue(int value)
    {

        GetComponent<Slider>().maxValue = value;
        sliderValue = value;
        InvokeRepeating("setValue", 0, 1);

    }
    /// <summary>
    /// ������--
    /// </summary>
    public override void setValue()
    {
        qm = GameObject.Find("QuestManager");
        if (sliderValue <= 0)
        {
            CancelInvoke("setValue");
            qm.GetComponent<QuestManager>().turnFailedState(transform.parent.gameObject);

        }

        sliderValue = sliderValue - 1;
        GetComponent<Slider>().value = sliderValue;
    }
}
