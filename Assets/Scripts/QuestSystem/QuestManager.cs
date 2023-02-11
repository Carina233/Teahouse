using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 任务管理器
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
    /// 系统任务列表
    /// </summary>
    [SerializeField]
    private List<Quest> questList;

    


    /// <summary>
    /// 现存需要完成的任务
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
    /// 使任务失败
    /// </summary>
    /// <param name="go"></param>
    public void turnFailedState(GameObject go)
    {
        go.GetComponent<QuestUI>().state = State.failed;

        deleteQuest(go.transform.name);

        Destroy(go);

    }

    /// <summary>
    /// 使任务被完成
    /// </summary>
    /// <param name="go"></param>
    public void turnFinshedState(GameObject go)
    {
        go.GetComponent<QuestUI>().state = State.finshed;

        deleteQuest(go.transform.name);

        Destroy(go);

    }

    /// <summary>
    /// 删除该任务
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
    /// 新增一个任务
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
    /// 任务初始化
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


        //NPC初始化
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
    /// 检查任务是否被完成
    /// </summary>
    /// <param name="go"></param>
    public void checkQuest(GameObject go)
    {
        for(int i=0;i<currentList.Count;i++)
        {
            if(currentList[i].transform.GetComponent<QuestUI>().foodName==go.transform.name|| 
                currentList[i].transform.GetComponent<QuestUI>().foodName == go.transform.GetComponent<DishesFoodController>().getDishesFoodName())
            {

                Debug.Log("完成任务"+ currentList[i].name + go.transform.name+ currentList[i].transform.GetComponent<QuestUI>().foodName);

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
    /// 任务状态枚举类型
    /// </summary>
    public enum State
    {
        waiting,
        finshed,
        failed
    }

}
