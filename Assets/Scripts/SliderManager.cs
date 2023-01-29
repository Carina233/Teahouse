using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class SliderManager : MonoBehaviour
{

    /// <summary>
    /// 设置进度条的最大值
    /// </summary>
    /// <param name="value"></param>
    public abstract void setMaxValue(int value);

    /// <summary>
    /// 进度条--
    /// </summary>
    public abstract void setValue();
    

}

