using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutputCheck : MonoBehaviour
{
    private GameObject qm;
    // Start is called before the first frame update
    void Start()
    {
        qm = GameObject.Find("QuestManager");

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
        //Outputcheck
        //  Dish
        //      Food
        //          XXX
        foreach (Transform child in transform)
        {
            if(child.gameObject.layer==LayerMask.NameToLayer("Food"))
            {
                Debug.Log("菜品name:" + child.name);
                qm.GetComponent<QuestManager>().checkQuest(child.gameObject);
            }
            else if(child.transform.name=="Dish")
            {
                Debug.Log("菜品name:" + child.GetChild(0).name);
                qm.GetComponent<QuestManager>().checkQuest(child.GetChild(0).gameObject);
            }
            //Destroy(child.gameObject);
        }


    }
}
