using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodCookedController : MonoBehaviour
{
    
    private List<foodDetail> cookiesDataList;
    private GameObject cooker;

    public int calculator;
    
    void Start()
    {
        cooker = transform.parent.gameObject;
        cookiesDataList = GameObject.Find("FoodList").transform.GetComponent<FoodListManager>().questList[2].foodDetailDataList;
        
    }
    // Update is called once per frame
    void Update()
    {
        getChildObj();
        
    }

    private void getChildObj()
    {
        if (transform.childCount == 0)
        {
            noChildSprite();
        }
        else
        {
            setSprite();
        }
    }

    private void noChildSprite()
    {
        GameObject foodSprite = transform.gameObject;
        SpriteRenderer sr = foodSprite.transform.GetComponent<SpriteRenderer>();
        sr.sprite = null;
    }

    public void setSprite()
    {
        Debug.Log("颜色变白那百年");
        if(cooker.GetComponent<CookerController>().getCookerFoodState()==-1)
        {
            Debug.Log("啊？");
            transform.GetComponent<SpriteRenderer>().color = Color.black;
        }
        else
        {
            Debug.Log("白");
            transform.GetComponent<SpriteRenderer>().color = Color.white;
        }
        string spriteName = "";
        for (int i = 0; i < cookiesDataList.Count; i++)
        {
            if (cookiesDataList[i].calculateNum == calculator)
            {
                spriteName = cookiesDataList[i].foodImage.name;
                Debug.Log("spriteName66666" + spriteName + calculator);
                break;
            }

        }

        GameObject foodSprite = transform.gameObject;

        SpriteRenderer sr = foodSprite.transform.GetComponent<SpriteRenderer>();
        Sprite sprite = Resources.Load("GameItem/Cookies/" + spriteName, typeof(Sprite)) as Sprite;

        sr.sprite = sprite;
        Debug.Log("sr.sprite66666" + sr.sprite);

    }
}
