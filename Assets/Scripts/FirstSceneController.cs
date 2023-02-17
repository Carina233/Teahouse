using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstSceneController : MonoBehaviour
{
    GameObject canvas;
    // Start is called before the first frame update
    void Start()
    {
        canvas = GameObject.Find("Canvas");
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


    

    /// <summary>
    /// �˳���Ϸ
    /// </summary>
    public void ExitThisApp()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif


    }

    
}
