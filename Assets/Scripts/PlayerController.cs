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
    bool isMoving;//��ɫ�Ƿ����ƶ�

    Animator anim;//��ɫ����
 
    //ToolTip������Inspector����ʾ������ע��
    [Tooltip("����ܷ��ƶ��������ײ�����")] float layerCheckDistance_collider; // ����Ƿ����ƶ��������ײ�����
    [Tooltip("���Ӱ���ƶ�����ײ��")] public LayerMask checkLayer_collider; // ���Ӱ���ƶ�����ײ��

    [Tooltip("���ĳ�������Ƿ���ʳ��")] float layerCheckDistance_food; // ���ĳ�������Ƿ���ʳ��
    [Tooltip("����ʳ������")] public LayerMask checkLayer_food; // ����ʳ������

    [Tooltip("���ĳ�������Ƿ���λ�÷Ŷ���")] float layerCheckDistance_place; // ���ĳ�������Ƿ���ʳ��
    [Tooltip("����λ������")] public LayerMask checkLayer_place; // ����ʳ������

    public GameObject real; //��ʵ����
    public LayerMask tableLayer;
    public Tilemap realTilemap; // ���õ�Tilemap
    List<GameObject> realGameObjectList; // real�����������б�
   
    
    private void Start()
    {
        //rb = GetComponent<Rigidbody2D>();
        //anim = GetComponent<Animator>();
        isMoving = false;
        moveDir = Vector3.zero;
        layerCheckDistance_collider = 0.5f;
        layerCheckDistance_place = 1.0f;
        layerCheckDistance_food = 1.0f;
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
        Debug.Log("??");
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            isMoving = true;
            moveDir = Vector2.right;
            playStepSound();
            setWalkAnim(moveDir);
            Debug.Log("right");
        }

        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            isMoving = true;
            moveDir = Vector2.left;
            playStepSound();
            setWalkAnim(moveDir);
            Debug.Log("left");
        }

        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            isMoving = true;
            moveDir = Vector2.up;
            playStepSound();
            setWalkAnim(moveDir);
            Debug.Log("up");
        }

        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            Debug.Log("down");
            isMoving = true;
            moveDir = Vector2.down;
            playStepSound();
            setWalkAnim(moveDir);
        }


        MoveCheck(moveDir);
        if (isMoving)
        {
            Debug.Log("aba?");
            isMoving = false;
            StartCoroutine(MoveToDir(moveDir));
        }
        

       




    }

    
    void setWalkAnim(Vector2 dir)
    {
        //anim.SetFloat("Walk", 1);
        //anim.SetFloat("X", dir.x);
        //anim.SetFloat("Y", dir.y);
    }

    /// <summary>
    /// �ƶ����,���true���ϰ��ɼ����ƶ�
    /// </summary>
    /// <param name="dir"></param>
    /// <returns></returns>
    GameObject MoveCheck(Vector2 dir)
    {
        //���˹�����Tooltip
        //[Tooltip("����ܷ��ƶ��������ײ�����")] float layerCheckDistance; // ����Ƿ����ƶ��������ײ�����
        //[Tooltip("���Ӱ���ƶ�����ײ��")] public LayerMask checkLayer; // ���Ӱ���ƶ�����ײ��
        //layerCheckDistance = 0.5f; ��˼���Ǽ���ɫǰ��0.5f����û����ײ��
        //ΪTileMap Wall�½�һ��Layer Wall��Ȼ��checkLayer��ѡ��Wall���������Ŀ����ǽ��
        //checkLayer_colliderֻѡ��Layermask���ڵ���ײ��������������ײ������
        RaycastHit2D hit = Physics2D.Raycast(transform.position, dir, layerCheckDistance_collider, checkLayer_collider);//���߼��
        Debug.Log(dir+"hit"+ hit.collider);
        if (!hit)
        {
            Debug.Log(dir+"ɶҲû��");
            //Debug.Log("����transform.position:"+ transform.position+"����dir:"+dir+ "��������ײ��ΪcheckLayer:"+ checkLayer_collider + "����������");
            return null;
        }
        Debug.Log("hit.collider.gameObject" + hit.collider.gameObject);
        return hit.collider.gameObject;
    }
    /// <summary>
    /// ��ĳ�����ƶ���λ����
    /// </summary>
    /// <param name="dir"></param>
    IEnumerator MoveToDir(Vector2 dir)
    {
        // transform.Translate(dir);
        //isMoving = true;
        Debug.Log("isMoving:"+ isMoving);

        Vector2 targetPos = transform.position + (Vector3)dir * moveStep;

        while (!MoveCheck(dir)&&Vector3.Distance(transform.position, targetPos) > 0.005f)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPos, moveSpeed * Time.deltaTime);
            //TODO:������ײ���Ժ����λ�û���һ���ƫ�ƣ����0.01-0.03
           yield return null;
        }
        isMoving = false;

        
        //anim.SetFloat("Walk", 0);

    }

    ///
    /// ��������
    ///
    public void playStepSound()
    {
        //((AudioSource)this.gameObject.GetComponent(typeof(AudioSource))).Play();
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

            Debug.Log(moveDir+"placeType"+placeType);


            if (foodInHand)//�����ж���
            {
                Transform foodMap = GameObject.Find("FoodMap").transform;
                Transform table = GameObject.Find("Table").transform;
                Transform device = GameObject.Find("Device").transform;
                Transform outPutCheck = GameObject.Find("OutputCheck").transform;

                // int layer = LayerMask.NameToLayer("Table");//�������ƻ�ȡ�㼶
                switch (LayerMask.LayerToName(placeType.layer))//���ݲ㼶��ȡ����
                {
           
                    case "Table"://��ͨ����
                        foodInHand.transform.SetParent(table);
                        break;
                    case "Device"://�ŵ���⿹�����
                        foodInHand.transform.SetParent(device);
                        break;
                    case "OutputCheck"://�ŵ����˿�
                        foodInHand.transform.SetParent(outPutCheck);
                        break;
                    default:
                        foodInHand.transform.SetParent(foodMap);
                        break;
                }
              
            }
            else//����û����
            {
                GameObject food = foodCheck(moveDir);
                if (food)
                {
                    //��ǰ�ж�����������
                    food.transform.SetParent(this.transform);
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
        Debug.Log(dir + "placehit" + hit.collider);
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
        Debug.Log(dir);
        RaycastHit2D hit = Physics2D.Raycast(transform.position, dir, layerCheckDistance_food, checkLayer_food);//���߼��
        if (hit)
        {
            Debug.Log("��ɫλ��transform.position:" + transform.position + "����dir:" + dir + "������ʳ��ΪcheckLayer:" + checkLayer_food + "�ж������Լ�:" + hit.collider.name);
            return hit.collider.gameObject;
        }
        return null;
    }
}