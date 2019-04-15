using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class play : MonoBehaviour
{
    public int level = 1;       //等级
    public int hp = 500;        //生命值
    public int hp_max = 500;    //最大生命值
    public int mp = 300;        //魔法值
    public int mp_max = 300;    //最大魔法值
    public GameObject Tower;
    private Transform m_Transform;
    private Quaternion b = new Quaternion(0, 0, 0, 0);
    private Vector3 mousePositionOnScreen;
    private Vector3 targetPoint = Vector3.zero;     //鼠标点击的位置
    private Vector3 towerPoint = Vector3.zero;
    private CharacterController controller;
    private GameObject newTower;
    public bool havetower = false;
    public int cd;
    public float speed;
    public bool ifcd;
    public bool ismove=false;
    public bool isbuild = false;
    public Animator anima;
    Ray ray;
    RaycastHit hit;

    void Start()
    {
        anima = GetComponent<Animator>();
        speed = 5f;
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        transform.localEulerAngles = new Vector3(0.0f, transform.position.y, 0.0f);
        if (Input.GetKeyDown(KeyCode.Q) && ifcd == false)
        {
            Beforebuild();
        }
        if (havetower == true)      //预制跟随鼠标
        {
            newTower.transform.position = new Vector3(targetPoint.x, targetPoint.y + 1f, targetPoint.z);
        }

        if (Input.GetMouseButtonDown(0) && havetower == true)       //确定建立
        {
            towerPoint = targetPoint;
            Startbuild();
        }

        if (ifcd == true)       //冷却
        {
            cd++;
            if (cd > 100)
            {
                ifcd = false;
                cd = 0;
            }
        }
        if (isbuild == true)        //开始建立
        {
            MoveBuild();
            anima.SetBool("attack", false);
        }

        if (Input.GetMouseButtonDown(1))        //移动
        {
            ismove = true;
            anima.SetBool("run", true);
            GetMouse();
        }
        if(ismove == true)
        {
            Move();
        }

        if(ismove == false)        //实时获取鼠标位置 
        {
            GetMouse();
        }
    }


    void Move()
    {
        transform.LookAt(new Vector3(targetPoint.x, targetPoint.y + 0.5f, targetPoint.z));
        float distance = Vector3.Distance(targetPoint, transform.position);
        if (distance > 1f)
        {
            transform.Translate(Vector3.forward * speed * Time.deltaTime);
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
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit))
        {
            //判断点击的是否地形  
            if (!hit.collider.tag.Equals("Plane")&&!hit.collider.tag.Equals("Player")&& !hit.collider.tag.Equals("tower"))
            {
                return;
            }
            //点击位置坐标   
            targetPoint = hit.point;
        }
    }

    void Beforebuild()
    {
        if (havetower == false)
        {
            newTower = GameObject.Instantiate(Tower, targetPoint, b) as GameObject;
            newTower.GetComponent<Renderer>().material.color = new Color(0f, 0.4894f, 1.0f, 0.5f);
            havetower = true;
        }
    }

    void Startbuild()
    {
        havetower = false;
        ifcd = true;
        isbuild = true;
    }

    /// <summary>
    /// 移动去建造
    /// </summary>
    void MoveBuild()
    {
        transform.LookAt(new Vector3(towerPoint.x, towerPoint.y+0.5f, towerPoint.z));
        float distance = Vector3.Distance(towerPoint, transform.position);//计算目标位置到当前位置
        if (distance > 1.5f)
        {
            anima.SetBool("run", true);
            transform.Translate(Vector3.forward * speed * Time.deltaTime);
        }
        if (distance <= 1.5f)
        {
            anima.SetBool("run", false);
            anima.SetBool("attack", true);
            newTower.GetComponent<Renderer>().material.color = new Color(0f, 0.4894f, 1.0f, 1f);
            newTower = null;
            isbuild = false;
        }
    }
}
