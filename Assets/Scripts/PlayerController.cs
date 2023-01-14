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
    [Tooltip("����ܷ��ƶ��������ײ�����")] float layerCheckDistance; // ����Ƿ����ƶ��������ײ�����
    [Tooltip("���Ӱ���ƶ�����ײ��")] public LayerMask checkLayer; // ���Ӱ���ƶ�����ײ��

    public GameObject real; //��ʵ����
    public LayerMask tableLayer;
    public Tilemap realTilemap; // ���õ�Tilemap
    List<GameObject> realGameObjectList; // real�����������б�
   
    
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
    /// �ƶ�
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
    /// �ƶ����,���true���ϰ��ɼ����ƶ�
    /// </summary>
    /// <param name="dir"></param>
    /// <returns></returns>
    bool MoveCheck(Vector2 dir)
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, dir, layerCheckDistance, checkLayer);
        if (!hit)
        {
            Debug.Log("����transform.position:"+ transform.position+"����dir:"+dir+ "��������ײ��ΪcheckLayer:"+ checkLayer+"����������");
            return true;
        }
        return false;
    }

    /// <summary>
    /// ��ĳ�����ƶ���λ����
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
}