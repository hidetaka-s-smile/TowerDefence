using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tower : MonoBehaviour
{
    public List<GameObject> enemys = new List<GameObject>();
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "emeny")
        {
            enemys.Add(other.gameObject);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "emeny")
        {
            enemys.Remove(other.gameObject);
        }
    }
    public GameObject bulletPrefeb;
    public float attackRateTime = 1;
    private float timer = 0;
    public GameObject bul;
    public Transform firepostion;
    public Transform head;
    void Start()
    {
        
    }
    void Update()
    {
        if (enemys.Count > 0 && enemys[0]!=null)
        {
            Vector3 targetPosition = enemys[0].transform.position;
            targetPosition.y = head.position.y;
            head.LookAt(enemys[0].transform.position);
        }
        timer += Time.deltaTime;
        if(enemys.Count>0&&timer>attackRateTime && enemys[0] != null)
        {
            timer -= attackRateTime;
            Attack();
        }
    }
    void Attack()
    {
        GameObject bullet= GameObject.Instantiate(bulletPrefeb, firepostion.position, firepostion.rotation);
        bullet.GetComponent<bullet>().SetTarget(enemys[0].transform);
    }
}
