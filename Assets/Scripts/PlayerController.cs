using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PlayerController : MonoBehaviour
{
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

    [Tooltip("���ĳ�������Ƿ��е��ӷŶ���")] float layerCheckDistance_dish; // ���ĳ�������Ƿ���ʳ��
    [Tooltip("���ĵ�������")] public LayerMask checkLayer_dish; // ���ĵ�������

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
        controlWarehouse();
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
    public void controlWarehouse()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {

            //������û�ж���
            GameObject food = foodCheck(moveDir);
            if (food||checkHand())
            {
                return;
            }
            //ǰ���ǲֿ����ǵĻ�����ֿ�����ȡ����Ʒ
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
    /// �鿴�Ƿ��е���/��
    /// </summary>
    public GameObject checkDish(Vector2 dir)
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, dir, layerCheckDistance_dish, checkLayer_dish);//���߼��
        //Debug.Log(dir + "placehit" + hit.collider);
        if (!hit)
        {
            Debug.Log(dir + "û��diezi");
            //Debug.Log("����transform.position:"+ transform.position+"����dir:"+dir+ "��������ײ��ΪcheckLayer:"+ checkLayer_collider + "����������");
            return null;
        }
        Debug.Log("�е���:" + hit.collider.gameObject);
        return hit.collider.gameObject;
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


            if (foodInHand)//�����ж���
            {
                if (foodInHand.transform.name == "Dish")
                {
                    foodInHand.layer = LayerMask.NameToLayer("Dish");
                }
              
                
                //ǰ�������ӣ���������ټ�һ���ҿ��Է���������ж�,���������岻�ܷ�����
                if(dish!=null)
                {
                    if(foodInHand.GetComponent<FoodManager>().stackable == false)
                    {
                        return;
                    }
                    
                    Debug.Log("�Ѷ����ŵ�������ѽ");
                    foodInHand.transform.SetParent(dish.transform);
                    foodInHand.transform.position = dish.transform.position;

                    dish.transform.GetComponent<DishController>().checkMix = true;
                    

                    //foodInHand.layer = LayerMask.NameToLayer("Food");
                    return;
                }
                //ǰ��û�����ӣ����������棬�������ϣ��ټ�һ����ʳ�ﱻ���Է������ϵ��ж�
                if (!placeType)
                {
                    return;
                }
                switch (LayerMask.LayerToName(placeType.layer))//���ݲ㼶��ȡ����
                {

                    case "Table"://��ͨ����
                        foodInHand.transform.SetParent(placeType.transform);
                        foodInHand.transform.position = placeType.transform.position;
                        //foodInHand.layer = LayerMask.NameToLayer("Food");
                        break;
                    case "Device"://�ŵ���⿹�����
                        foodInHand.transform.SetParent(placeType.transform);
                        foodInHand.transform.position = placeType.transform.position;
                        //foodInHand.layer = LayerMask.NameToLayer("Food");
                        break;
                    case "OutputCheck"://�ŵ����˿�
                        foodInHand.transform.SetParent(placeType.transform);
                        foodInHand.transform.position = placeType.transform.position;
                        //foodInHand.layer = LayerMask.NameToLayer("Food");
                        break;
                    
                }
                
              
            }
            else//����û����
            {
                if (dish)
                {
                    //��ǰ�ж�����������
                    dish.transform.SetParent(this.transform);
                    Debug.Log("gameObject.layer" + gameObject.layer);
                    dish.layer = gameObject.layer;
                    return;
                }
                if (food)
                {
                    //��ǰ�ж�����������
                    food.transform.SetParent(this.transform);
                    Debug.Log("gameObject.layer" + gameObject.layer);
                    //food.layer = gameObject.layer;
                    return;
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
    /// <returns></returns>
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