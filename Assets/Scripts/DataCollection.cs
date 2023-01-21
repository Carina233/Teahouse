using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/*这里使用class还是struct，建议这样判断，
 * 如果这个东西的数据在游戏运行过程中一直不变，建议用class，
 * 需要进行调整其中数值的建议用struct。
 * 例如我们人物现在持有的每一个菜品的情况*/

[System.Serializable]
public class foodDetail//菜肴明细
{
    public string foodId;
    public string foodName;
    public string foodLayer;
    public Sprite foodImage;
    public string foodIntroduction;
    public bool stackable;//食物是否可叠加
    public int stackNum;//叠加个数
    public LayerMask place;//可以放在什么地方
    public bool finshCooking;//是否完成烹饪
    public bool serveCorrectly;//是否正确
    public List<string> canMakeWhatDishes;//可以做成什么菜
    public List<string> canBeMakeByWhat;//由什么合成
}

[System.Serializable] 
public struct playerHold
{
    public foodDetail foodDetail;
    public int num;//持有数量
}