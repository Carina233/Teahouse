using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WasteBinController : MonoBehaviour
{
    
    void Update()
    {
        checkWaste();
    }

    void checkWaste()
    {
        int length = GetComponentsInChildren<Transform>(true).Length;
        if (length <= 1)//根节点自己算一个transform
        {
            return;
        }
        for(int i= transform.childCount-1; i>=0;i--)
        {
            Debug.Log("垃圾来了");
            Destroy(transform.GetChild(i).gameObject);
            
        }
    }
}
