using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
    public int time;
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
    }
}