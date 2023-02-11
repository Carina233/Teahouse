using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
    private string dishID;
    public string _dishID
    {
        get
        {
            return dishID;
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


    [SerializeField]
    private int maxTime;//做一个鸡蛋肠粉
    public int _maxTime
    {
        get
        {
            return maxTime;
        }
    }


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


