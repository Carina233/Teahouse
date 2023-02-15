using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missile : MonoBehaviour
{
   
    
    [SerializeField]
    private float speed=5f;

    [HideInInspector]
    public GameObject target;
    
    public void Move(GameObject target)
    {
        
        gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, target.transform.position, speed * Time.deltaTime);
    }

    public void Boom(GameObject target)
    {
        if (target == null|| Vector3.Distance(this.transform.position, target.transform.position)<1f)
        {
            Debug.Log("BOOM!");
            Destroy(this.gameObject);
        }
    }

    
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Move(target);

    }

    // return the top-most (root) parent GameObject that contains "go"
    // 返回go物体的根节点
    GameObject GetTopMostParent(GameObject go)
    {
        Transform root;
        root = go.transform;
        while (root.transform.parent!=null)
        {
            root = root.transform.parent;
        }
        return root.gameObject;
    }



}
