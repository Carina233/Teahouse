using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PlayerController : MonoBehaviour
{
    private InHandState inHandState;
    private PlaceState placeState;
    public float moveSpeed;//��ɫ�ƶ��ٶ�
    public float moveStep;//��ɫ�ƶ�����
    public Rigidbody2D rb;//��ɫRigidbody
    Vector2 moveDir; //�ƶ����򣨼����ƶ���
    

    Animator anim;//��ɫ����
 
    //ToolTip������Inspector����ʾ������ע��
    [Tooltip("����ܷ��ƶ��������ײ�����")] float layerCheckDistance_collider; // ����Ƿ����ƶ��������ײ�����
    [Tooltip("���Ӱ���ƶ�����ײ��")] public LayerMask checkLayer_collider; // ���Ӱ���ƶ�����ײ��

    [Tooltip("���ĳ�������Ƿ���ʳ��")] float layerCheckDistance_food; // ���ĳ�������Ƿ���ʳ��
    [Tooltip("����ʳ������")] public LayerMask checkLayer_food; // ����ʳ������

    [Tooltip("���ĳ�������Ƿ���λ�÷Ŷ���")] float layerCheckDistance_place; // ���ĳ�������Ƿ���λ�÷Ŷ���
    [Tooltip("����λ������")] public LayerMask checkLayer_place; // ����λ������

    [Tooltip("���ĳ�������Ƿ��������Ŷ���")] float layerCheckDistance_dish; // ���ĳ�������Ƿ�������
    [Tooltip("������������")] public LayerMask checkLayer_dish; // ������������

    public GameObject real; //��ʵ����
    public LayerMask tableLayer;
    public Tilemap realTilemap; // ���õ�Tilemap
    List<GameObject> realGameObjectList; // real�����������б�
   
    
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
    /// �ƶ�
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
    /// �ƶ����,���true���ϰ��ɼ����ƶ����ϰ���ǽ��װ�Σ����ӣ��豸���ϲ�̨
    /// </summary>
    /// <param name="dir"></param>
    /// <returns></returns>
    private GameObject MoveCheck(Vector2 dir)
    {
        //���˹�����Tooltip
        //[Tooltip("����ܷ��ƶ��������ײ�����")] float layerCheckDistance; // ����Ƿ����ƶ��������ײ�����
        //[Tooltip("���Ӱ���ƶ�����ײ��")] public LayerMask checkLayer; // ���Ӱ���ƶ�����ײ��
        //layerCheckDistance = 0.5f; ��˼���Ǽ���ɫǰ��0.5f����û����ײ��
        //ΪTileMap Wall�½�һ��Layer Wall��Ȼ��checkLayer��ѡ��Wall���������Ŀ����ǽ��
        //checkLayer_colliderֻѡ��Layermask���ڵ���ײ��������������ײ������
        
        RaycastHit2D hit = Physics2D.Raycast(transform.position, dir, layerCheckDistance_collider, checkLayer_collider);//���߼��
        //Debug.Log(dir+"hit"+ hit.collider);
        if (!hit)
        {
            //Debug.Log(dir + "ɶҲû��");
            //Debug.Log("����transform.position:"+ transform.position+"����dir:"+dir+ "��������ײ��ΪcheckLayer:"+ checkLayer_collider + "����������");
            return null;
        }
        else
        {
            Debug.Log("hit.collider.gameObject" + hit.collider.gameObject);
            return hit.collider.gameObject;
        }

    }


    /// <summary>
    /// ������Ч
    /// </summary>
    public void playStepSound()
    {
        //((AudioSource)this.gameObject.GetComponent(typeof(AudioSource))).Play();
    }

    /// <summary>
    /// �Ӳֿ�ȡ��
    /// </summary>
    public void controlWarehouse(GameObject placeType)
    {
        
         placeType.GetComponent<DeviceManager>().requirement = 1;
         placeType.GetComponent<DeviceManager>().requirementOwner = gameObject;
            
    }
    /// <summary>
    /// �鿴�Ƿ��е���/��
    /// </summary>
    public GameObject checkDish(Vector2 dir)
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, dir, layerCheckDistance_dish, checkLayer_dish);//���߼��
        //Debug.Log(dir + "placehit" + hit.collider);
        if (!hit)
        {
            Debug.Log(dir + "û������");
            //Debug.Log("����transform.position:"+ transform.position+"����dir:"+dir+ "��������ײ��ΪcheckLayer:"+ checkLayer_collider + "����������");
            return null;
        }
        Debug.Log("������:" + hit.collider.gameObject);
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
    /// ����/����ʳ��
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

            if (foodInHand)//�����ж���
            {
               
                //�����ǵ���
                if (foodInHand.transform.name== "Dish")
                {
                    //�ǿյ�����
                    if(foodInHand.transform.GetChild(0).childCount==0)//��
                    {
                        inHandState = InHandState.EmptyDish;
                    }
                    else//����
                    {
                        //foodInHand.layer = LayerMask.NameToLayer("Dish");
                        inHandState = InHandState.FullDish;
                    }
                }
                else if (foodInHand.transform.name == "MixingContainer")
                {
                    //�ǿ���
                    if (foodInHand.transform.GetChild(0).childCount == 0)//��
                    {
                        inHandState = InHandState.EmptyContainer;
                    }
                    else//����
                    {
                        Debug.Log("�����ŷǿջ��������");
                        inHandState = InHandState.MixContainer;
                    }
                }
                else if (foodInHand.transform.name == "Cooker")//�ǳ���
                {
                    //�ǿճ�����
                    if (foodInHand.transform.GetChild(0).childCount == 0)//��
                    {
                        inHandState = InHandState.EmptyCooker;
                       
                    }
                    else//����
                    {
                        inHandState = InHandState.FullCooker;
                    }
                }
                else if (foodInHand.layer == LayerMask.NameToLayer("Food"))//��ʳ��
                {
                    //�ɻ����
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
            else//����û����
            {
                
                //���ж������������ߣ��գ��ǿն�������
                if (dish)
                {
                    //��ǰ�ж�����������
                    dish.transform.SetParent(this.transform);
                    Debug.Log("gameObject.layer" + gameObject.layer);
                    dish.layer = gameObject.layer;
                    return;
                }
                //���жϳ��ߣ��ճ��ߣ��ǿճ��߶�������

                //�ж�ʳ��
                if (food)
                {
                    //��ǰ�ж�����������
                    food.transform.SetParent(this.transform);
                    Debug.Log("gameObject.layer" + gameObject.layer);
                    
                    return;
                }

                //�ֿ���Ķ���������
                if(placeType&&placeType.transform.name=="Warehouse")
                {
                    controlWarehouse(placeType);
                    return;
                }
            }

            /////////////////////////////
            //ǰ����ʲô�ط�

            //ǰ��������
            if (dish != null)
            {
                //�ǵ���
                if (dish.layer == LayerMask.NameToLayer("Dish"))
                {
                    //�ǿյ�����
                    if (dish.transform.GetChild(0).childCount == 0)//��
                    {
                        placeState = PlaceState.EmptyDish;
                    }
                    else//����
                    {
                        placeState = PlaceState.FullDish;
                        //foodInHand.layer = LayerMask.NameToLayer("Dish");
                    }
                }

                /*Debug.Log("�Ѷ����ŵ�������ѽ");
                foodInHand.transform.SetParent(dish.transform.GetChild(0));
                foodInHand.transform.position = dish.transform.GetChild(0).position;

                dish.transform.GetChild(0).GetComponent<FoodMixtureController>().checkMix = true;

                return;*/

                //ʲô�������ԷŽ��յ��ӣ����Էŵ����ӵ�ʳ��յ��ӣ��ǿյ��������ʳ��ǿճ��������ʳ�
                else if (dish.layer == LayerMask.NameToLayer("Cooker"))
                {
                    //�ǿճ�����
                    if (dish.transform.GetChild(0).childCount == 0)//��
                    {

                        Debug.Log("ǰ���пճ���");
                        placeState = PlaceState.EmptyCooker;
                        //check
                    }
                    else//����
                    {
                        placeState = PlaceState.FullCooker;
                    }
                    //ʲô�������ԷŽ��ճ��ߣ��ǿյ��������ʳ��ǿճ��������ʳ�

                }
                else if (dish.layer == LayerMask.NameToLayer("MixingContainer"))
                {
                    //�ǿջ��������
                    if (dish.transform.GetChild(0).childCount == 0)//��
                    {
                        placeState = PlaceState.EmptyContainer;
                    }
                    else//����
                    {
                        placeState = PlaceState.MixContainer;
                    }
                   

                }

            }
           

            //ǰ��ɶ�ض�Ҳû�У���
            else if (!placeType)
            {
                return;
            }
            //ɶ�ض�
            else
            {
                switch (LayerMask.LayerToName(placeType.layer))//���ݲ㼶��ȡ����
                {

                    case "Table"://��ͨ��������
                        foodInHand.transform.SetParent(placeType.transform);
                        foodInHand.transform.position = placeType.transform.position;
                        if(foodInHand.layer==LayerMask.NameToLayer("Default"))
                        {
                            foodInHand.layer = LayerMask.NameToLayer(foodInHand.transform.name);
                        }
                        break;
                    case "Device"://�޳��߳�����,ֻ�г��߿��Է�����
                        if (foodInHand.transform.name=="Cooker")
                        {
                            foodInHand.transform.SetParent(placeType.transform);
                            foodInHand.transform.position = placeType.transform.position;
                            foodInHand.layer = LayerMask.NameToLayer(foodInHand.transform.name);

                            //check
                        }
                        break;
                    case "OutputCheck"://�ŵ����˿�
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

                    case "WasteBin"://�ŵ�����Ͱ
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

            ////////���ݵ�ǰ�õĶ���������/ʳ��/����/�������������ǰ�Ķ��������ӣ����ߣ�������������д���

            //�õ��ǿյ��ӣ�����װ����ǰ�����������ʳ����߱��
            //�õ��Ƿǿյ��ӡ����Ե�������Ͱ��գ��ûؿյ��ӣ��������ϲˣ����٣�

            if(inHandState.Equals(InHandState.EmptyDish)&&placeState.Equals(PlaceState.FullCooker))
            {
                //���������
               if(dish.transform.GetComponent<CookerController>().getCookerFoodState()==1)
               {
                    for (int i = dish.transform.GetChild(0).childCount - 1; i >= 0; i--)
                    {
                        dish.transform.GetChild(0).GetChild(i).position = foodInHand.transform.position;
                        dish.transform.GetChild(0).GetChild(i).SetParent(foodInHand.transform.GetChild(0));

                    }
               }
                
               
            }


            //�õ��ǿջ����������
            //�õ��ǻ�������������Ե�������Ͱ��գ��ûؿ������������ߵ����ճ��ߣ��ûؿ����������߱�ǿգ�
            else if (inHandState.Equals(InHandState.MixContainer) && placeState.Equals(PlaceState.EmptyCooker))
            {
                //Debug.Log("����ﵹ���ճ���");
                for (int i = foodInHand.transform.GetChild(0).childCount-1; i >=0; i--)
                {
                    foodInHand.transform.GetChild(0).GetChild(i).position = dish.transform.position;
                    foodInHand.transform.GetChild(0).GetChild(i).SetParent(dish.transform.GetChild(0).transform);

                }
            }

            //�õ��ǵ�ʳ�
            //�õ��ǿɻ��ʳ����ϡ����Էŵ������������
            //�õ�����Ҫ���ʳ��
            else if (inHandState.Equals(InHandState.StackableFood) && (placeState.Equals(PlaceState.EmptyContainer)|| placeState.Equals(PlaceState.MixContainer)))
            {
                foodInHand.transform.SetParent(dish.transform.GetChild(0));
                foodInHand.transform.position = dish.transform.GetChild(0).position;

                dish.transform.GetChild(0).GetComponent<FoodMixtureController>().checkMix = true;
            }

            //�õ��ǿճ��ߡ�
            //�õ��Ƿǿճ��ߣ����԰ѳ������ʳ��ŵ���ǰ�յ��ӣ����� ��գ�����������Ͱ��
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
    /// �������������޶���
    /// </summary>
     GameObject checkHand()
    {
        int length = GetComponentsInChildren<Transform>(true).Length;
        if (length <= 1)//���ڵ��Լ���һ��transform
        {
            Debug.Log("û��������");
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
    /// ��Ʒ����λ�ü��
    /// </summary>
    /// <param name="dir"></param>
    /// <returns>null����û�м�⵽place</returns>
    GameObject placeCheck(Vector2 dir)
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, dir, layerCheckDistance_place, checkLayer_place);//���߼��
        //Debug.Log(dir + "placehit" + hit.collider);
        if (!hit)
        {
            Debug.Log(dir + "û��place");
            //Debug.Log("����transform.position:"+ transform.position+"����dir:"+dir+ "��������ײ��ΪcheckLayer:"+ checkLayer_collider + "����������");
            return null;
        }
        Debug.Log("place:" + hit.collider.gameObject);
        return hit.collider.gameObject;
    }

    /// <summary>
    /// ��Ʒ��⣬true������
    /// </summary>
    /// <param name="dir"></param>
    /// <returns></returns>
    GameObject foodCheck(Vector2 dir)
    {
        //���˹�����Tooltip
        //[Tooltip("����ܷ��ƶ��������ײ�����")] float layerCheckDistance; // ����Ƿ����ƶ��������ײ�����
        //[Tooltip("���Ӱ���ƶ�����ײ��")] public LayerMask checkLayer; // ���Ӱ���ƶ�����ײ��
        //layerCheckDistance = 0.5f; ��˼���Ǽ���ɫǰ��0.5f����û����ײ��
        //ΪTileMap Wall�½�һ��Layer Wall��Ȼ��checkLayer��ѡ��Wall���������Ŀ����ǽ��
        //Debug.Log(dir);
        RaycastHit2D hit = Physics2D.Raycast(transform.position, dir, layerCheckDistance_food, checkLayer_food);//���߼��
        if (hit)
        {
            Debug.Log("��ɫλ��transform.position:" + transform.position + "����dir:" + dir + "������ʳ��ΪcheckLayer:" + checkLayer_food + "�ж������Լ�:" + hit.collider.name);
            return hit.collider.gameObject;
        }
        return null;
    }
}