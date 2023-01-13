using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
    public int time;
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

    void CheckStepsLeft()
    {
        if (playerMoveStepLeft <= 0)
        {
            GameOver();
        }
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
    }
}