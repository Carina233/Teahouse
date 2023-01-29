using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CookerController : MonoBehaviour
{
    public Slider cookingSlider;
    public bool readyCooking=false;//未收到开火通知
    public bool cooking = false;
   
    // Start is called before the first frame update
    void Start()
    {
        cookingSlider.transform.GetComponent<Slider>().value = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if(!cooking)
        {
            //初始化
            Debug.Log("66666666666");
            initCurrentTime(getFoodNeedTime());
        }
        if (cookingSlider.transform.GetComponent<Slider>().value>0&&readyCooking)
        {
            int crt = (int)cookingSlider.transform.GetComponent<Slider>().value;
            Debug.Log("11111111111");
            readyCooking = false;
            cooking = true;
            cookingSlider.GetComponent<CookingSlider>().setMaxValue(crt);
            
        }
    }

   

    public int getFoodNeedTime()
    {
        if(transform.GetChild(0).childCount>0)
        {
            //计算时间
            //Food计算的时间time， 先默认为10
        }
        int cookTime = 20;
        return cookTime;
    }

    private void initCurrentTime(int sctime)
    {
        cookingSlider.transform.GetComponent<Slider>().maxValue = sctime;
        cookingSlider.transform.GetComponent<Slider>().value = sctime;
    }

}
