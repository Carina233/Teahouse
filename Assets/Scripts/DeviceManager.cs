using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DeviceManager : MonoBehaviour
{
    public int deviceLayer;
    
    public int requirement;
    public GameObject requirementOwner;
    public GameObject foodType;
    
    // Start is called before the first frame update
    void Start()
    {
        requirement = 0;
        deviceLayer = gameObject.layer ;
       
    }

    // Update is called once per frame
    void Update()
    {
        if (deviceLayer == LayerMask.NameToLayer("Warehouse"))
        {
            checkRequirement();
        }
        
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
