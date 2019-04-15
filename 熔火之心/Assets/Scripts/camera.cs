using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camera : MonoBehaviour
{
    public GameObject sphere;
    private Vector3 offset;
    // Use this for initialization
    void Start()
    {
        offset = transform.position - sphere.transform.position;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        transform.position = sphere.transform.position + offset;
    }
}
