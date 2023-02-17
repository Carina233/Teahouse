using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSceneController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        //测试用，正式删
        GameObject objSceneManager = new GameObject("SceneManager");
        
        SceneManager.CreateInstance(objSceneManager);

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /// <summary>
    /// 跳转到关卡界面
    /// </summary>
    public void TurnToLevel()
    {
        SceneManager.LoadScene("LevelScene");
        
        
    }

    
}
