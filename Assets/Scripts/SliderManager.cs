using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class SliderManager : MonoBehaviour
{

    /// <summary>
    /// ���ý����������ֵ
    /// </summary>
    /// <param name="value"></param>
    public abstract void setMaxValue(int value);

    /// <summary>
    /// ������--
    /// </summary>
    public abstract void setValue();
    

}

