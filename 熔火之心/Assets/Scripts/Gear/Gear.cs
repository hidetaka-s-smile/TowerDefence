using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gear : MonoBehaviour
{
    public GameObject Player;
    public float speed = 2.5f;
    private Transform P_Transform;
    // Start is called before the first frame update
    void Start()
    {
        P_Transform = Player.GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Vector3.right * speed);
        float distance = Vector3.Distance(P_Transform.position, transform.position);
        if(distance <18f)
        {
            PickUp();
        }
    }
    void PickUp()
    {
        Player.GetComponent<Play>().Component++;
        Destroy(this.gameObject);
    }
}
