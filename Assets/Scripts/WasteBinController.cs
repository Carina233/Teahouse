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
        if (length <= 1)//���ڵ��Լ���һ��transform
        {
            return;
        }
        for(int i= transform.childCount-1; i>=0;i--)
        {
            Debug.Log("��������");
            Destroy(transform.GetChild(i).gameObject);
            
        }
    }
}
