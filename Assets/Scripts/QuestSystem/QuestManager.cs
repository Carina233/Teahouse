using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class QuestManager : MonoBehaviour
{
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

    [SerializeField]
    private List<Quest> questList;

    [SerializeField]
    private List<GameObject> currentList;

    [SerializeField]
    private GameObject quest;

    private int index = 0;

    // Start is called before the first frame update
    void Start()
    {
        currentList = new List<GameObject>();
        Debug.Log(questList.Count);
        


    }

    // Update is called once per frame
    void Update()
    {
       
    }

    public void turnFailedState(GameObject go)
    {
        go.GetComponent<QuestUI>().state = State.failed;

        deleteQuest(go.transform.name);

        Destroy(go);

    }
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

    public void addQuest()
    {

        if(currentList.Count>=3)
        {
            return;
        }

        int random = Random.Range(0, questList.Count);
        
        currentList.Add(initQuest(questList[random]));
    }

    private GameObject initQuest(Quest q)
    {
        GameObject newQuest = GameObject.Instantiate(quest);
        newQuest.transform.SetParent(GameObject.Find("QuestList").transform);
        newQuest.transform.name = "quest" + questNo;
        questNo = questNo + 1;

        Slider slider = newQuest.GetComponent<QuestUI>().slider;
        slider.transform.GetComponent<SliderManager>().setMaxValue(q._maxTime);

        TMP_Text description = newQuest.GetComponent<QuestUI>().description;
        description.transform.GetComponent<TMP_Text>().text=q._description;

        newQuest.GetComponent<QuestUI>().state = State.waiting;

        return newQuest;
    }

    public enum State
    {
        waiting,
        finshed,
        failed
    }

}
