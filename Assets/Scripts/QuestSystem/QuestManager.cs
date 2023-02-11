using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// ���������
/// </summary>
public class QuestManager : MonoBehaviour
{
    public GameObject npcList;
    private static QuestManager instance;
    private int questNo=1;
    public static QuestManager Instance
    {
        get
        {
            if (instance == null || !instance.gameObject)
                instance = FindObjectOfType<QuestManager>();
            return instance;
        }
    }

    /// <summary>
    /// ϵͳ�����б�
    /// </summary>
    [SerializeField]
    private List<Quest> questList;

    


    /// <summary>
    /// �ִ���Ҫ��ɵ�����
    /// </summary>
    [SerializeField]
    private List<GameObject> currentList;

    [SerializeField]
    private GameObject quest;

    private int index = 0;

    // Start is called before the first frame update
    void Start()
    {
        
        currentList = new List<GameObject>();
        //Debug.Log(questList.Count);
        InvokeRepeating("addQuest", 0, 5);


    }

    // Update is called once per frame
    void Update()
    {
       
    }

    /// <summary>
    /// ʹ����ʧ��
    /// </summary>
    /// <param name="go"></param>
    public void turnFailedState(GameObject go)
    {
        go.GetComponent<QuestUI>().state = State.failed;

        deleteQuest(go.transform.name);

        Destroy(go);

    }

    /// <summary>
    /// ʹ�������
    /// </summary>
    /// <param name="go"></param>
    public void turnFinshedState(GameObject go)
    {
        go.GetComponent<QuestUI>().state = State.finshed;

        deleteQuest(go.transform.name);

        Destroy(go);

    }

    /// <summary>
    /// ɾ��������
    /// </summary>
    /// <param name="objName"></param>
    public void deleteQuest(string objName)
    {
        for(int i=0;i<currentList.Count;i++)
        {
            if(currentList[i].transform.name==objName)
            {
                currentList.Remove(currentList[i]);
                return;
            }
        }
    }

    /// <summary>
    /// ����һ������
    /// </summary>
    public void addQuest()
    {

        if(currentList.Count>=3)
        {
            return;
        }

        int random = Random.Range(0, questList.Count);
        
        currentList.Add(initQuest(questList[random]));
    }

    /// <summary>
    /// �����ʼ��
    /// </summary>
    /// <param name="q"></param>
    /// <returns></returns>
    private GameObject initQuest(Quest q)
    {
        GameObject newQuest = GameObject.Instantiate(quest);
        newQuest.transform.SetParent(GameObject.Find("QuestList").transform);
        newQuest.transform.name = "quest" + questNo;
        questNo = questNo + 1;

        newQuest.GetComponent<QuestUI>().foodName=q._title;


        //NPC��ʼ��
        AudioSource audioSource= newQuest.GetComponent<AudioSource>();
        
        TMP_Text npcName = newQuest.GetComponent<QuestUI>().npcName;

        Image npcSprite = newQuest.GetComponent<QuestUI>().npcImage;

        string npcID = q._npcID;
        List<NPCDetail> npcDetailList = npcList.GetComponent<NpcListManager>().npcList[0].npcDetailDataList;

        foreach(NPCDetail n in npcDetailList)
        {
            if(n.npcID==npcID)
            {
                
                audioSource.clip = n.npcVoice;
                
                audioSource.Play();
                
                Debug.Log("n.npcVoice" + n.npcVoice);

                npcSprite.transform.GetComponent<Image>().sprite = n.npcSprite;
                npcName.transform.GetComponent<TMP_Text>().text =n.npcName;
                
                
                break;
            }
        }




        Slider slider = newQuest.GetComponent<QuestUI>().slider;
        slider.transform.GetComponent<QuestSlider>().setMaxValue(q._maxTime);
      

        TMP_Text description = newQuest.GetComponent<QuestUI>().description;
        description.transform.GetComponent<TMP_Text>().text=q._description;

        newQuest.GetComponent<QuestUI>().state = State.waiting;

        


        return newQuest;
    }

    /// <summary>
    /// ��������Ƿ����
    /// </summary>
    /// <param name="go"></param>
    public void checkQuest(GameObject go)
    {
        for(int i=0;i<currentList.Count;i++)
        {
            if(currentList[i].transform.GetComponent<QuestUI>().foodName==go.transform.name|| 
                currentList[i].transform.GetComponent<QuestUI>().foodName == go.transform.GetComponent<DishesFoodController>().getDishesFoodName())
            {

                Debug.Log("�������"+ currentList[i].name + go.transform.name+ currentList[i].transform.GetComponent<QuestUI>().foodName);

                GameObject quest= GameObject.Find(currentList[i].name);
                turnFinshedState(quest);
                
                break;
            }
        }
        if (go.transform.parent.name == "Dish")
        {
            Destroy(go.transform.parent.gameObject);
        }
        else
        {
            Destroy(go);
        }
    }

    /// <summary>
    /// ����״̬ö������
    /// </summary>
    public enum State
    {
        waiting,
        finshed,
        failed
    }

}
