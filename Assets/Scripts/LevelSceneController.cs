using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSceneController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /// <summary>
    /// 跳转到最开始界面
    /// </summary>
    public void TurnToFirst()
    {
        SceneManager.LoadScene("FirstScene");
    }
}
