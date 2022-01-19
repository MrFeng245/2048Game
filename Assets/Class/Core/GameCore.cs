using System;
using System.Collections.Generic;

namespace Console2048
{
    /// <summary>
    /// 游戏核心类，负责处理游戏核心算法，与界面无关
    /// </summary>
    class GameCore
    {
        //逻辑代码      界面代码        数据代码
        //将参数换成成员——避免每次移动产生垃圾
        private int[,] map;

        private int[] mergeArray;

        private int[] removeZeroArray;

        public int score = 0;

        public int[,] Map
        { 
            get
            { return map; }
        }


        public GameCore()
        {
            map = new int[4, 4];
            mergeArray = new int[4];
            removeZeroArray = new int[4];
            emptyLocationList = new List<Location>(16);
            random = new Random();
            originalMap = new int[4,4];
        }

        #region 数据合并

        private void RemoveZero()
        {
            Array.Clear(removeZeroArray, 0, 4);
            int index = 0;
            for (int i = 0; i < mergeArray.Length; i++)
            {
                if (mergeArray[i] != 0)
                    removeZeroArray[index++] = mergeArray[i];
            }
            removeZeroArray.CopyTo(mergeArray, 0);
        }

        private void Merge()
        {
            RemoveZero();

            for (int i = 0; i < mergeArray.Length - 1; i++)
            {
                if (mergeArray[i] != 0 && mergeArray[i] == mergeArray[i + 1])
                {
                    mergeArray[i] += mergeArray[i + 1];
                    mergeArray[i + 1] = 0;

                    //积分
                    score ++;
                    //记录合并位置——位置集合
                }
            }

            RemoveZero();
        }
        #endregion

        #region 移动

        private void MoveUp()
        {
            for (int c = 0; c < map.GetLength(1); c++)
            {
                for (int r = 0; r < map.GetLength(0); r++)
                {
                    mergeArray[r] = map[r, c];
                }

                Merge();

                for (int r = 0; r < map.GetLength(0); r++)
                {
                    map[r, c] = mergeArray[r];
                }
            }
        }

        private void MoveDown()
        {
            for (int c = 0; c < map.GetLength(1); c++)
            {
                for (int r = map.GetLength(0) - 1; r >= 0; r--)
                {
                    mergeArray[3 - r] = map[r, c];
                }

                Merge();

                for (int r = map.GetLength(0) - 1; r >= 0; r--)
                {
                    map[r, c] = mergeArray[3 - r];
                }
            }
        }

        private void ShiftLeft()
        {
            for (int c = 0; c < map.GetLength(0); c++)
            {
                for (int r = 0; r < map.GetLength(1); r++)
                {
                    mergeArray[r] = map[c, r];
                }

                Merge();

                for (int r = 0; r < map.GetLength(1); r++)
                {
                    map[c, r] = mergeArray[r];
                }
            }
        }

        private void ShiftRight()
        {
            for (int c = 0; c < map.GetLength(0); c++)
            {
                for (int r = map.GetLength(1) - 1; r >= 0; r--)
                {
                    mergeArray[3 - r] = map[c, r];
                }

                Merge();

                for (int r = map.GetLength(1) - 1; r >= 0; r--)
                {
                    map[c, r] = mergeArray[3 - r];
                }
            }
        }

        private int[,] originalMap;
        /// <summary>
        /// 地图是否发生改变
        /// </summary>
        public bool IsChange { get; set; }
        public void Move(MoveDirction dirction)
        {
            Array.Copy(map, originalMap, map.Length);
            IsChange = false;

            switch (dirction)
            {
                case MoveDirction.Up:
                    MoveUp();
                    break;
                case MoveDirction.Down:
                    MoveDown();
                    break;
                case MoveDirction.Left:
                    ShiftLeft();
                    break;
                case MoveDirction.Right:
                    ShiftRight();
                    break;
            }
            for (int r = 0; r < map.GetLength(0); r++)
            {
                for (int c = 0; c < map.GetLength(1); c++)
                {
                    if (map[r, c] != originalMap[r, c])
                    {
                        IsChange = true;
                        return;
                    }

                }
            }
        }

        #endregion

        #region 生成新数字
        //需求：在空白位置，随机产生一个2（90%）或 4（10%）
        //分析：先计算所有空白位置
        //     再随机一个位置
        //     随机2、4
        private List<Location> emptyLocationList;
        private void CalculateEmpty()
        {
            //每次统计空位置前先清零
            emptyLocationList.Clear();

            for (int r = 0; r < map.GetLength(0); r++)
            {
                for (int c = 0; c < map.GetLength(1); c++)
                {
                    if(map[r,c] == 0)
                    {
                        //记录 r c
                        //将多个基本数据类型，封装为一个自定义类型
                        emptyLocationList.Add(new Location(r, c));
                    }
                }
            }
        }

        private Random random;
        public void GenerateNumber(out Location? loc,out int? number)
        {
            CalculateEmpty();
            if (emptyLocationList.Count > 0)
            {
                int randomIndex = random.Next(0, emptyLocationList.Count);
                loc = emptyLocationList[randomIndex];
                number = map[loc.Value.RIndex, loc.Value.CIndex] = random.Next(0, 10) == 6 ? 4 : 2;
            }
            else
            {
                //提供不可能的值告诉不可能存在——等同于值类型赋空值
                //loc = new Location(-1, -1);
                //number = -1;
                //int？是c#的可空值类型
                //int？不再是原来的值类型，如果要获取原来的值类型，使用a.Value
                loc = null;
                number = null;
            }
        }
        public bool IsOver()
        {
            //如果空位置集合不为空 或 相邻相同，返回false
            if (emptyLocationList == null) return false;
            for (int r = 0; r < map.GetLength(0); r++)
            {
                for (int c = 0; c < map.GetLength(1) -1; c++)
                {
                    if (map[r, c + 1] == map[r, c]) return false;                      
                }
            }
            for (int c = 0; c < map.GetLength(1); c++)
            {
                for (int r = 0; r < map.GetLength(0) - 1; r++)
                {
                    if (map[r + 1, c] == map[r, c]) return false;
                }
            }
            return true;
        }

        #endregion
    }
}
