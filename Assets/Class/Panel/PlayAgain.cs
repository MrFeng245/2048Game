using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
/// <summary>
/// 重新来过
/// </summary>
public class PlayAgain : MonoBehaviour, IPointerClickHandler
{
    private Vector2 startPos;
    private GameObject buttonPanel;
    private void Start()
    {
        buttonPanel = GameObject.FindWithTag("Finish");
        startPos = buttonPanel.transform.position;
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        iTween.MoveTo(buttonPanel, startPos, 1f);
    }
}
