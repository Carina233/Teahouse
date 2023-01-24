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
    

    Animator anim;//角色动画
 
    //ToolTip可以在Inspector上显示变量的注释
    [Tooltip("检测能否移动的相距碰撞层距离")] float layerCheckDistance_collider; // 检测是否能移动的相距碰撞层距离
    [Tooltip("检测影响移动的碰撞层")] public LayerMask checkLayer_collider; // 检测影响移动的碰撞层

    [Tooltip("检测某距离内是否有食物")] float layerCheckDistance_food; // 检测某距离内是否有食物
    [Tooltip("检测的食物类型")] public LayerMask checkLayer_food; // 检测的食物类型

    [Tooltip("检测某距离内是否有位置放东西")] float layerCheckDistance_place; // 检测某距离内是否有位置放东西
    [Tooltip("检测的位置类型")] public LayerMask checkLayer_place; // 检测的位置类型

    [Tooltip("检测某距离内是否有碟子放东西")] float layerCheckDistance_dish; // 检测某距离内是否有食物
    [Tooltip("检测的碟子类型")] public LayerMask checkLayer_dish; // 检测的碟子类型

    public GameObject real; //真实世界
    public LayerMask tableLayer;
    public Tilemap realTilemap; // 引用的Tilemap
    List<GameObject> realGameObjectList; // real中所有物体列表
   
    
    private void Start()
    {
        //rb = GetComponent<Rigidbody2D>();
        //anim = GetComponent<Animator>();
        
        moveDir = Vector3.zero;
        layerCheckDistance_collider = 0.5f;
        layerCheckDistance_place = 1.0f;
        layerCheckDistance_food = 1.0f;
        layerCheckDistance_dish = 1.0f;
    }

    void Update()
    {
       
        Move();
        controlFood();
        controlWarehouse();
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
            //Debug.Log("right");
        }

        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
          
            moveDir = Vector2.left;
            playStepSound();
            setWalkAnim(moveDir);
            //Debug.Log("left");
        }

        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
           
            moveDir = Vector2.up;
            playStepSound();
            setWalkAnim(moveDir);
            //Debug.Log("up");
        }

        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            //Debug.Log("down");
            
            moveDir = Vector2.down;
            playStepSound();
            setWalkAnim(moveDir);
        }

        
        
        if(!MoveCheck(moveDir))
        {
            float horizontal = Input.GetAxis("Horizontal");
            float vertical = Input.GetAxis("Vertical");

            Vector2 position = transform.position;
            position.x = position.x + moveSpeed * horizontal * Time.deltaTime;
            position.y = position.y + moveSpeed * vertical * Time.deltaTime;

            transform.position = position;
        }
    }

    
    void setWalkAnim(Vector2 dir)
    {
        //anim.SetFloat("Walk", 1);
        //anim.SetFloat("X", dir.x);
        //anim.SetFloat("Y", dir.y);
    }

    /// <summary>
    /// 移动检测,如果true无障碍可继续移动，障碍有墙壁装饰，桌子，设备，上菜台
    /// </summary>
    /// <param name="dir"></param>
    /// <returns></returns>
    private GameObject MoveCheck(Vector2 dir)
    {
        //搬运过来的Tooltip
        //[Tooltip("检测能否移动的相距碰撞层距离")] float layerCheckDistance; // 检测是否能移动的相距碰撞层距离
        //[Tooltip("检测影响移动的碰撞层")] public LayerMask checkLayer; // 检测影响移动的碰撞层
        //layerCheckDistance = 0.5f; 意思就是检测角色前面0.5f处有没有碰撞物
        //为TileMap Wall新建一个Layer Wall，然后checkLayer中选中Wall，代表检测的目标是墙壁
        //checkLayer_collider只选定Layermask层内的碰撞器，其它层内碰撞器忽略
        
        RaycastHit2D hit = Physics2D.Raycast(transform.position, dir, layerCheckDistance_collider, checkLayer_collider);//射线检测
        //Debug.Log(dir+"hit"+ hit.collider);
        if (!hit)
        {
            //Debug.Log(dir + "啥也没有");
            //Debug.Log("物体transform.position:"+ transform.position+"朝向dir:"+dir+ "，检测的碰撞物为checkLayer:"+ checkLayer_collider + "但还可以走");
            return null;
        }
        else
        {
            Debug.Log("hit.collider.gameObject" + hit.collider.gameObject);
            return hit.collider.gameObject;
        }

    }


    /// <summary>
    /// 播放音效
    /// </summary>
    public void playStepSound()
    {
        //((AudioSource)this.gameObject.GetComponent(typeof(AudioSource))).Play();
    }

    /// <summary>
    /// 从仓库取物
    /// </summary>
    public void controlWarehouse()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {

            //手上有没有东西
            GameObject food = foodCheck(moveDir);
            if (food||checkHand())
            {
                return;
            }
            //前面是仓库吗，是的话就向仓库请求取出物品
            GameObject placeType = placeCheck(moveDir);
            if(!placeType)
            {
                return;
            }
            if (placeType.name=="Warehouse")
            {
                placeType.GetComponent<DeviceManager>().requirement = 1;
                placeType.GetComponent<DeviceManager>().requirementOwner = gameObject;
            }
        }
    }
    /// <summary>
    /// 查看是否有碟子/碗
    /// </summary>
    public GameObject checkDish(Vector2 dir)
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, dir, layerCheckDistance_dish, checkLayer_dish);//射线检测
        //Debug.Log(dir + "placehit" + hit.collider);
        if (!hit)
        {
            Debug.Log(dir + "没有diezi");
            //Debug.Log("物体transform.position:"+ transform.position+"朝向dir:"+dir+ "，检测的碰撞物为checkLayer:"+ checkLayer_collider + "但还可以走");
            return null;
        }
        Debug.Log("有碟子:" + hit.collider.gameObject);
        return hit.collider.gameObject;
    }

  



    /// <summary>
    /// 拿起/放下食物
    /// </summary>
    public void controlFood()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            GameObject foodInHand = checkHand(); 
            GameObject placeType = placeCheck(moveDir);
            GameObject dish = checkDish(moveDir);
            GameObject food = foodCheck(moveDir);
            Debug.Log(moveDir+"placeType"+placeType);


            if (foodInHand)//手上有东西
            {
                if (foodInHand.transform.name == "Dish")
                {
                    foodInHand.layer = LayerMask.NameToLayer("Dish");
                }
              
                
                //前面有盘子，放盘子里，再加一个且可以放盘子里的判定,单个的物体不能放盘子
                if(dish!=null)
                {
                    if(foodInHand.GetComponent<FoodManager>().stackable == false)
                    {
                        return;
                    }
                    
                    Debug.Log("把东西放到碟子里呀");
                    foodInHand.transform.SetParent(dish.transform);
                    foodInHand.transform.position = dish.transform.position;

                    dish.transform.GetComponent<DishController>().checkMix = true;
                    

                    //foodInHand.layer = LayerMask.NameToLayer("Food");
                    return;
                }
                //前面没有盘子，但是有桌面，放桌面上，再加一个且食物被可以放桌子上的判定
                if (!placeType)
                {
                    return;
                }
                switch (LayerMask.LayerToName(placeType.layer))//根据层级获取名称
                {

                    case "Table"://普通放置
                        foodInHand.transform.SetParent(placeType.transform);
                        foodInHand.transform.position = placeType.transform.position;
                        //foodInHand.layer = LayerMask.NameToLayer("Food");
                        break;
                    case "Device"://放到烹饪工具中
                        foodInHand.transform.SetParent(placeType.transform);
                        foodInHand.transform.position = placeType.transform.position;
                        //foodInHand.layer = LayerMask.NameToLayer("Food");
                        break;
                    case "OutputCheck"://放到出菜口
                        foodInHand.transform.SetParent(placeType.transform);
                        foodInHand.transform.position = placeType.transform.position;
                        //foodInHand.layer = LayerMask.NameToLayer("Food");
                        break;
                    
                }
                
              
            }
            else//手上没东西
            {
                if (dish)
                {
                    //面前有东西就拿起来
                    dish.transform.SetParent(this.transform);
                    Debug.Log("gameObject.layer" + gameObject.layer);
                    dish.layer = gameObject.layer;
                    return;
                }
                if (food)
                {
                    //面前有东西就拿起来
                    food.transform.SetParent(this.transform);
                    Debug.Log("gameObject.layer" + gameObject.layer);
                    //food.layer = gameObject.layer;
                    return;
                }
            }
            
        }
    }

    /// <summary>
    /// 检查玩家手上有无东西
    /// </summary>
     GameObject checkHand()
    {
        int length = GetComponentsInChildren<Transform>(true).Length;
        if (length <= 1)//根节点自己算一个transform
        {
            Debug.Log("没有子物体");
            return null;
        }

        Transform [] childList=new Transform[length+1];
        int i = 0;
        foreach (Transform child in transform)
        {
            childList[i++] = child;
            Debug.Log("i:"+i+"name:"+child.name);
        }

        return childList[0].gameObject;
    }
    /// <summary>
    /// 物品放置位置检测
    /// </summary>
    /// <param name="dir"></param>
    /// <returns></returns>
    GameObject placeCheck(Vector2 dir)
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, dir, layerCheckDistance_place, checkLayer_place);//射线检测
        //Debug.Log(dir + "placehit" + hit.collider);
        if (!hit)
        {
            Debug.Log(dir + "没有place");
            //Debug.Log("物体transform.position:"+ transform.position+"朝向dir:"+dir+ "，检测的碰撞物为checkLayer:"+ checkLayer_collider + "但还可以走");
            return null;
        }
        Debug.Log("place:" + hit.collider.gameObject);
        return hit.collider.gameObject;
    }

    /// <summary>
    /// 物品检测，true就是有
    /// </summary>
    /// <param name="dir"></param>
    /// <returns></returns>
    GameObject foodCheck(Vector2 dir)
    {
        //搬运过来的Tooltip
        //[Tooltip("检测能否移动的相距碰撞层距离")] float layerCheckDistance; // 检测是否能移动的相距碰撞层距离
        //[Tooltip("检测影响移动的碰撞层")] public LayerMask checkLayer; // 检测影响移动的碰撞层
        //layerCheckDistance = 0.5f; 意思就是检测角色前面0.5f处有没有碰撞物
        //为TileMap Wall新建一个Layer Wall，然后checkLayer中选中Wall，代表检测的目标是墙壁
        //Debug.Log(dir);
        RaycastHit2D hit = Physics2D.Raycast(transform.position, dir, layerCheckDistance_food, checkLayer_food);//射线检测
        if (hit)
        {
            Debug.Log("角色位置transform.position:" + transform.position + "朝向dir:" + dir + "，检测的食物为checkLayer:" + checkLayer_food + "有东西可以捡:" + hit.collider.name);
            return hit.collider.gameObject;
        }
        return null;
    }
}