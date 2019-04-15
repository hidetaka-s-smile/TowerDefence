using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet : MonoBehaviour
{
    public int speed = 20;
    private Transform target;
    public void SetTarget(Transform _target)
    {
        this.target = _target;
    }
    // Update is called once per frame
    void Update()
    {
        transform.LookAt(target.position);
        transform.Translate(Vector3.forward * speed * Time.deltaTime);

    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag=="emeny")
        {
            Destroy(this.gameObject);
        }
    }
}
