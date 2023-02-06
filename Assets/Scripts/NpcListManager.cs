using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcListManager : MonoBehaviour
{
    private static NpcListManager instance;

    public static NpcListManager Instance
    {
        get
        {
            if (instance == null || !instance.gameObject)
                instance = FindObjectOfType<NpcListManager>();
            return instance;
        }
    }
    [SerializeField]
    public List<NPCDatalist_SO> npcList;
}
