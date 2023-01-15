using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeviceManager : MonoBehaviour
{
    public int requirement;
    public GameObject requirementOwner;
    public GameObject foodType;
    // Start is called before the first frame update
    void Start()
    {
        requirement = 0;
    }

    // Update is called once per frame
    void Update()
    {
        checkRequirement();
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
        food.transform.SetParent(requirementOwner.transform);
        food.transform.position = requirementOwner.transform.position;
    }
}
