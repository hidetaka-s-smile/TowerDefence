using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Burner : MonoBehaviour
{
    public GameObject gear;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Creat()
    {
        GameObject NewGear = GameObject.Instantiate(gear, new Vector3(transform.position.x, transform.position.y + 5f, transform.position.z + 5f), Quaternion.Euler(0.0f, 0.0f, 90.0f)) as GameObject;
        NewGear.GetComponent<Gear>().num = 50;
    }
}
