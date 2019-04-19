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
    }
    private void Movement()
    {
        transform.position = Vector3.MoveTowards(transform.position, theTarget.position, Time.deltaTime * moveSpeed);
    }
    private void Update()
    {
        Movement();
        if ((transform.position - theTarget.position).sqrMagnitude < 1)
        { 
            if(theTarget.tag == Tags.player)
                theTarget.GetComponent<Player>().GetDamage(theAtkValue);
            else theTarget.GetComponent<Tower>().GetDamage(theAtkValue);
        }
    }
}
