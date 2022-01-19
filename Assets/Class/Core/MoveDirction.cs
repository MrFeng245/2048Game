using System;
using System.Collections.Generic;
using System.Text;

namespace Console2048
{
    /// <summary>
    /// 定义枚举类型：移动方向
    /// </summary>
    //简单枚举
    //默认为int，MoveDirtion:long可以改为long
    enum MoveDirction
    {
        Up = 0,
        Down = 1,
        Left = 3,
        Right = 4
    }
}
