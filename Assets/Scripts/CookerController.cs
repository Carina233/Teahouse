using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CookerController : MonoBehaviour
{
    public Slider cookingSlider;
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
                foodNeedTime = getFoodNeedTime();//������ʳ������Ҫ��ʱ��
                //needCookingTime = foodNeedTime;
                cookingSlider.transform.GetComponent<CookingSlider>().setMaxValue(foodNeedTime);//��slider��ֵ
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
    /// ����¯��������
    /// </summary>
    public void checkCookerTable()
    {

        //û¯,����������
        if (transform.parent.name!="CookerTable")
        {
            Debug.Log("transform.parent.name!=CookerTable");
            cookingSlider.GetComponent<CookingSlider>().stopValue();
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

    

}


//��̨֪ͨ�г����ж���������
//��һ�ο��𣬳�ʼ����⿵�ʱ�䣬����
//�ػ�ֹͣ--
//û����������ߣ�������ֹͣ���ȣ���¼����
//�ڶ��ο����Խ�������⿡