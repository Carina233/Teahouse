using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour
{
    public int levelNum;
    private int _level;
    // Start is called before the first frame update
    void Start()
    {
        _level = levelNum;
    }

    /// <summary>
    /// 跳转到游戏界面
    /// </summary>
    public void TurnToGame()
    {
        int levelNum = getLevelNum();
        SceneManager.LoadScene("GameScene");
        
    }

    /// <summary>
    /// 获取关卡编号
    /// </summary>
    public int getLevelNum()
    {
       // _level = levelNum;
        return _level;
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
