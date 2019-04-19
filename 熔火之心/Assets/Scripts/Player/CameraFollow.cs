using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CameraFollow : MonoBehaviour {

    public float scrollSpeed = 5f;//鼠标滚轮灵敏度
    public float rotateSpeed = 5f;//转动屏幕灵敏度
    public float minDistance = 5f; //摄像机最近距离
    public float maxDistance = 15f;//摄像机最远距离

    private Transform player;
    private Vector3 offset;//玩家指向摄像机的偏移量
    private float distance = 0;//玩家到摄像机的距离

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag(Tags.player).GetComponent<Transform>();
        offset = transform.position - player.position;
        transform.LookAt(player);
    }

    private void Update()
    {
        //跟随玩家       
        transform.position = player.position + offset;
        //滚轮控制视野远近
        if (!EventSystem.current.IsPointerOverGameObject())
        {
            ScrollView();
        }
        //按住Alt+鼠标左键旋转屏幕
        RotateView();
    }

    /// <summary>
    ///鼠标滚轮控制视野远近
    /// </summary>
    private void ScrollView()
    {
        distance = offset.magnitude;
        distance -= Input.GetAxis("Mouse ScrollWheel") * scrollSpeed;
        distance = Mathf.Clamp(distance, minDistance, maxDistance);
        offset = offset.normalized * distance;
    }

    /// <summary>
    /// 按住Alt+鼠标左键旋转屏幕
    /// </summary>
    private void RotateView()
    {
        if (Input.GetKey(KeyCode.LeftAlt) && Input.GetMouseButton(0))
        {
            Vector3 origionPos = transform.position;
            Quaternion origionRot = transform.rotation;

            //绕玩家上下旋转
            transform.RotateAround(player.position, transform.right, -Input.GetAxis("Mouse Y") * rotateSpeed);
            //绕玩家左右旋转
            transform.RotateAround(player.position, player.up, Input.GetAxis("Mouse X") * rotateSpeed);

            //控制视野转向范围
            if (transform.eulerAngles.x > 80 || transform.eulerAngles.x < 0)
            {
                transform.position = origionPos;
                transform.rotation = origionRot;
            }

            offset = transform.position - player.position;                       
        }
    }
}
