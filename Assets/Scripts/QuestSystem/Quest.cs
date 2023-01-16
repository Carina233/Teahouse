using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
[CreateAssetMenu(fileName = "quest", menuName = "quest/newquest")]
public class Quest : ScriptableObject
{
    [SerializeField]
    private string questID;
    public string _questID
    {
        get
        {
            return questID;
        }
    }

    [SerializeField]
    private string npcID;
    public string _npcID
    {
        get
        {
            return npcID;
        }
    }

    [SerializeField]
    private string title;//做一个鸡蛋肠粉
    public string _title
    {
        get
        {
            return title;
        }
    }

    [SerializeField]
    [TextArea]
    private string description;
    public string _description//鸡蛋肠粉好好吃
    {
        get
        {
            return description;
        }
    }







    /*[SerializeField]
    private QuestGroup questGroup;
    public QuestGroup MQuestGroup
    {
        get
        {
            return questGroup;
        }

        set
        {
            questGroup = value;
        }
    }*/
    [SerializeField]
    private QuestState questState;
    public QuestState _questState
    {
        get
        {
            return questState;
        }
    }

    [SerializeField]
    private QuestReward questReward;
    public QuestReward _questReward
    {
        get
        {
            return questReward;
        }
    }

   
    

    [Space]
    [SerializeField]
    [Tooltip("勾选此项，则勾选InOrder的目标按OrderIndex从小到大的顺序执行，若相同，则表示可以同时进行；若目标没有勾选InOrder，则表示该目标不受顺序影响。")]
    private bool cmpltObjectiveInOrder = false;
    public bool CmpltObjectiveInOrder
    {
        get
        {
            return cmpltObjectiveInOrder;
        }
    }

    [System.NonSerialized]
    private List<Objective> objectives = new List<Objective>();//存储所有目标，在运行时用到，初始化时自动填，不用人为干预，详见QuestGiver类
    public List<Objective> Objectives
    {
        get
        {
            return objectives;
        }
    }

    [SerializeField]
    private CollectObjective[] collectObjectives;
    public CollectObjective[] CollectObjectives
    {
        get
        {
            return collectObjectives;
        }
    }

   

    [System.NonSerialized]
    private QuestGiver questGiver;
    public QuestGiver _questGiver
    {
        get
        {
            return questGiver;
        }

        set
        {
            questGiver = value;
        }
    }


    //任务完成状态
    //[HideInInspector]
    [System.Serializable]
    public class QuestState
    {
        public bool IsOngoing;//任务是否正在执行，在运行时用到

        public bool IsComplete
        {
            get
            {
                /*foreach (CollectObjective co in collectObjectives)
                    if (!co.IsComplete) return false;*/

                return true;
            }
        }

        public bool IsFailed;
    }

    


    /// <summary>
    /// 判断该任务是否需要某个道具
    /// </summary>
    /// <param name="itemID">所需判定的道具</param>
    /// <param name="leftAmount">所需判定的数量</param>
    /// <returns></returns>
    public bool RequiredItem(string itemID, int leftAmount)
    {
        if (CmpltObjectiveInOrder)
        {
            foreach (Objective o in Objectives)
            {
                //当目标是收集类目标时才进行判断
                if (o is CollectObjective && itemID == (o as CollectObjective).ItemID)
                {
                    if (o.IsComplete && o.InOrder)
                    {
                        //如果剩余的道具数量不足以维持该目标完成状态
                        if (o.Amount > leftAmount)
                        {
                            Objective tempObj = o.NextObjective;
                            while (tempObj != null)
                            {
                                //则判断是否有后置目标在进行，以保证在打破该目标的完成状态时，后置目标不受影响
                                if (tempObj.CurrentAmount > 0 && tempObj.OrderIndex > o.OrderIndex)
                                {
                                    //Debug.Log("Required");
                                    return true;
                                }
                                tempObj = tempObj.NextObjective;
                            }
                        }
                        //Debug.Log("NotRequired3");
                        return false;
                    }
                    //Debug.Log("NotRequired2");
                    return false;
                }
            }
        }
        //Debug.Log("NotRequired1");
        return false;
    }
}

[System.Serializable]
public class QuestReward
{
    [SerializeField]
    private int money;
    public int Money
    {
        get
        {
            return money;
        }
    }

    [SerializeField]
    private int EXP;
    public int _EXP
    {
        get
        {
            return EXP;
        }
    }

    
}


