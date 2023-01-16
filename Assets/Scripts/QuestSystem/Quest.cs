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
    private string title;//��һ����������
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
    public string _description//�������ۺúó�
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
    [Tooltip("��ѡ�����ѡInOrder��Ŀ�갴OrderIndex��С�����˳��ִ�У�����ͬ�����ʾ����ͬʱ���У���Ŀ��û�й�ѡInOrder�����ʾ��Ŀ�겻��˳��Ӱ�졣")]
    private bool cmpltObjectiveInOrder = false;
    public bool CmpltObjectiveInOrder
    {
        get
        {
            return cmpltObjectiveInOrder;
        }
    }

    [System.NonSerialized]
    private List<Objective> objectives = new List<Objective>();//�洢����Ŀ�꣬������ʱ�õ�����ʼ��ʱ�Զ��������Ϊ��Ԥ�����QuestGiver��
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


    //�������״̬
    //[HideInInspector]
    [System.Serializable]
    public class QuestState
    {
        public bool IsOngoing;//�����Ƿ�����ִ�У�������ʱ�õ�

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
    /// �жϸ������Ƿ���Ҫĳ������
    /// </summary>
    /// <param name="itemID">�����ж��ĵ���</param>
    /// <param name="leftAmount">�����ж�������</param>
    /// <returns></returns>
    public bool RequiredItem(string itemID, int leftAmount)
    {
        if (CmpltObjectiveInOrder)
        {
            foreach (Objective o in Objectives)
            {
                //��Ŀ�����ռ���Ŀ��ʱ�Ž����ж�
                if (o is CollectObjective && itemID == (o as CollectObjective).ItemID)
                {
                    if (o.IsComplete && o.InOrder)
                    {
                        //���ʣ��ĵ�������������ά�ָ�Ŀ�����״̬
                        if (o.Amount > leftAmount)
                        {
                            Objective tempObj = o.NextObjective;
                            while (tempObj != null)
                            {
                                //���ж��Ƿ��к���Ŀ���ڽ��У��Ա�֤�ڴ��Ƹ�Ŀ������״̬ʱ������Ŀ�겻��Ӱ��
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


#region ��������
/// <summary>
/// �����������
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

#region ����Ŀ��
public delegate void UpdateNextObjListener(Objective nextObj);
[System.Serializable]
/// <summary>
/// ����Ŀ��
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

    protected bool AllPrevObjCmplt//�ж�����ǰ��Ŀ�궼�Ƿ����
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
    protected bool HasNextObjOngoing//�ж��Ƿ��к���Ŀ�����ڽ���
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
/// �ռ���Ŀ��
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
    private bool checkBagAtAccept = true;//���ڱ�ʶ�Ƿ��ڽ�ȡ����ʱ��鱳�����߿��Ƿ�����Ŀ�꣬����Ŀ����ͷ��ʼ����
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