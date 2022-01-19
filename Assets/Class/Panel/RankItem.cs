using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Console2048;
/// <summary>
/// 排名——提供获取数据、排名、加载数据的方法
/// </summary>
public class RankItem : MonoBehaviour
{
    //在游戏结束时调用改脚本
    private int[] scoreArray;
    private Text scoreText;
    public RankMessage[] rankMsg;
    private Text[] rankItem;
    private GameObject[] itemObject;
    private int score;
    /// <summary>
    /// 启用物体计数菌
    /// </summary>
    private int count = 0;
    /// <summary>
    /// 录入成绩计数菌
    /// </summary>
    private int index = 0;
    private void Start()
    {
        scoreArray = new int[10];
        rankMsg = new RankMessage[10];
        scoreText = GameObject.Find("CurrentScoreShow").GetComponent<Text>();
        rankItem = GetComponentsInChildren<Text>();
        itemObject = GameObject.FindGameObjectsWithTag("Rank");
        for (int i = 0; i < itemObject.Length; i++)
        {
            itemObject[i].SetActive(false);
        }
    }
    //封装三个方法
    public void EnterMessage()
    {
        //分数存入变量，判断
        score = int.Parse(scoreText.text);
        //判断数据是否满足存入条件——得分在前十
        if (scoreArray[9] > score) return;
        GetScoreAndTime();
        SetRank();
        EnterRanking();
    }

    /// <summary>
    /// 获取时间与得分
    /// </summary>
    private void GetScoreAndTime()
    {
        //录入排名信息
        rankMsg[index].score = score;
        rankMsg[index].time = DateTime.Now.ToString();
    }
    /// <summary>
    /// 排序
    /// </summary>
    /// <param name="score">得分</param>
    private void SetRank()
    {
        RankMessage temp;
        for (int i = 0; i < rankMsg.Length; i++)
        {
            for (int j = 0; j < rankMsg.Length - 1; j++)
            {
                if(rankMsg[j].score < rankMsg[j+1].score)
                {
                    temp = rankMsg[j];
                    rankMsg[j] = rankMsg[j + 1];
                    rankMsg[j + 1] = temp;
                }
            }
        }
    }
    /// <summary>
    /// 录入排名
    /// </summary>
    private void EnterRanking()
    {
        //分别解禁时个物体，并加载数据
        if(count < 10)
        {
            itemObject[index].SetActive(true);
            rankItem[3 * count].text = (count+1).ToString();
            rankItem[3 * count + 1].text = rankMsg[count].score.ToString();
            rankItem[3 * count + 2].text = rankMsg[count].time;
        } 
        //全部解禁后，重新加载录入信息
        else
        {
            for (int i = 0; i < 10; i++)
            {
                rankItem[3 * i].text = (i +1).ToString();
                rankItem[3 * i + 1].text = rankMsg[i].score.ToString();
                rankItem[3 * i + 2].text = rankMsg[i].time;
            }
        }

        //用结构体的索引表示排名——index的变化不再影响排名
        rankMsg[index].ranking = index;
        index = index < 9 ? index++ : 9;
        count = count < 9 ? count++ : 10;
    }

}
