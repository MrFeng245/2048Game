using UnityEngine;
using UnityEngine.UI;
using Console2048;
using UnityEngine.EventSystems;

/// <summary>
/// 游戏控制器
/// </summary>
public class GameController : MonoBehaviour,IPointerDownHandler,IDragHandler
{
    private GameCore core;
    private GameObject overPanel;
    private NumberSprite[,] spriteActionArray;
    private OverTransferPanel overTransfer;
    private Text currentScore;
    private Text maxScore;
    private RankItem item;
    private SettlementPanel settlement;

    private void Start()
    {
        core = new GameCore();
        spriteActionArray = new NumberSprite[4, 4];
        item = GameObject.Find("Rank").gameObject.AddComponent<RankItem>();
        overPanel = GameObject.Find("PanelGameOver");
        overTransfer = gameObject.AddComponent<OverTransferPanel>();
        currentScore = GameObject.Find("CurrentScoreShow").GetComponent<Text>();
        maxScore = GameObject.Find("CurrentScoreText/MaxScore").GetComponent<Text>();
        settlement = GameObject.Find("PanelGameOver").AddComponent<SettlementPanel>();
        Init();
        GenerateNewNumber();
        GenerateNewNumber();
    }
    /// <summary>
    /// 初始化
    /// </summary>
    private void Init()
    {
        for (int r = 0; r < 4; r++)
        {
            for (int c = 0; c < 4; c++)
            {
                CreateSprite(r,c);
            }
        }
    }
    private void CreateSprite(int r,int c)
    {
        GameObject go = new GameObject(r.ToString() + c.ToString());
        //设置图片
        go.AddComponent<Image>();
        NumberSprite action = go.AddComponent<NumberSprite>();
        spriteActionArray[r, c] = action;
        action.SetImage(0);
        go.transform.SetParent(transform, false);

    }
    private void GenerateNewNumber()
    {
        Location? loc;
        int? number;
        core.GenerateNumber(out loc,out number);
        //根据位置，获取精灵行为脚本对象引用
        spriteActionArray[loc.Value.RIndex, loc.Value.CIndex].SetImage(number.Value);
        spriteActionArray[loc.Value.RIndex, loc.Value.CIndex].CreateEffect();
    }

    private void Update()
    {
        if(core.IsChange)
        {
            //更新分数
            UpadteScore();
            //更新界面
            UpdateMap();
            //产生新数字
            GenerateNewNumber();

            //移动效果
            //合并效果

            //游戏结束——调用面板、加载排行榜
            if (core.IsOver())
            {
                overTransfer.OverMove(overPanel);
                item.EnterMessage();
                settlement.Settlement(item.rankMsg[0].score);
                currentScore.text = "0";
            }
            maxScore.text = "最高分：" + item.rankMsg[0].score;
            //只做一次
            core.IsChange = false;
        }
    }

    private void UpadteScore()
    {
        int score = int.Parse(currentScore.text) + core.score;
        currentScore.text = score.ToString();
        core.score = 0;
    }

    private void UpdateMap()
    {
        for (int r = 0; r < 4; r++)
        {
            for (int c = 0; c < 4; c++)
            {
                spriteActionArray[r, c].SetImage(core.Map[r, c]);
            }
        }

    }

    private Vector2 startPoint;
    public void OnPointerDown(PointerEventData eventData)
    {
        startPoint = eventData.position;
        isDown = true;
    }

    private bool isDown = false;
    //每帧执行
    public void OnDrag(PointerEventData eventData)
    {
        if (!isDown) return;
        Vector2 offset = eventData.position - startPoint;
        float x = Mathf.Abs(offset.x);
        float y = Mathf.Abs(offset.y);

        MoveDirction? dir = null;
        //水平
        if(x > y && x>= 50)
        {
            dir = offset.x > 0 ? MoveDirction.Right : MoveDirction.Left;
        }
        //竖直
        if(x < y && y >= 50)
        {
            dir = offset.y > 0 ? MoveDirction.Up : MoveDirction.Down;
        }

        if(dir != null)
        {
            core.Move(dir.Value);
            isDown = false;
        }
    }

    /// <summary>
    /// 重新开始游戏
    /// </summary>
    public void RestartGame()
    {
        for (int r = 0; r < 4; r++)
        {
            for (int c = 0; c < 4; c++)
            {
                core.Map[r, c] = 0;
                spriteActionArray[r, c].SetImage(0);
            }
        }
        GenerateNewNumber();
        GenerateNewNumber();
    }
    public void GameExit()
    { 
        Application.Quit(); 
    }
    
    /*
     一、创建4*4方格
     1.创建GameController脚本，用于控制游戏逻辑（生成方格）
     2.创建NumberSprite脚本，用于定义精灵行为（设置图片，产生动画）
     3.创建ResourceManager类，用于读取资源（根据名称加载精灵）

    二、生成新数字
    1.导入算法类：GameCore、Location、MoveDirection
    2.修改GameCore类 返回位置与数字
            GenerationNumber() ==> GenerationNumber(out Location? loc, out int? number)
    3.在GameController脚本中添加生成新数字的方法
        修改创建精灵的方法CreateSprite(创建精灵时，存储精灵行为引用)

    三、更新页面
    1.创建GenerateNewNumber脚本，用于生成新数字
        修改GameCore中生成数字的方法，获取生成的数字及数字位置索引
    2.创建二维脚本数组，存储每个方块的行为，
    3.创建UpdateMap脚本，用于同步GameCore中的map
    
    四、创建动画
    1.NumberSprite脚本解决方格生成、         移动、合并动画——    待做
    2.MovePanel脚本解决按钮的界面切换
    3.创建ExitGame脚本，实现按钮控制退出游戏
     */
}
