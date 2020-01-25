using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private GameObject player;              // Player Object

    private float speedH = 2f;              // Speed of horizontal camera rotation
    private float speedV = 2f;              // Speed of vertical camera rotation

    private float yaw;
    private float pitch;

    private Vector3 initialRot;             // Initial rotation position of camera

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");

        yaw = 0;
        pitch = 0;

        initialRot = transform.localEulerAngles;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            transform.localEulerAngles = initialRot;
        }

        yaw = speedH * Input.GetAxisRaw("Mouse X");
        pitch = -speedV * Input.GetAxisRaw("Mouse Y");

        transform.eulerAngles += new Vector3(pitch, yaw, 0);
    }
}