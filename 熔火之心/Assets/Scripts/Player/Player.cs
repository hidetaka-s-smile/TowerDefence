using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public int Level = 1;       //等级
    public int Hp = 500;        //生命值
    public int Hp_max = 500;    //最大生命值
    public int EXP = 200;        //经验值
    public int EXP_max = 200;    //最大经验值
    public int Component = 0;   //零件数
    public GameObject burner;
    public AudioSource BuildAudio;
    public Texture2D cursor_normal;//正常
    public Texture2D cursor_clear;//拆除
    public Texture2D cursor_isclear;//选中拆除
    public GameObject MoveEffect;
    public Vector2 hotpots = Vector2.zero;
    public CursorMode mode = CursorMode.Auto;
    private Transform m_Transform;
    private Quaternion b = new Quaternion(0, 0, 0, 0);
    private Vector3 mousePositionOnScreen;
    private Vector3 targetPoint = Vector3.zero;     //鼠标点击的位置
    private Vector3 towerPoint = Vector3.zero;
    private Vector3 MousePoint = Vector3.zero;
    private CharacterController controller;
    private GameObject newTower;
    public bool havetower = false;
    public int cd;
    public float speed;
    public bool ifcd;
    public bool ifclear = false;
    public bool ismove=false;
    public bool isbuild = false;
    public bool isclear = false;
    public GameObject clearTower;
    public Animator anima;
    public bool CanMove = true;
    private int BuildTime = 0;
    private TowerInfo TInfo;
    Ray ray;
    RaycastHit hit;

    private GameObject towerGO;//即将建造的塔的物体
    [Header("状态栏UI")]
    public Text levelNumTxt;
    public Text componentText;
    public SmoothSlider hpBarSlider;
    public SmoothSlider expBarSlider;
   
    void Start()
    {
        anima = GetComponent<Animator>();
        speed = 30f;
        controller = GetComponent<CharacterController>();
        //初始化slider
        hpBarSlider.InitValue(Hp, Hp_max);
        expBarSlider.InitValue(0, 200);
    }

    void Update()
    {
        transform.localEulerAngles = new Vector3(0.0f, transform.localEulerAngles.y, 0.0f);
        if (Input.GetKeyDown(KeyCode.C))
        {
            burner.GetComponent<Burner>().Creat();
        }
        if (Input.GetKeyDown(KeyCode.F) && isbuild == false)
        {
            Cursor.SetCursor(cursor_clear, hotpots, mode);
            ifclear = true;
            if (havetower == true)
            {
                Destroy(newTower);
                havetower = false;
            }
        }
        if (havetower == true)      //预制跟随鼠标
        {
            newTower.transform.position = new Vector3(MousePoint.x, MousePoint.y + 2f, MousePoint.z);
        }

        if(Input.GetMouseButtonDown(0) && ifclear == true)        //确认拆除
        {
            StopMove();
            if (Physics.Raycast(ray, out hit))
            {
                //判断点击的是否塔 
                print(hit.collider.name);
                if (hit.collider.tag.Equals("Tower"))
                {
                    clearTower = hit.transform.gameObject;
                    towerPoint = MousePoint;
                    isclear = true;
                    ifclear = false;
                }
                else
                {
                    Cursor.SetCursor(cursor_normal, hotpots, mode);
                    ifclear = false;
                }
            }
        }

        if(isclear == true)
        {
            MoveClear();
        }

        if (Input.GetMouseButtonDown(0) && havetower == true && ifclear == false)       //确定建立
        {
            ifclear = false;
            if (ismove == true)
            {
                CanMove = false;
                ismove = false;
            }
            towerPoint = MousePoint;
            Startbuild();
        }
        if (isbuild == true)        //开始建立
        {
            MoveBuild();
        }

        if (Input.GetMouseButtonDown(1)&&CanMove == true)        //移动
        {
            ismove = true;
            ifclear = false;
            Cursor.SetCursor(cursor_normal, hotpots, mode);
            anima.SetBool("run", true);
            if(havetower==true)
            {
                Destroy(newTower);
                havetower = false;
            }
            GetTarget();
            GameObject ME = GameObject.Instantiate(MoveEffect, targetPoint, Quaternion.Euler(0.0f, 0.0f, 0.0f)) as GameObject;
        }
        if(ismove == true)
        {
            Move();
        }
            GetMouse();         //实时获取鼠标位置 
    }

    void Move()
    {
        transform.LookAt(new Vector3(targetPoint.x, targetPoint.y + 1f, targetPoint.z));
        float distance = Vector3.Distance(targetPoint, transform.position);
        if (distance > 3f)
        {
            transform.Translate(Vector3.forward * speed * Time.deltaTime);
            if(CanMove==false)
            {
                return;
            }
        }
        else
        {
            ismove = false;
            anima.SetBool("run", false);
        }
        isbuild = false;
        isclear = false;
    }

    void StopMove()
    {
        ismove = false;
        anima.SetBool("run", false);
    }

    void GetMouse()
    {
        ray = UnityEngine.Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit))
        {
            //判断点击的是否地形  
            if (hit.collider.tag.Equals("Tower") && ifclear == true)
            {
                Cursor.SetCursor(cursor_isclear, hotpots, mode);
            }
            else if (!hit.collider.tag.Equals("Tower") && ifclear == true)
            {
                Cursor.SetCursor(cursor_clear, hotpots, mode);
            }
            if (!hit.collider.tag.Equals("Plane"))
            {
                return;
            }
            
            //点击位置坐标   
            MousePoint = hit.point;
        }
    }

    /// <summary>
    /// 根据快捷键获取塔的信息，动态加载塔的预制体
    /// </summary>
    /// <param name="towerInfo"></param>
    public void Beforebuild(TowerInfo towerInfo)
    {
        TInfo = towerInfo;
        GetMouse();
        if (havetower == false)
        {
            //根据塔名从Resources文件夹动态加载塔的种类
            towerGO = Resources.Load<GameObject>(@"TowerGameObject\" + towerInfo.name);
            newTower = GameObject.Instantiate(towerGO, MousePoint, b) as GameObject;
            //炮塔建造中
            newTower.GetComponent<Tower>().IsBuilding = true;
            for (int i = 0; i < 3; i++)
            {
                Transform wallTransform = newTower.GetComponentsInChildren<Transform>()[i];
                wallTransform.gameObject.GetComponent<Renderer>().material.color = new Color(1f, 1f, 1f, 0.5f);
            }
            havetower = true;
        }
    }

    void Startbuild()
    {
        havetower = false;
        isbuild = true;
    }

    void MoveBuild()
    {
        transform.LookAt(new Vector3(towerPoint.x, towerPoint.y+2f, towerPoint.z));
        float distance = Vector3.Distance(towerPoint, transform.position);//计算目标位置到当前位置
        if (distance > 12f)
        {
            anima.SetBool("run", true);
            transform.Translate(Vector3.forward * speed * Time.deltaTime);
        }
        else
        {
            anima.SetBool("attack1", true);
            CanMove = false;
            anima.SetBool("run", false);
            if (!BuildLoader.instance.isLoading)
            {
                BuildAudio.Play();
                BuildLoader.instance.BuildLoad(newTower.GetComponent<Tower>().buildTime);
            }
            BuildTime++;
            if (BuildTime >= newTower.GetComponent<Tower>().buildTime * 50)
            {
                BuildEnd();
                BuildTime = 0;
            }
        }
        if (Input.GetMouseButtonDown(1) && CanMove == true)
        {
            Destroy(newTower);
            Move();
            return;
        }
    }

    void MoveClear()
    {
        transform.LookAt(new Vector3(towerPoint.x, towerPoint.y + 2f, towerPoint.z));
        float distance = Vector3.Distance(towerPoint, transform.position);//计算目标位置到当前位置
        if (distance > 12f)
        {
            anima.SetBool("run", true);
            transform.Translate(Vector3.forward * speed * Time.deltaTime);
        }
        else
        {
            anima.SetBool("attack1", true);
            CanMove = false;
            anima.SetBool("run", false);
            BuildTime++;
            //炮塔摧毁中
            if (!BuildLoader.instance.isLoading)
            {
                BuildAudio.Play();
                BuildLoader.instance.BuildLoad(clearTower.GetComponent<Tower>().buildTime);
            }
            clearTower.GetComponent<Tower>().IsBuilding = true;
            if (BuildTime >= clearTower.GetComponent<Tower>().buildTime * 49)
            {
                ClearEnd();
                BuildTime = 0;
            }
        }
    }
    void BuildEnd()
    {
        //炮塔可以攻击了
        newTower.GetComponent<Tower>().IsBuilding=false;
        isbuild = false;
        CanMove = true;
        BuildLoader.instance.HideLoader();
        anima.SetBool("attack1", false);
        gameObject.GetComponent<Player>().BuildAudio.Stop();
        for (int i = 0; i < 3; i++)
        {
            Transform wallTransform = newTower.GetComponentsInChildren<Transform>()[i];
            wallTransform.gameObject.GetComponent<Renderer>().material.color = new Color(1f, 1f, 1f, 1f);
        }
        Component -= newTower.GetComponent<Tower>().buildCost;
        componentText.text = Component.ToString();
        return;
    }
    void ClearEnd()
    {
        Cursor.SetCursor(cursor_normal, hotpots, mode);
        isclear = false;
        CanMove = true;
        BuildLoader.instance.HideLoader();
        anima.SetBool("attack1", false);
        gameObject.GetComponent<Player>().BuildAudio.Stop();
        clearTower.GetComponent<Tower>().BeDestroyed();
    }

    void GetTarget()
    {
        ray = UnityEngine.Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit))
        {
            //判断点击的是否地形  
            if (!hit.collider.tag.Equals("Plane"))
            {
                return;
            }
            //点击位置坐标   
            targetPoint = hit.point;
        }
    }
    /// <summary>
    /// 获得伤害
    /// </summary>
    public void GetDamage(int damage)
    {
        Hp -= damage;
        gameObject.GetComponent<PlayerAudio>().DamageAudio();
        if (Hp < 0) Hp = 0;
        //反应在UI上
        hpBarSlider.ChangeValue(Hp);
        if (Hp <= 0)
        {
            //播放死亡音效
            gameObject.GetComponent<PlayerAudio>().DieAudio();
            //角色死亡，游戏结束，调用关卡管理器的游戏结束事件
            anima.SetBool("die", true);
            if (GameManager.instance.gameStatus != GameStatus.GameOver)
            {
                GameManager.instance.GameOver();
            }
        }
        //还没死则播放受伤音效

    }

    /// <summary>
    /// 获得经验值
    /// </summary>
    /// <param name="exp"></param>
    public void GetExp(int exp)
    {
        EXP += exp;
        if (EXP >= EXP_max)
        {
            //将多出的经验值加上，然后升级
            int remain = EXP_max - EXP;
            EXP += remain;
            LevelUp();
        }
        //反应在UI上
        expBarSlider.ChangeValue(EXP);
    }

    /// <summary>
    /// 角色升级
    /// </summary>
    public void LevelUp()
    {
        //等级增加，血量恢复
        Level++;
        if(Level < 5) levelNumTxt.text = Level.ToString();
        else levelNumTxt.text = "MAX";
        EXP_max = Level * 100 + 100;
        Hp = Hp_max;//Hp_max随等级如何更改，之后多番测试后决定
        //反应在UI上
        hpBarSlider.ChangeValue(Hp);
        expBarSlider.ChangeMaxValue(EXP_max);
        LevelUpTxt.instance.Show();
        //调用蓝图系统，发明新的图纸
        BluePrintPanel.instance.InventNewTower();
    }

    /// <summary>
    /// 加零件
    /// </summary>
    /// <param name="num"></param>
    public void AddComponent(int num)
    {
        Component += num;
        componentText.text = Component.ToString();
    }
}
