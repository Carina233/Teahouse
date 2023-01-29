using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DeviceManager : MonoBehaviour
{
    public string deviceName;
    
    public int requirement;
    public GameObject requirementOwner;
    public GameObject foodType;
    
    // Start is called before the first frame update
    void Start()
    {
        requirement = 0;
        deviceName = transform.name;
       
    }

    // Update is called once per frame
    void Update()
    {
        if (deviceName =="Warehouse")
        {
            checkRequirement();
        }
        else if(deviceName=="CookerTable")
        {
            checkCooking();
        }
    }
    /// <summary>
    /// 有东西在煮吗
    /// </summary>
    /// <returns></returns>
    public void checkCooking()
    {
        //没锅
        if(!transform.Find("Cooker"))
        {
            return ;
        }
        //有锅 有东西放在上面 准备自动开火
        if (transform.Find("Cooker/Food").childCount>0&& transform.Find("Cooker").GetComponent<CookerController>().cooking==false)
        {
            //通知锅已经开火
            transform.Find("Cooker").GetComponent<CookerController>().readyCooking=true;
        }
        //有锅没东西在煮


        return ;
    }

    

    private void checkRequirement()
    {
        if(requirement==0)
        {
            return;
        }
        else
        {
            requirement = 0;
            meetRequirement();
        }
    }

    private void meetRequirement()
    {
        GameObject food = Instantiate(foodType, transform);
        food.transform.name = foodType.transform.name;
        food.transform.SetParent(requirementOwner.transform);
        food.transform.position = requirementOwner.transform.position;
        
    }

    /// <summary>
    /// 检查物体能否被放进来
    /// </summary>
    public bool CheckInput(GameObject foodInHand)
    {
        GameObject food;
        if(foodInHand.layer==LayerMask.NameToLayer("Dish"))
        {
            food = foodInHand.transform.GetChild(0).gameObject;
            if(food.transform.GetComponent<FoodMixtureController>().canBeCooked)
            {
                return true;
            }
        }
        
        return false;

    }
    
}
