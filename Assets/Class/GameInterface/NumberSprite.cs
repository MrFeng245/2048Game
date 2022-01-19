using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/// <summary>
/// 附加到每个方格中，用于定义方格行为
/// </summary>
public class NumberSprite : MonoBehaviour
{
    private Image image;

    private void Awake()
    {      
        image = GetComponent<Image>();
    }
    public void SetImage(int number)
    {
        image.sprite = ResourceManager.LoadSprite(number);
    }

    /// <summary>
    /// 生成效果
    /// </summary>
    public void CreateEffect()
    {
        //小 --> 大
        iTween.ScaleFrom(gameObject, Vector3.zero, 0.3f);
    }

}
