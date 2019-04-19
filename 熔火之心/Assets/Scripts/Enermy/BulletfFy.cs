using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletfFy : MonoBehaviour
{
    public int moveSpeed = 60;
    Vector3 theTarget;
    public void ini(Vector3 target)
    {
        theTarget = target;
    }
    private void Movement()
    {
        transform.position = Vector3.MoveTowards(transform.position, theTarget, Time.deltaTime * moveSpeed);
    }
    private void Update()
    {
        Movement();
        if ((transform.position - theTarget).sqrMagnitude < 1)
        { 
            Destroy(gameObject);
        }
    }
}
