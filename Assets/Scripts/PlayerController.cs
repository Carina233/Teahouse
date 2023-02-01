using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PlayerController : MonoBehaviour
{
    private InHandState inHandState;
    private PlaceState placeState;
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

    [Tooltip("检测某距离内是否有容器放东西")] float layerCheckDistance_dish; // 检测某距离内是否有容器
    [Tooltip("检测的容器类型")] public LayerMask checkLayer_dish; // 检测的容器类型

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
    public void controlWarehouse(GameObject placeType)
    {
        
         placeType.GetComponent<DeviceManager>().requirement = 1;
         placeType.GetComponent<DeviceManager>().requirementOwner = gameObject;
            
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
            Debug.Log(dir + "没有容器");
            //Debug.Log("物体transform.position:"+ transform.position+"朝向dir:"+dir+ "，检测的碰撞物为checkLayer:"+ checkLayer_collider + "但还可以走");
            return null;
        }
        Debug.Log("有容器:" + hit.collider.gameObject);
        return hit.collider.gameObject;
    }


    private enum InHandState {
    
        none,
        EmptyDish,
        FullDish,
        EmptyContainer,
        MixContainer,
        EmptyCooker,
        FullCooker,
        SingleFood,
        StackableFood,
  
    
    }

    private enum PlaceState
    {
        none,
        EmptyDish,
        FullDish,
        EmptyCooker,
        FullCooker,
        MixContainer,
        EmptyContainer,

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
            inHandState = InHandState.none;
            placeState = PlaceState.none;

            if (foodInHand)//手上有东西
            {
               
                //手上是碟子
                if (foodInHand.transform.name== "Dish")
                {
                    //是空碟子吗
                    if(foodInHand.transform.GetChild(0).childCount==0)//是
                    {
                        inHandState = InHandState.EmptyDish;
                    }
                    else//不是
                    {
                        //foodInHand.layer = LayerMask.NameToLayer("Dish");
                        inHandState = InHandState.FullDish;
                    }
                }
                else if (foodInHand.transform.name == "MixingContainer")
                {
                    //是空吗
                    if (foodInHand.transform.GetChild(0).childCount == 0)//是
                    {
                        inHandState = InHandState.EmptyContainer;
                    }
                    else//不是
                    {
                        Debug.Log("我拿着非空混合物容器");
                        inHandState = InHandState.MixContainer;
                    }
                }
                else if (foodInHand.transform.name == "Cooker")//是厨具
                {
                    //是空厨具吗
                    if (foodInHand.transform.GetChild(0).childCount == 0)//是
                    {
                        inHandState = InHandState.EmptyCooker;
                       
                    }
                    else//不是
                    {
                        inHandState = InHandState.FullCooker;
                    }
                }
                else if (foodInHand.layer == LayerMask.NameToLayer("Food"))//是食物
                {
                    //可混合吗
                    if (foodInHand.GetComponent<FoodManager>().stackable == false)
                    {
                        inHandState = InHandState.SingleFood;
                    }
                    else
                    {
                        inHandState = InHandState.StackableFood;
                    }
                }
            }
            else//手上没东西
            {
                
                //先判断容器包括厨具，空，非空都可以拿
                if (dish)
                {
                    //面前有东西就拿起来
                    dish.transform.SetParent(this.transform);
                    Debug.Log("gameObject.layer" + gameObject.layer);
                    dish.layer = gameObject.layer;
                    return;
                }
                //再判断厨具，空厨具，非空厨具都可以拿

                //判断食物
                if (food)
                {
                    //面前有东西就拿起来
                    food.transform.SetParent(this.transform);
                    Debug.Log("gameObject.layer" + gameObject.layer);
                    
                    return;
                }

                //仓库里的东西可以拿
                if(placeType&&placeType.transform.name=="Warehouse")
                {
                    controlWarehouse(placeType);
                    return;
                }
            }

            /////////////////////////////
            //前面有什么地方

            //前面有容器
            if (dish != null)
            {
                //是碟子
                if (dish.layer == LayerMask.NameToLayer("Dish"))
                {
                    //是空碟子吗
                    if (dish.transform.GetChild(0).childCount == 0)//是
                    {
                        placeState = PlaceState.EmptyDish;
                    }
                    else//不是
                    {
                        placeState = PlaceState.FullDish;
                        //foodInHand.layer = LayerMask.NameToLayer("Dish");
                    }
                }

                /*Debug.Log("把东西放到碟子里呀");
                foodInHand.transform.SetParent(dish.transform.GetChild(0));
                foodInHand.transform.position = dish.transform.GetChild(0).position;

                dish.transform.GetChild(0).GetComponent<FoodMixtureController>().checkMix = true;

                return;*/

                //什么东西可以放进空碟子，可以放到碟子的食物，空碟子，非空碟子里面的食物。非空厨具里面的食物。
                else if (dish.layer == LayerMask.NameToLayer("Cooker"))
                {
                    //是空厨具吗
                    if (dish.transform.GetChild(0).childCount == 0)//是
                    {

                        Debug.Log("前方有空厨具");
                        placeState = PlaceState.EmptyCooker;
                        //check
                    }
                    else//不是
                    {
                        placeState = PlaceState.FullCooker;
                    }
                    //什么东西可以放进空厨具，非空碟子里面的食物。非空厨具里面的食物。

                }
                else if (dish.layer == LayerMask.NameToLayer("MixingContainer"))
                {
                    //是空混合容器吗
                    if (dish.transform.GetChild(0).childCount == 0)//是
                    {
                        placeState = PlaceState.EmptyContainer;
                    }
                    else//不是
                    {
                        placeState = PlaceState.MixContainer;
                    }
                   

                }

            }
           

            //前面啥地儿也没有，滚
            else if (!placeType)
            {
                return;
            }
            //啥地儿
            else
            {
                switch (LayerMask.LayerToName(placeType.layer))//根据层级获取名称
                {

                    case "Table"://普通放置桌子
                        foodInHand.transform.SetParent(placeType.transform);
                        foodInHand.transform.position = placeType.transform.position;
                        if(foodInHand.layer==LayerMask.NameToLayer("Default"))
                        {
                            foodInHand.layer = LayerMask.NameToLayer(foodInHand.transform.name);
                        }
                        break;
                    case "Device"://无厨具厨具桌,只有厨具可以放上来
                        if (foodInHand.transform.name=="Cooker")
                        {
                            foodInHand.transform.SetParent(placeType.transform);
                            foodInHand.transform.position = placeType.transform.position;
                            foodInHand.layer = LayerMask.NameToLayer(foodInHand.transform.name);

                            //check
                        }
                        break;
                    case "OutputCheck"://放到出菜口
                        if (foodInHand.name=="Dish"&&foodInHand.transform.GetChild(0).childCount!=0)
                        {
                            foodInHand.transform.SetParent(placeType.transform);
                            foodInHand.transform.position = placeType.transform.position;
                        }
                        else if(inHandState==InHandState.SingleFood)
                        {
                            foodInHand.transform.SetParent(placeType.transform);
                            foodInHand.transform.position = placeType.transform.position;
                        }

                       
                        break;

                    case "WasteBin"://放到垃圾桶
                        if(foodInHand.layer==LayerMask.NameToLayer("Food"))
                        {
                            foodInHand.transform.SetParent(placeType.transform);
                            foodInHand.transform.position = placeType.transform.position;
                        }
                        else if(foodInHand.transform.GetChild(0).childCount>0)
                        {
                            for (int i = foodInHand.transform.GetChild(0).childCount - 1; i >= 0; i--)
                            {
                                foodInHand.transform.GetChild(0).GetChild(i).position = placeType.transform.position;
                                foodInHand.transform.GetChild(0).GetChild(i).SetParent(placeType.transform);
                            }
                        }

                        break;
                }
            }

            ////////根据当前拿的东西（碟子/食物/厨具/混合容器）和面前的东西（碟子，厨具，混合容器）进行处理

            //拿的是空碟子，可以装起面前厨具里煮熟的食物，厨具变空
            //拿的是非空碟子。可以倒进垃圾桶变空（拿回空碟子），或者上菜（销毁）

            if(inHandState.Equals(InHandState.EmptyDish)&&placeState.Equals(PlaceState.FullCooker))
            {
                //如果煮熟了
               if(dish.transform.GetComponent<CookerController>().getCookerFoodState()==1)
               {
                    for (int i = dish.transform.GetChild(0).childCount - 1; i >= 0; i--)
                    {
                        dish.transform.GetChild(0).GetChild(i).position = foodInHand.transform.position;
                        dish.transform.GetChild(0).GetChild(i).SetParent(foodInHand.transform.GetChild(0));

                    }
               }
                
               
            }


            //拿的是空混合物容器。
            //拿的是混合物容器。可以倒进垃圾桶变空（拿回空容器），或者倒进空厨具（拿回空容器，厨具变非空）
            else if (inHandState.Equals(InHandState.MixContainer) && placeState.Equals(PlaceState.EmptyCooker))
            {
                //Debug.Log("混合物倒进空厨具");
                for (int i = foodInHand.transform.GetChild(0).childCount-1; i >=0; i--)
                {
                    foodInHand.transform.GetChild(0).GetChild(i).position = dish.transform.position;
                    foodInHand.transform.GetChild(0).GetChild(i).SetParent(dish.transform.GetChild(0).transform);

                }
            }

            //拿的是单食物。
            //拿的是可混合食物材料。可以放到混合物容器。
            //拿的是需要煮的食物
            else if (inHandState.Equals(InHandState.StackableFood) && (placeState.Equals(PlaceState.EmptyContainer)|| placeState.Equals(PlaceState.MixContainer)))
            {
                foodInHand.transform.SetParent(dish.transform.GetChild(0));
                foodInHand.transform.position = dish.transform.GetChild(0).position;

                dish.transform.GetChild(0).GetComponent<FoodMixtureController>().checkMix = true;
            }

            //拿的是空厨具。
            //拿的是非空厨具，可以把厨具里的食物放到面前空碟子（厨具 变空）。倒进垃圾桶。
            else if (inHandState.Equals(InHandState.FullCooker) && placeState.Equals(PlaceState.EmptyDish))
            {
               /* for (int i = foodInHand.transform.GetChild(0).childCount - 1; i >= 0; i--)
                {
                    foodInHand.transform.GetChild(0).GetChild(i).position = dish.transform.position;
                    foodInHand.transform.GetChild(0).GetChild(i).SetParent(dish.transform.GetChild(0));
                }*/

                if (foodInHand.transform.GetComponent<CookerController>().getCookerFoodState() == 1)
                {
                    for (int i = foodInHand.transform.GetChild(0).childCount - 1; i >= 0; i--)
                    {
                        foodInHand.transform.GetChild(0).GetChild(i).position = dish.transform.position;
                        foodInHand.transform.GetChild(0).GetChild(i).SetParent(dish.transform.GetChild(0));
                    }
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
    /// <returns>null就是没有检测到place</returns>
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