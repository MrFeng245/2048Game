using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/// <summary>
/// 结束面板——结算功能
/// </summary>
public class SettlementPanel : MonoBehaviour
{
    private Text scoreMessage;
    private Text currentTime;
    private Text currentScore;
    private Text maxScoreText;
    private void Start()
    {
        scoreMessage = GameObject.Find("CurrentScoreShow").GetComponent<Text>();
        currentTime = transform.GetChild(0).gameObject.GetComponent<Text>();
        currentScore = transform.GetChild(1).gameObject.GetComponent<Text>();
        maxScoreText = transform.GetChild(2).gameObject.GetComponent<Text>();
    }
    //获取游戏结束时间，结束分数
    public void Settlement(int maxScore)
    {
        currentScore.text = "本次得分：" + scoreMessage.text;
        currentTime.text = DateTime.Now.ToString();
        maxScoreText.text = "最高分：" + maxScore.ToString();
    }
}
