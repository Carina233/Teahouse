using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
    [Tooltip("�ؿ�")] public int level;
    [Tooltip("ʱ��")] public int time;
    [Tooltip("�ɹ�������")] public int successNum;
    [Tooltip("ʧ�ܶ�����")] public int failNum;
    [Tooltip("�ܼ�����")] public int money;
    [Tooltip("�ȼ�����")] public List<int> starLevel;
    [Tooltip("�������")] public int star;

    /*public int time;
    [Tooltip("�ܹ����Ӹ���")] public int totalBoxCount;
    [Tooltip("��ǰ��������Ӹ���")] public int currentBoxCount;
    [Tooltip("����ƶ�����")] public int playerMoveStep = 0;
    [HideInInspector] public int playerMoveStepLeft;
    [Tooltip("���ʣ���ƶ�������ʾ��ֵ")] public int playerMoveStepLeftWarning;
    [Tooltip("���ʣ���ƶ�������ʾ�ı���ʧʱ��")] public float playerMoveStepLeftWarningFadeTime;

    Dictionary<int, int> levelToStepDic = new Dictionary<int, int>();
    Dictionary<int, int> levelToWin = new Dictionary<int, int>();

    private void Start()
    {
        time = 0;
        levelToStepDic.Add(2, 18); //������Ӧ���ò�����level01
        levelToStepDic.Add(3, 50); //������Ӧ���ò�����level02
        levelToStepDic.Add(4, 100); //������Ӧ���ò�����level03
        levelToStepDic.Add(5, 150); //������Ӧ���ò�����level04
        levelToStepDic.Add(6, 999); //������Ӧ���ò�����level05
    }

    void InitBoxes()
    {
        GameObject[] boxes = GameObject.FindGameObjectsWithTag("Box");
        totalBoxCount = boxes.Length;
    }

    
    void Update()
    {
        if (CheckLevel()) // ��ʽ�ؿ���������
        {
            CheckGameRevert();
            UpdateTime();
            CheckStepsLeft();
        }
    }

    bool CheckLevel()
    {
        int nowLevel = SceneManager.GetActiveScene().buildIndex;
        if (nowLevel < 2)
        {
            return false;
        }

        return true;
    }

  

    public void CheckPassLevel()
    {
        if (currentBoxCount >= totalBoxCount)
        {
            Debug.Log("Win!");
            // UIManager.Instance.winText.gameObject.SetActive(true);
            UIManager.Instance.ShowPassInfo();
            // Invoke("LoadGame", 2f);
            StartCoroutine(PassLevel());
        }
    }

    IEnumerator PassLevel()
    {
        float t = 2;
        while (t > 0)
        {
            Time.timeScale = 0;
            t -= Time.unscaledDeltaTime;
            yield return null;
        }

        Time.timeScale = 1;
        LoadGame();
    }

    /// <summary>
    /// ����ʱ��
    /// </summary>
    void UpdateTime()
    {
        time = ((int)Time.time);
        UIManager.Instance.timeText.text = "Time:" + time;
    }

    /// <summary>
    /// ���ùؿ�
    /// </summary>
    void CheckGameRevert()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            reset();
            UIManager.Instance.reset();
            Time.timeScale = 1f;
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }

    /// <summary>
    /// ��ȡ��һ��
    /// </summary>
    public void LoadGame(int level = 1)
    {
        int nextLevel = SceneManager.GetActiveScene().buildIndex + level;
        if (levelToStepDic.ContainsKey(nextLevel))
        {
            levelToStepDic.TryGetValue(nextLevel, out playerMoveStepLeft);
        }

        reset();
        SceneManager.LoadScene(nextLevel);
    }

    /// <summary>
    /// ��������
    /// </summary>
    void reset()
    {
        if (CheckLevel())
        {
            playerMoveStep = 0;
            currentBoxCount = 0;
            totalBoxCount = 0;
            time = 0;
            int nowLevel = SceneManager.GetActiveScene().buildIndex;
            levelToStepDic.TryGetValue(nowLevel, out playerMoveStepLeft); // ʣ�ಽ��
            UIManager.Instance.gameOverText.gameObject.SetActive(false);
            // UIManager.Instance.winText.gameObject.SetActive(false);
            UIManager.Instance.UnShowPassInfo();
        }
    }

    public void GameOver()
    {
        UIManager.Instance.gameOverText.gameObject.SetActive(true);
        Time.timeScale = 0f;
    }

    public void DecreaseStepLeft()
    {
        if (playerMoveStepLeft != 0)
        {
            playerMoveStepLeft--;
            if (playerMoveStepLeft <= playerMoveStepLeftWarning)
            {
                UIManager.Instance.ShowStepLeftWarning();
            }
        }
    }*/

    /// <summary>
    /// �ؿ�����
    /// </summary>
    public void PassCheck()
    {
        //ֹͣ���棬�򿪽������
        Time.timeScale = 0;
        
        GameObject basePanel=GameObject.Find("BasePanel");
        GameObject PassMaskPanel = basePanel.transform.Find("PassMaskPanel").gameObject;
        PassMaskPanel.SetActive(true);

      
    }

    /// <summary>
    /// ���ùؿ�
    /// </summary>
    public void ResetThisGame()
    {
        Debug.Log("����" + SceneManager.s_strLoadedSceneName);
        SceneManager.ReloadScene(SceneManager.s_strLoadedSceneName);


    }

    public void startGame()
    {
        Time.timeScale = 1;
    }

    public void SaveGame()
    {
        SaveByXML();
    }

    /// <summary>
    /// ����Ϸ����д��XML������ҵ���߷���
    /// </summary>
    private void SaveByXML()
    {
        
        Save save = createSaveGameObject();
        XmlDocument xmlDocument = new XmlDocument();

        //���û��xml�ļ����½�xml�ļ������ڵ��Ǳ����
        if (!File.Exists(Application.dataPath + "/DataXML_level.text"))
        {
            XmlElement root = xmlDocument.CreateElement("Save");
            root.SetAttribute("FileName", "File_levelData");
            xmlDocument.AppendChild(root);
            xmlDocument.Save(Application.dataPath + "/DataXML_level.text");
        }

        //����һ��xml�ļ�
        xmlDocument.Load(Application.dataPath + "/DataXML_level.text");

        
        #region CreateXML elements

        //���ҹؿ��浵
        XmlNodeList levelNode = xmlDocument.GetElementsByTagName("level"+ save.level.ToString());
        
        //��ǰ�Ǵ浵���Ĺؿ���ˢ��һ�¼�¼
        //TODO:����������滻
        if (levelNode.Count>0)
        {
            Debug.Log("levelNode.Count>0  "+ levelNode.Count);


            XmlNodeList levelChildList = levelNode[0].ChildNodes;

            foreach(XmlNode childNode in levelChildList)
            {
                XmlElement childElement = (XmlElement)childNode;//���ڵ�ת��һ������
                if (childElement.Name== "money")
                {
                    childElement.InnerText = save.money.ToString();
                }
                else if(childElement.Name == "star")
                {
                    childElement.InnerText = save.star.ToString();
                }
            }
            xmlDocument.Save(Application.dataPath + "/DataXML_level.text");
        }
        //��ǰ��δ�浵���Ĺؿ����½��浵
        else
        {
           //�Ҹ��ڵ�
            XmlNode root = xmlDocument.SelectSingleNode("Save");
            Debug.Log("Roottttttttttt:" + root.Name);

            //�½�level,�Լ�level���ӽڵ㣺��ǰ����������
            XmlElement levelElement = xmlDocument.CreateElement("level" + save.level.ToString());
            
            XmlElement moneyElement = xmlDocument.CreateElement("money");
            moneyElement.InnerText = save.money.ToString();
            levelElement.AppendChild(moneyElement);

            XmlElement starElement = xmlDocument.CreateElement("star");
            starElement.InnerText = save.star.ToString();
            levelElement.AppendChild(starElement);

            root.AppendChild(levelElement);

            //����
            xmlDocument.Save(Application.dataPath + "/DataXML_level.text");
        }


        #endregion

        Debug.Log("XML FILE SAVED");
    }



    /// <summary>
    /// �ӳ����л�ȡҪͬ������Ϸ����
    /// </summary>
    /// <returns>saveʵ��</returns>
    private Save createSaveGameObject()
    {
        Save save = new Save();
        save.level = level;
        save.money = money;
        save.star = star;
        

        return save;
    }

    /// <summary>
    /// ������Ϸ
    /// </summary>
    public void LoadGame()
    {
        LoadByXML();
    }

    /// <summary>
    /// ��ȡXML��������Ϸ���ȣ�����ҵ���߷���
    /// </summary>
    private void LoadByXML()
    {
        //�ҵ���Ϸ�����ļ�
        if (File.Exists(Application.dataPath + "/DataXML_level.text"))
        {

            Save save = createSaveGameObject();
            XmlDocument xmlDocument = new XmlDocument();
            xmlDocument.Load(Application.dataPath + "/DataXML_level.text");

            //ͨ���ؿ���ţ��ҵ�XML�еĽڵ�,����ֵ����save,ͬ����GameManager
            XmlNodeList levelNodeList = xmlDocument.GetElementsByTagName("level" + save.level.ToString());

            Debug.Log("save.level"+ save.level);

            XmlNodeList levelChildList = levelNodeList[0].ChildNodes;

            foreach (XmlNode childNode in levelChildList)
            {
                XmlElement childElement = (XmlElement)childNode;//���ڵ�ת��һ������
                if (childElement.Name == "money")
                {
                    int money = int.Parse(childElement.InnerText);
                    save.money = money;
                }
                else if (childElement.Name == "star")
                {
                    int star = int.Parse(childElement.InnerText);
                    save.star = star;
                }
            }

            this.money = save.money;
            this.star = save.star;
            Debug.Log("File Loaded");
        }
        else
        {
            Debug.Log("Not Founded File");
        }

    }
   
}