#region 任务条件
/// <summary>
/// 任务接收条件
/// </summary>
[System.Serializable]
public class QuestAcceptCondition
{
    

    [SerializeField]
    private int level;
    public int Level
    {
        get
        {
            return level;
        }
    }

    [SerializeField]
    
    private string IDOfCompleteQuest;
    public string _IDOfCompleteQuest
    {
        get
        {
            return IDOfCompleteQuest;
        }
    }

    [SerializeField]
    //[ConditionalHide("acceptCondition", (int)QuestCondition.ComplexQuest, true)]
    private Quest completeQuest;
    public Quest CompleteQuest
    {
        get
        {
            return completeQuest;
        }
    }

    [SerializeField]
    //[ConditionalHide("acceptCondition", (int)QuestCondition.HasItem, true)]
    private string IDOfOwnedItem;
    public string _IDOfOwnedItem
    {
        get
        {
            return IDOfOwnedItem;
        }
    }

    [SerializeField]
    //[ConditionalHide("acceptCondition", (int)QuestCondition.HasItem, true)]
    private ItemBase owneditem;
    public ItemBase Owneditem
    {
        get
        {
            return owneditem;
        }
    }

    
}


#endregion

#region 任务目标
public delegate void UpdateNextObjListener(Objective nextObj);
[System.Serializable]
/// <summary>
/// 任务目标
/// </summary>
public abstract class Objective
{
    [HideInInspector]
    public string runtimeID;

    [SerializeField]
    private string displayName;
    public string DisplayName
    {
        get
        {
            return displayName;
        }
    }

    [SerializeField]
    private int amount;
    public int Amount
    {
        get
        {
            return amount;
        }
    }

    private int currentAmount;
    public int CurrentAmount
    {
        get
        {
            return currentAmount;
        }

        set
        {
            bool befCmplt = IsComplete;
            if (value < amount && value >= 0)
                currentAmount = value;
            else if (value < 0)
            {
                currentAmount = 0;
            }
            else currentAmount = amount;
            if (!befCmplt && IsComplete)
                OnCompleteThisEvent(NextObjective);
        }
    }

    public bool IsComplete
    {
        get
        {
            if (currentAmount >= amount)
                return true;
            return false;
        }
    }

    [SerializeField]
    private bool inOrder;
    public bool InOrder
    {
        get
        {
            return inOrder;
        }
    }

    [SerializeField]
    //[ConditionalHide("inOrder", true)]
    private int orderIndex;
    public int OrderIndex
    {
        get
        {
            return orderIndex;
        }
    }

    [System.NonSerialized]
    public Objective PrevObjective;
    [System.NonSerialized]
    public Objective NextObjective;

    [field: System.NonSerialized]
    public event UpdateNextObjListener OnCompleteThisEvent;

    protected virtual void UpdateStatus()
    {
        if (IsComplete) return;
        if (!InOrder) CurrentAmount++;
        else if (InOrder && AllPrevObjCmplt) CurrentAmount++;
    }

    protected bool AllPrevObjCmplt//判定所有前置目标都是否完成
    {
        get
        {
            Objective tempObj = PrevObjective;
            while (tempObj != null)
            {
                if (!tempObj.IsComplete && tempObj.OrderIndex < OrderIndex)
                {
                    return false;
                }
                tempObj = tempObj.PrevObjective;
            }
            return true;
        }
    }
    protected bool HasNextObjOngoing//判定是否有后置目标正在进行
    {
        get
        {
            Objective tempObj = NextObjective;
            while (tempObj != null)
            {
                if (tempObj.CurrentAmount > 0 && tempObj.OrderIndex > OrderIndex)
                {
                    return true;
                }
                tempObj = tempObj.NextObjective;
            }
            return false;
        }
    }
}
/// <summary>
/// 收集类目标
/// </summary>
[System.Serializable]
public class CollectObjective : Objective
{
    [SerializeField]
    private string itemID;
    public string ItemID
    {
        get
        {
            return itemID;
        }
    }

    [SerializeField]
    private bool checkBagAtAccept = true;//用于标识是否在接取任务时检查背包道具看是否满足目标，否则目标重头开始计数
    public bool CheckBagAtAccept
    {
        get
        {
            return checkBagAtAccept;
        }

        set
        {
            checkBagAtAccept = value;
        }
    }

   
}

#endregion