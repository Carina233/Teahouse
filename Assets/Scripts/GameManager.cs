using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
    [Tooltip("关卡")] public int level;
    [Tooltip("时间")] public int time;
    [Tooltip("成功订单数")] public int successNum;
    [Tooltip("失败订单数")] public int failNum;
    [Tooltip("总计收入")] public int money;
    [Tooltip("等级划分")] public List<int> starLevel;
    [Tooltip("达标星数")] public int star;

    /*public int time;
    [Tooltip("总共箱子个数")] public int totalBoxCount;
    [Tooltip("当前已完成箱子个数")] public int currentBoxCount;
    [Tooltip("玩家移动步数")] public int playerMoveStep = 0;
    [HideInInspector] public int playerMoveStepLeft;
    [Tooltip("玩家剩余移动步数提示阈值")] public int playerMoveStepLeftWarning;
    [Tooltip("玩家剩余移动步数提示文本消失时间")] public float playerMoveStepLeftWarningFadeTime;

    Dictionary<int, int> levelToStepDic = new Dictionary<int, int>();
    Dictionary<int, int> levelToWin = new Dictionary<int, int>();

    private void Start()
    {
        time = 0;
        levelToStepDic.Add(2, 18); //场景对应可用步数，level01
        levelToStepDic.Add(3, 50); //场景对应可用步数，level02
        levelToStepDic.Add(4, 100); //场景对应可用步数，level03
        levelToStepDic.Add(5, 150); //场景对应可用步数，level04
        levelToStepDic.Add(6, 999); //场景对应可用步数，level05
    }

    void InitBoxes()
    {
        GameObject[] boxes = GameObject.FindGameObjectsWithTag("Box");
        totalBoxCount = boxes.Length;
    }

    
    void Update()
    {
        if (CheckLevel()) // 正式关卡才起作用
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
    /// 更新时间
    /// </summary>
    void UpdateTime()
    {
        time = ((int)Time.time);
        UIManager.Instance.timeText.text = "Time:" + time;
    }

    /// <summary>
    /// 重置关卡
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
    /// 读取下一关
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
    /// 重置数据
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
            levelToStepDic.TryGetValue(nowLevel, out playerMoveStepLeft); // 剩余步数
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
    /// 关卡结算
    /// </summary>
    public void PassCheck()
    {
        Time.timeScale = 0;
        GameObject basePanel=GameObject.Find("BasePanel");
        GameObject PassMaskPanel = basePanel.transform.Find("PassMaskPanel").gameObject;
        PassMaskPanel.SetActive(true);

      
    }

    /// <summary>
    /// 重置关卡
    /// </summary>
    public void ResetThisGame()
    {
        Debug.Log("重载" + SceneManager.s_strLoadedSceneName);
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
    private void SaveByXML()
    {
        Save save = createSaveGameObject();
        XmlDocument xmlDocument = new XmlDocument();
        
        if (!File.Exists(Application.dataPath + "/DataXML_level.text"))
        {
            XmlElement root = xmlDocument.CreateElement("Save");
            root.SetAttribute("FileName", "File_levelData");
            xmlDocument.AppendChild(root);
            xmlDocument.Save(Application.dataPath + "/DataXML_level.text");
        }

        //XmlNode rootNode = xmlDocument.FirstChild;
        //test
       /* XmlElement starElement = xmlDocument.CreateElement("star");
        starElement.InnerText = save.star.ToString();
        rootNode.AppendChild(starElement);*/

        //xmlDocument.Save(Application.dataPath + "/DataXML_level.text");

        xmlDocument.Load(Application.dataPath + "/DataXML_level.text");

        
        #region CreateXML elements

        
        XmlNodeList levelNode = xmlDocument.GetElementsByTagName("level"+ save.level.ToString());
        
        if (levelNode.Count>0)
        {
            Debug.Log("levelNode.Count>0  "+ levelNode.Count);


            XmlNodeList levelChildList = levelNode[0].ChildNodes;

            foreach(XmlNode childNode in levelChildList)
            {
                XmlElement childElement = (XmlElement)childNode;//将节点转换一下类型
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
        else
        {
           
            XmlNode root = xmlDocument.SelectSingleNode("Save");
            Debug.Log("Roottttttttttt:" + root.Name);

            XmlElement levelElement = xmlDocument.CreateElement("level" + save.level.ToString());
            

            XmlElement moneyElement = xmlDocument.CreateElement("money");
            moneyElement.InnerText = save.money.ToString();
            levelElement.AppendChild(moneyElement);

            

            XmlElement starElement = xmlDocument.CreateElement("star");
            starElement.InnerText = save.star.ToString();
            levelElement.AppendChild(starElement);

            root.AppendChild(levelElement);


            xmlDocument.Save(Application.dataPath + "/DataXML_level.text");
        }


        #endregion

        Debug.Log("XML FILE SAVED");
    }



    private Save createSaveGameObject()
    {
        Save save = new Save();
        save.level = level;
        save.money = money;
        save.star = star;
        

        return save;
    }

    public void LoadGame()
    {
        LoadByXML();
    }
    private void LoadByXML()
    {
        if (File.Exists(Application.dataPath + "/DataXML_level.text"))
        {

            Save save = createSaveGameObject();
            XmlDocument xmlDocument = new XmlDocument();
            xmlDocument.Load(Application.dataPath + "/DataXML_level.text");

            XmlNodeList levelNodeList = xmlDocument.GetElementsByTagName("level" + save.level);

            Debug.Log("save.level"+ save.level);

            XmlNodeList levelChildList = levelNodeList[0].ChildNodes;

            foreach (XmlNode childNode in levelChildList)
            {
                XmlElement childElement = (XmlElement)childNode;//将节点转换一下类型
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