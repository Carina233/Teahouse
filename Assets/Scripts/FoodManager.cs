using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodManager : MonoBehaviour
{
    private GameObject foodList;

    public string foodId;
    public string foodName;
    public string foodLayer;
    public Sprite foodImage;
    public string foodIntroduction;
    public bool stackable;//ʳ���Ƿ�ɵ���
    public int stackNum;//���Ӹ���
    public LayerMask place;//���Է���ʲô�ط�
    public bool finshCooking;//�Ƿ�������
    public bool serveCorrectly;//�Ƿ���ȷ
    public List<string> canMakeWhatDishes;//��������ʲô��
    public List<string> canBeMakeByWhat;//��ʲô�ϳ�
    // Start is called before the first frame update
    void Start()
    {
        foodList = GameObject.Find("FoodList");
        foodName = transform.name;
        List<foodDetail> fdList= foodList.GetComponent<FoodListManager>().questList[0].foodDetailDataList;
        foreach(foodDetail fd in fdList)
        {
            if(foodName==fd.foodName)
            {
                foodId = fd.foodId;
                foodLayer = fd.foodLayer;
                canMakeWhatDishes = fd.canMakeWhatDishes;
                canBeMakeByWhat = fd.canBeMakeByWhat;
                stackable = fd.stackable;
            }
        }


    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
