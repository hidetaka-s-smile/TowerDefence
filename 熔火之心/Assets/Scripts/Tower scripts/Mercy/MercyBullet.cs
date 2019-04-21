using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MercyBullet : Bullet
{
    public void Start()
    {
        Destroy(this.gameObject, 3);
    }
    public override void Update()
    {
        
        
      transform.Translate(Vector3.forward * speed * Time.deltaTime);
       

    }
    public override void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy" || other.tag == "Boss")
        {
           
            other.GetComponent<EnemyStatusInfo>().Damage(damage);
            print("1");
        }
    }
}
    
    
