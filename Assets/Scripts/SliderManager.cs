using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderManager : MonoBehaviour
{
    private int sliderValue;
    private GameObject qm;

    public void setMaxValue(int value)
    {
        qm = GameObject.Find("QuestManager");
        GetComponent<Slider>().maxValue = value;
        sliderValue = value;
        InvokeRepeating("setValue", 0, 1);

    }
    private void setValue()
    {

        if(sliderValue<=0)
        {
            CancelInvoke("setValue");
            qm.GetComponent<QuestManager>().turnFailedState(transform.parent.gameObject);
            
        }

        sliderValue = sliderValue - 1;
        GetComponent<Slider>().value = sliderValue;
    }
    
    // Start is called before the first frame update
    void Start()
    {
       

    }

    // Update is called once per frame
    void Update()
    {
       
    }
}
