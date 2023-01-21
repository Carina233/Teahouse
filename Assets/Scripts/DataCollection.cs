using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/*����ʹ��class����struct�����������жϣ�
 * ��������������������Ϸ���й�����һֱ���䣬������class��
 * ��Ҫ���е���������ֵ�Ľ�����struct��
 * ���������������ڳ��е�ÿһ����Ʒ�����*/

[System.Serializable]
public class foodDetail//������ϸ
{
    public string foodId;
    public string foodName;
    public string foodLayer;
    public Sprite foodImage;
    public string foodIntroduction;
    public bool stackable;//ʳ���Ƿ�ɵ���
    public int stackNum;//���Ӹ���
    public LayerMask place;//���Է���ʲô�ط�
    public bool finshCooking;//�Ƿ�������
    public bool serveCorrectly;//�Ƿ���ȷ
    public List<string> canMakeWhatDishes;//��������ʲô��
    public List<string> canBeMakeByWhat;//��ʲô�ϳ�
}

[System.Serializable] 
public struct playerHold
{
    public foodDetail foodDetail;
    public int num;//��������
}