using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
/// <summary>
/// 返回主菜单
/// </summary>
public class ReturnMenu : MonoBehaviour, IPointerClickHandler
{
    private Vector2 buttonPos;
    private Vector2 gamePos;
    private GameObject buttonPanel;
    private GameObject gamePanel;
    private void Start()
    {
        buttonPanel = GameObject.FindWithTag("Finish");
        gamePanel = GameObject.Find("PanelGame");
        buttonPos = buttonPanel.transform.position;
        gamePos = gamePanel.transform.position;
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        iTween.MoveTo(buttonPanel, buttonPos, 1f);
        iTween.MoveTo(gamePanel, gamePos, 1f);
    }
}
