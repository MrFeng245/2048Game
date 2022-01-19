using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 资源管理类，负责加载资源
/// </summary>
public class ResourceManager 
{
    //创建字典集合
    private static Dictionary<int,Sprite> spriteDic;
    static ResourceManager()
    {
        // 数字变为精灵，设置到Image中
        //读取单个精灵，使用Load，读取图集，使用LoadAll
        //spriteArray = Resources.LoadAll<Sprite>("2028Atlas");

        spriteDic = new Dictionary<int, Sprite>();
        var spriteArray = Resources.LoadAll<Sprite>("2048游戏块");
        foreach (var item in spriteArray)
        {
            int intSpriteName = int.Parse(item.name);
            spriteDic.Add(intSpriteName, item);
        }

    }
    //静态可以用类名去 .
    /// <summary>
    /// 读取数字精灵
    /// </summary>
    /// <param name="number">精灵表示的数字</param>
    /// <returns>精灵</returns>
    public static Sprite LoadSprite(int number)
    {

        //foreach (var item in spriteArray)
        //{
        //    if (item.name == number.ToString())
        //        return item;
        //}
        //return null;
        
        return spriteDic[number];
    }
}
