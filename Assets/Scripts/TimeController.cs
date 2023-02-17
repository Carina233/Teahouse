using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TimeController : MonoBehaviour
{
        private TMP_Text textTime;
        public int second = 120;
        public float totalTime;
        int M;
        int S;
        private void Start()
        {
            textTime = this.GetComponent<TMP_Text>();
        }
        private void Update()
        {
            Timer();
        }

        private void Timer()
        {
            totalTime += Time.deltaTime;
            //deltaTime��һ֡����ʱ�䣬��������N֡������ʱ��Ϊ1��ʱ���룬��ÿ��ִ��һ��
            if (totalTime >= 1)
            {
                second--;
                if (second >= 0)
                {
                    totalTime = totalTime - 1;
                    M = second / 60;
                    S = second % 60;
                    textTime.text = string.Format("{0:d2}:{1:d2}", M, S);
                    if (M == 0 && S == 10)//ʣʮ����
                        textTime.color = Color.red;
                }
                else
                {
                    GameManager.Instance.PassCheck();
                }

            }


    }
    
    
}
