using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{

    [SerializeField] float minvalue;
    [SerializeField] float maxvalue;


    void Update()
    {
        var balls = GameObject.FindGameObjectsWithTag("Ball");
        if(balls.Length > 0)
        {
            var camPos = transform.position;
            camPos.x = balls[0].transform.position.x;
            camPos.x = Mathf.Clamp(camPos.x, minvalue, maxvalue);
            transform.position = Vector3.Lerp(transform.position,camPos,.03f);
        }

    }
}
