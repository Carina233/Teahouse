using System.Net.Mime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : Singleton<UIManager>
{
    [Tooltip("当前已完成箱子个数")]
    public Text currentBoxCount;
    [Tooltip("总共箱子个数")]
    public Text totalBoxCount;
    [Tooltip("玩家移动步数")]
    public Text playerMoveStep;
    [Tooltip("玩家剩余移动步数")]
    public Text playerMoveStepLeft;
    public Text playerMoveStepLeftWarning;
    [Tooltip("通关文本")]
    public Text winText;
    public Text gameOverText;
    public Text tipsText;
    [Tooltip("当前时间文本")]
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
