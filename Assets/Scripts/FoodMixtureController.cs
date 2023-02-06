using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodMixtureController : MonoBehaviour
{
    
    private List<string> list;
    private List<foodDetail> materialsDataList;
    private List<foodDetail> mixturesDataList;
    public int calculator;
    public bool checkMix;

    public bool canBeCooked;
    private List<GameObject> childObj;
    // Start is called before the first frame update
    void Start()
    {
        canBeCooked = true;
        checkMix = false;

        materialsDataList = GameObject.Find("FoodList").transform.GetComponent<FoodListManager>().questList[0].foodDetailDataList;
        mixturesDataList = GameObject.Find("FoodList").transform.GetComponent<FoodListManager>().questList[1].foodDetailDataList;
    }

    // Update is called once per frame
    void Update()
    {
        getChildObj();
        if (checkMix)
        {
            checkMixture();
        }
    }

    private void getChildObj()
    {
       foreach(Transform child in this.transform)
        {
            canBeCooked=canBeCooked&&child.GetComponent<FoodManager>().canBeCooked;
        }
       
       if(transform.childCount==0)
        {
            noChildSprite();
        }
    }

    private void noChildSprite()
    {
        GameObject foodSprite = transform.gameObject;

        SpriteRenderer sr = foodSprite.transform.GetComponent<SpriteRenderer>();
        

        sr.sprite = null;
    }

    public void checkMixture()
    {
        checkMix = false;
        int cal = 0;
        //通过对食材的特殊Id命名，直接计算得合成的物品

        for (int i = 0; i < transform.childCount; i++)
        {
            GameObject go = transform.GetChild(i).gameObject;
            

            SpriteRenderer childSR = go.transform.GetComponent<SpriteRenderer>();
            childSR.sprite = null;
            if (go.GetComponent<FoodManager>())
            {
                cal = cal + go.GetComponent<FoodManager>().calculateNum;
            }

        }

        string spriteName = "";
        for (int i = 0; i < mixturesDataList.Count; i++)
        {
            if (mixturesDataList[i].calculateNum == cal)
            {
                spriteName = mixturesDataList[i].foodImage.name;
                Debug.Log("spriteName" + spriteName + cal);
                break;
            }

        }

        calculator = cal;

        if (spriteName == "")
        {
            Debug.Log("spriteName00000000");
            return;
        }

        GameObject foodSprite = transform.gameObject;

        SpriteRenderer sr = foodSprite.transform.GetComponent<SpriteRenderer>();
        Sprite sprite = Resources.Load("GameItem/Mixtures/" + spriteName, typeof(Sprite)) as Sprite;

        sr.sprite = sprite;
        Debug.Log("sr.sprite" + sr.sprite);

    }
}
