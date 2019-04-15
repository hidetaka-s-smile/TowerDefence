using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class address : MonoBehaviour
{
    public Transform tf;
    public Transform tf1;
    public GameObject[] prefabs;


    // Use this for initialization
    void Start()
    {

        InvokeRepeating("setBarrack", 1, 3f);
    }

    // Update is called once per frame
    void Update()
    {

    }




    void setBarrack()
    {

        Bound bound = getBound(tf);
        Vector3 pos = new Vector3(bound.getRandomX(), bound.y, bound.getRandomZ());
        Instantiate(prefabs[0], pos, Quaternion.identity);
    }


    Bound getBound(Transform tf)
    {
        Vector3 center = tf.GetComponent<Transform>().position;
        Vector3 extents = tf1.GetComponent<Transform>().position;
        Vector3 dL = new Vector3(center.x - extents.x, center.y, center.z - extents.z);
        Vector3 dR = new Vector3(center.x + extents.x, center.y, center.z - extents.z);
        Vector3 sR = new Vector3(center.x + extents.x, center.y, center.z + extents.z);
        Vector3 sL = new Vector3(center.x - extents.x, center.y, center.z + extents.z);
        Bound bound = new Bound(dL, dR, sR, sL, center.y);


        return bound;
    }


    class Bound
    {
        public Vector3 dL;
        public Vector3 dR;
        public Vector3 sR;
        public Vector3 sL;
        public float y;


        public Bound(Vector3 dL, Vector3 dR, Vector3 sR, Vector3 sL, float y)
        {
            this.dL = dL;
            this.dR = dR;
            this.sR = sR;
            this.sL = sL;
            this.y = y;
        }




        public float getRandomX()
        {
            float num = Random.Range(dL.x, dR.x);
            return num;
        }


        public float getRandomZ()
        {
            float num = Random.Range(dL.z, sL.z);
            return num;
        }






    }

}
