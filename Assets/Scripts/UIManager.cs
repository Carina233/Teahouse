using System.Net.Mime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : Singleton<UIManager>
{
    [Tooltip("��ǰ��������Ӹ���")]
    public Text currentBoxCount;
    [Tooltip("�ܹ����Ӹ���")]
    public Text totalBoxCount;
    [Tooltip("����ƶ�����")]
    public Text playerMoveStep;
    [Tooltip("���ʣ���ƶ�����")]
    public Text playerMoveStepLeft;
    public Text playerMoveStepLeftWarning;
    [Tooltip("ͨ���ı�")]
    public Text winText;
    public Text gameOverText;
    public Text tipsText;
    [Tooltip("��ǰʱ���ı�")]
    public Text timeText;

    public GameObject passInfo;

    private void Update()
    {
        updateText();
    }

    public void updateText()
    {
        currentBoxCount.text = "Current Box : " + GameManager.Instance.currentBoxCount;
        totalBoxCount.text = "Total Box : " + GameManager.Instance.totalBoxCount;
        playerMoveStep.text = "Your steps : " + GameManager.Instance.playerMoveStep;
        playerMoveStepLeft.text = "Left steps : " + GameManager.Instance.playerMoveStepLeft;
    }

    public void ShowStepLeftWarning()
    {
        StartCoroutine(IEShowStepLeftWarning());
    }

    IEnumerator IEShowStepLeftWarning()
    {
        playerMoveStepLeftWarning.gameObject.SetActive(true);
        playerMoveStepLeftWarning.text = "Left Steps:" + GameManager.Instance.playerMoveStepLeft;
        yield return new WaitForSeconds(GameManager.Instance.playerMoveStepLeftWarningFadeTime);
        playerMoveStepLeftWarning.gameObject.SetActive(false);
    }

    public void reset()
    {
        winText.gameObject.SetActive(false);
        gameOverText.gameObject.SetActive(false);
    }
    public void ShowPassInfo()
    {
        passInfo.gameObject.SetActive(true);
        passInfo.transform.GetChild(2).GetChild(0).GetComponent<Text>().text = GameManager.Instance.time + "";
        passInfo.transform.GetChild(3).GetChild(0).GetComponent<Text>().text = GameManager.Instance.playerMoveStep + "";
    }
    public void UnShowPassInfo()
    {
        passInfo.gameObject.SetActive(false);
    }
}
