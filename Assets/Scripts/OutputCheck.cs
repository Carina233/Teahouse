using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutputCheck : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        checkFood();
    }

    void checkFood()
    {
        int length = GetComponentsInChildren<Transform>(true).Length;
        if (length <= 1)//根节点自己算一个transform
        {
            Debug.Log("没上菜");
            return;
        }
        
        foreach (Transform child in transform)
        {
            
            Debug.Log("菜品name:" + child.name);
            Destroy(child.gameObject);
        }


    }
}
