using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSceneController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        //�����ã���ʽɾ
        GameObject objSceneManager = new GameObject("SceneManager");
        
        SceneManager.CreateInstance(objSceneManager);

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /// <summary>
    /// ��ת���ؿ�����
    /// </summary>
    public void TurnToLevel()
    {
        SceneManager.LoadScene("LevelScene");
        
        
    }

    
}
