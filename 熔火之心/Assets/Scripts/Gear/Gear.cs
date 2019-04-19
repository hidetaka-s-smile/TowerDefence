using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gear : MonoBehaviour
{
    public GameObject Player;
    public float speed = 2.5f;
    public int num = 0;
    private float distance;
    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Vector3.right * speed);
        print(Player.GetComponent<Transform>().position);
        distance = Vector3.Distance(Player.GetComponent<Transform>().position, transform.position);
        if(distance < 10f)
        {
            PickUp();
        }
    }
    void PickUp()
    {
        Player.GetComponent<Player>().AddComponent(num);
        Destroy(this.gameObject);
    }
}
