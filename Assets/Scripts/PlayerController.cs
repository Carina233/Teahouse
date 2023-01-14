using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed;//角色移动速度
    public float moveStep;//角色移动步数
    public Rigidbody2D rb;//角色Rigidbody
    Vector2 moveDir; //移动方向（即将移动）
    bool isMoving;//角色是否在移动

    Animator anim;//角色动画
 
    //ToolTip可以在Inspector上显示变量的注释
    [Tooltip("检测能否移动的相距碰撞层距离")] float layerCheckDistance; // 检测是否能移动的相距碰撞层距离
    [Tooltip("检测影响移动的碰撞层")] public LayerMask checkLayer; // 检测影响移动的碰撞层

    public GameObject real; //真实世界
    public LayerMask tableLayer;
    public Tilemap realTilemap; // 引用的Tilemap
    List<GameObject> realGameObjectList; // real中所有物体列表
   
    
    private void Start()
    {
        //rb = GetComponent<Rigidbody2D>();
        //anim = GetComponent<Animator>();
        layerCheckDistance = 0.5f;
    }

    void Update()
    {
        Move();
    }

    /// <summary>
    /// 移动
    /// </summary>
    void Move()
    {
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            moveDir = Vector2.right;
            playStepSound();
            setWalkAnim(moveDir);
        }

        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            moveDir = Vector2.left;
            playStepSound();
            setWalkAnim(moveDir);
        }

        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            moveDir = Vector2.up;
            playStepSound();
            setWalkAnim(moveDir);
        }

        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            moveDir = Vector2.down;
            playStepSound();
            setWalkAnim(moveDir);
        }

        if (moveDir != Vector2.zero) // pressed key to move
        {
            Debug.Log(moveDir);
            if (!isMoving)
            {
                StartCoroutine(MoveToDir(moveDir));
            }
        }

        moveDir = Vector2.zero;
    }

    
    void setWalkAnim(Vector2 dir)
    {
        //anim.SetFloat("Walk", 1);
        //anim.SetFloat("X", dir.x);
        //anim.SetFloat("Y", dir.y);
    }

    /// <summary>
    /// 移动检测,如果true无障碍可继续移动
    /// </summary>
    /// <param name="dir"></param>
    /// <returns></returns>
    bool MoveCheck(Vector2 dir)
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, dir, layerCheckDistance, checkLayer);
        if (!hit)
        {
            Debug.Log("物体transform.position:"+ transform.position+"朝向dir:"+dir+ "，检测的碰撞物为checkLayer:"+ checkLayer+"但还可以走");
            return true;
        }
        return false;
    }

    /// <summary>
    /// 向某方向移动单位长度
    /// </summary>
    /// <param name="dir"></param>
    IEnumerator MoveToDir(Vector2 dir)
    {
        // transform.Translate(dir);
        isMoving = true;
        Debug.Log("isMoving:"+ isMoving);

        Vector2 targetPos = transform.position + (Vector3)dir * moveStep;

        while (MoveCheck(dir)&&Vector3.Distance(transform.position, targetPos) > 0.005f)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPos, moveSpeed * Time.deltaTime);
            //TODO:遇到碰撞物以后玩家位置会有一点点偏移，大概0.01-0.03
           yield return null;
            
        }

        isMoving = false;

        
        //anim.SetFloat("Walk", 0);

    }

    ///
    /// 播放音乐
    ///
    public void playStepSound()
    {
        //((AudioSource)this.gameObject.GetComponent(typeof(AudioSource))).Play();
    }
}