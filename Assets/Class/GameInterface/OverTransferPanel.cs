using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 游戏结束调用结束界面
/// </summary>
public class OverTransferPanel : MonoBehaviour
{
    private Vector2 targetPos;
    private void Start()
    {
        targetPos = new Vector2(Screen.width / 2, Screen.height / 2);
    }
    public void OverMove(GameObject overPanel)
    {
        iTween.MoveTo(overPanel, targetPos, 1f);
    }
}
