using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DishesFoodController : MonoBehaviour
{
    private List<foodDetail> dishesDataList;
    
    public string dishesFoodName;
    public int calculator;

    void Start()
    {
        dishesDataList = GameObject.Find("FoodList").transform.GetComponent<FoodListManager>().questList[3].foodDetailDataList;

    }
    // Update is called once per frame
    void Update()
    {
        getChildObj();

    }
    public string getDishesFoodName()
    {
        return dishesFoodName;
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
        for (int i = 0; i < dishesDataList.Count; i++)
        {
            if (dishesDataList[i].calculateNum == calculator)
            {
                spriteName = dishesDataList[i].foodImage.name;
                Debug.Log("spriteName66666" + spriteName + calculator);
                dishesFoodName = dishesDataList[i].foodName;
                break;
            }

        }

        GameObject foodSprite = transform.gameObject;

        SpriteRenderer sr = foodSprite.transform.GetComponent<SpriteRenderer>();
        Sprite sprite = Resources.Load("GameItem/Dishes/" + spriteName, typeof(Sprite)) as Sprite;

        sr.sprite = sprite;
        Debug.Log("sr.sprite66666" + sr.sprite);

    }
}
