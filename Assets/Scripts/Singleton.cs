using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 单例模板类
/// </summary>
/// <typeparam name="T"></typeparam>
public class Singleton<T> : MonoBehaviour where T : Singleton<T>
{
    public static T Instance { get; private set; }

    protected virtual void Awake()
    {
        if (Instance == null)
        {
            Instance = (T)this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}