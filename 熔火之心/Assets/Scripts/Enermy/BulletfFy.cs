using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletfFy : MonoBehaviour
{
    public int moveSpeed = 60;
    Transform theTarget;
    private int theAtkValue ;
    public void ini(Transform target,int atkValue)
    {
        theAtkValue = atkValue;
        theTarget = target;
        Destroy(this.gameObject,2f);
    }
    private void Movement()
    {
        if (theTarget == null) Destroy(this);
        transform.position = Vector3.MoveTowards(transform.position,theTarget.position, Time.deltaTime * moveSpeed);
    }
    private void Update()
    {
        Movement();
        if (Vector3.Distance(transform.position ,theTarget.position) < 10)
        {
            Destroy(this.gameObject);
            if (theTarget.tag == Tags.player) 
              theTarget.GetComponent<Player>().GetDamage(theAtkValue);
            else theTarget.GetComponent<Tower>().GetDamage(theAtkValue);
            
        }
    }
}
