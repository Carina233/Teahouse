using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CookerController : MonoBehaviour
{
    public Slider cookingSlider;
    public CookerFoodState cookerFoodState;
    public bool readyCooking=false;//δ�յ�����֪ͨ
    public bool cooking = false;

    /// <summary>
    /// û�����ʳ����Ҫ����ʱ��
    /// </summary>
    private int foodNeedTime;

    /// <summary>
    /// ����ʳ�ﻹ��Ҫ��⿵�ʱ��
    /// </summary>
    private int needCookingTime;

    // Start is called before the first frame update
    void Start()
    {
        //��ʼ��������
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
            //��û������ģ���ʼ��
            if(cookingSlider.transform.GetComponent<Slider>().maxValue== 0)
            {
                Debug.Log("readyCooking==true&&cooking==false");
                foodNeedTime = getFoodNeedTime();//������ʳ������Ҫ��ʱ��
                //needCookingTime = foodNeedTime;
                cookingSlider.transform.GetComponent<CookingSlider>().setMaxValue(foodNeedTime);//��slider��ֵ
                cookerFoodState = CookerFoodState.Cooking;
                cookingSlider.GetComponent<CookingSlider>().repeatFunction();
            }
            else
            {
                //����ļ�����
                if (cookerFoodState == CookerFoodState.Cooking)
                {
                    cookingSlider.GetComponent<CookingSlider>().repeatFunction();
                }
                //��õļ�����
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
    /// ����¯��������
    /// </summary>
    public void checkCookerTable()
    {
        if (transform.Find("Food").childCount == 0)
        {
            resetCookController();
            cookingSlider.GetComponent<CookingSlider>().resetCookingSlider();
        }
        //û¯,����������
        if (transform.parent.name!="CookerTable")
        {
            Debug.Log("transform.parent.name!=CookerTable");
            cookingSlider.GetComponent<CookingSlider>().stopValue();
            cookingSlider.GetComponent<CookingSlider>().stopOverCookedTimeCount();
            cooking = false;
            //transform.Find("Cooker").GetComponent<CookerController>().readyCooking = false;

            return;
        }
        //��¯ �ж����������� ����
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
            //����ʱ��
            //Food�����ʱ��time�� ��Ĭ��Ϊ10
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
    /// ���ù���ʳ���״̬
    /// </summary>
    /// <param name="state">1��ã�0û��ã�-1���ͷ��</param>
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
    /// ��cooker���±�գ����������ｻ��������������/����Ͱʱ����������CookController
    /// </summary>
    public void resetCookController()
    {
        readyCooking = false;//δ�յ�����֪ͨ
        cooking = false;

        cookingSlider.transform.GetComponent<CookingSlider>().setMaxValue(0);
        cookingSlider.transform.GetComponent<Slider>().value = 0;
        cookerFoodState = CookerFoodState.none;
    }
    //��̨֪ͨ�г����ж���������
    //��һ�ο��𣬳�ʼ����⿵�ʱ�䣬����
    //�ػ�ֹͣ--
    //û����������ߣ�������ֹͣ���ȣ���¼����
    //�ڶ��ο����Խ�������⿡�


    


}





