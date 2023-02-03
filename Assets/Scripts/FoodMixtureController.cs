using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodMixtureController : MonoBehaviour
{
    
    private List<string> list;
    private List<foodDetail> foodDetailDataList;
    public bool checkMix;

    public bool canBeCooked;
    private List<GameObject> childObj;
    // Start is called before the first frame update
    void Start()
    {
        canBeCooked = true;
        checkMix = false;
        
        foodDetailDataList = GameObject.Find("FoodList").transform.GetComponent<FoodListManager>().questList[0].foodDetailDataList;
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
    }

    public void checkMixture()
    {
        checkMix = false;
        int calculator = 0;
        //通过对食材的特殊Id命名，直接计算得合成的物品

        for (int i = 0; i < transform.childCount; i++)
        {
            GameObject go = transform.GetChild(i).gameObject;
            if (go.GetComponent<FoodManager>())
            {
                calculator = calculator + go.GetComponent<FoodManager>().calculateNum;
            }

        }

        string spriteName = "";
        for (int i = 0; i < foodDetailDataList.Count; i++)
        {
            if (foodDetailDataList[i].calculateNum == calculator)
            {
                spriteName = foodDetailDataList[i].foodImage.name;
                Debug.Log("spriteName" + spriteName + calculator);
                break;
            }

        }

        if (spriteName == "")
        {
            Debug.Log("spriteName00000000");
            return;
        }

        GameObject foodSprite = transform.gameObject;

        SpriteRenderer sr = foodSprite.transform.GetComponent<SpriteRenderer>();
        Sprite sprite = Resources.Load("GameItem/" + spriteName, typeof(Sprite)) as Sprite;

        sr.sprite = sprite;
        Debug.Log("sr.sprite" + sr.sprite);

    }
}
