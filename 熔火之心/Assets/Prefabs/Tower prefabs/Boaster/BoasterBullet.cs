using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoasterBullet : MonoBehaviour
{
    public int damage;//炮弹伤害300
    public Transform bombpostion;//爆炸地点
    public GameObject Bombprafeb; //爆炸
    public int speed = 15;
    private Transform target;
    public void SetTarget(Transform _target)
    {
        this.target = _target;
    }
    public const float g = 9.8f;
    private float verticalSpeed;
    private Vector3 moveDirection;

    private float angleSpeed;
    private float angle;
    private float distance;
    public float distanceMax;
    void Start()
    {
        //抛物线算法
        distanceMax = 30;
        float tmepDistance = Vector3.Distance(transform.position, target.transform.position);
        float tempTime = tmepDistance / speed;
        float riseTime, downTime;
        riseTime = downTime = tempTime / 2;
        verticalSpeed = g * riseTime;
        transform.LookAt(target.transform.position);

        float tempTan = verticalSpeed / speed;
        double hu = Mathf.Atan(tempTan);
        angle = (float)(180 / Mathf.PI * hu);
        transform.eulerAngles = new Vector3(-angle, transform.eulerAngles.y, transform.eulerAngles.z);
        angleSpeed = angle / riseTime;

        moveDirection = target.transform.position - transform.position;
    }
    private float time;
    void Update()
    {
        //距离远时用抛物线 近时直接跟踪
        distance= Vector3.Distance(transform.position, target.transform.position);
        if(distance>=distanceMax)
        {
            transform.LookAt(target.transform.position);
            time += Time.deltaTime;
            float test = verticalSpeed - g * time;
            transform.Translate(moveDirection.normalized * speed * Time.deltaTime, Space.World);
            transform.Translate(Vector3.up * test * Time.deltaTime, Space.World);
            float testAngle = -angle + angleSpeed * time;
            transform.eulerAngles = new Vector3(testAngle, transform.eulerAngles.y, transform.eulerAngles.z);
        }
        else
        {
            transform.LookAt(target.position);
            transform.Translate(Vector3.forward * speed * Time.deltaTime);
        }
        if(target==null)
        {
            Die();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Plane")
        {
            GameObject Bomb = GameObject.Instantiate(Bombprafeb, bombpostion.position, bombpostion.rotation);
            Die();
            return;
        }
        if (other.tag == "Enemy")
        {

            GameObject Bomb = GameObject.Instantiate(Bombprafeb, bombpostion.position, bombpostion.rotation);
            Die();
            other.GetComponent<EnemyStatusInfo>().Damage(damage);
            return;
        }
    }
    //销毁子弹
    public void Die()
    {
        Destroy(this.gameObject);
    }

}
