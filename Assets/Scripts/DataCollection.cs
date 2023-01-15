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
    public int foodId;
    public string foodName;
    public Sprite foodImage;
    public string foodIntroduction;
    public bool stackable;//ʳ���Ƿ�ɵ���
    public int stackNum;//���Ӹ���
    public bool finshCooking;//�Ƿ�������
    public bool serveCorrectly;//�Ƿ���ȷ
    public List<int> canMakeWhatDishes;//��������ʲô��
    public List<int> canBeMakeByWhat;//��ʲô�ϳ�
}

[System.Serializable] 
public struct playerHold
{
    public foodDetail foodDetail;
    public int num;//��������
}