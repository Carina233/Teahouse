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
    /// ��ת����Ϸ����
    /// </summary>
    public void TurnToGame()
    {
        int levelNum = getLevelNum();
        SceneManager.LoadScene("GameScene");
        
    }

    /// <summary>
    /// ��ȡ�ؿ����
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
