using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Play : MonoBehaviour
{
    public int Level = 1;       //等级
    public float Hp = 500f;        //生命值
    public float Hp_max = 500f;    //最大生命值
    public float EXP = 300f;        //魔法值
    public float EXP_max = 300f;    //最大魔法值
    public int Component = 0;

    public GameObject Tower;
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
    public bool ismove=false;
    public bool isbuild = false;
    public Animator anima;
    private bool CanMove = true;
    Ray ray;
    RaycastHit hit;

    void Start()
    {
        anima = GetComponent<Animator>();
        speed = 30f;
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        transform.localEulerAngles = new Vector3(0.0f, transform.localEulerAngles.y, 0.0f);
        if (Input.GetKeyDown(KeyCode.Q) && isbuild == false)
        {
            Beforebuild();
        }
        if (havetower == true)      //预制跟随鼠标
        {
            newTower.transform.position = new Vector3(MousePoint.x, MousePoint.y + 2f, MousePoint.z);
        }

        if (Input.GetMouseButtonDown(0) && havetower == true)       //确定建立
        {
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
            anima.SetBool("run", true);
            if(havetower==true)
            {
                Destroy(newTower);
                havetower = false;
            }
            GetTarget();
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
    }

    void GetMouse()
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
            MousePoint = hit.point;
        }
    }

    /// <summary>
    /// 根据快捷键获取塔的信息，动态加载塔的预制体
    /// </summary>
    /// <param name="towerInfo"></param>
    public void Beforebuild()
    {
        if (havetower == false)
        {
            GetMouse();
            newTower = GameObject.Instantiate(Tower, MousePoint, b) as GameObject;
            newTower.GetComponent<Renderer>().material.color = new Color(0f, 1f, 0.878356f, 0.5f);
            
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
        if (distance > 15f)
        {
            anima.SetBool("run", true);
            transform.Translate(Vector3.forward * speed * Time.deltaTime);
        }
        else
        {
            anima.SetBool("attack1", true);
            CanMove = false;
            anima.SetBool("run", false);
            Invoke("BuildEnd", 2);
        }
    }

    void BuildEnd()
    {
        newTower.GetComponent<Renderer>().material.color = new Color(0f, 1f, 0.878356f, 0.5f);
        isbuild = false;
        CanMove = true;
        anima.SetBool("attack1", false);
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
        //反应在UI上
        if(Hp <= 0)
        {
            //角色死亡，游戏结束，调用关卡管理器的游戏结束事件

        }
    }
}
