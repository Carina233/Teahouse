using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodCookedController : MonoBehaviour
{
    
    private List<foodDetail> cookiesDataList;

    public int calculator;
    
    void Start()
    {
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
