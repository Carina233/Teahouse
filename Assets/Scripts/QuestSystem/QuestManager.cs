using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class QuestManager : MonoBehaviour
{

    private static QuestManager instance;
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
    private List<Quest> currentList;

    [SerializeField]
    private TMP_Text description;

    private int index = 0;

    // Start is called before the first frame update
    void Start()
    {
        currentList = new List<Quest>();
        Debug.Log(questList.Count);
        


    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void addQuest()
    {
        int random = Random.Range(0, questList.Count);
        currentList.Add(questList[random]);
        //description.GetComponent<TMP_Text>().text = currentList[0]._description;
    }
}
