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


    [SerializeField]
    private int maxTime;//��һ����������
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
    [Tooltip("��ѡ�����ѡInOrder��Ŀ�갴OrderIndex��С�����˳��ִ�У�����ͬ�����ʾ����ͬʱ���У���Ŀ��û�й�ѡInOrder�����ʾ��Ŀ�겻��˳��Ӱ�졣")]
    private bool cmpltObjectiveInOrder = false;
    public bool CmpltObjectiveInOrder
    {
        get
        {
            return cmpltObjectiveInOrder;
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


