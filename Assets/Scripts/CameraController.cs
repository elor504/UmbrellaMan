﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField]
    Vector2 maxLimit;
    Vector2 minLimit;
    public Vector3 offset;
    [SerializeField]
    Transform playerPos;

    float xClamp;
    float yClamp;
 
    public void UpdateCameraPos(Transform playerpos)
    {
        this.transform.position = playerpos.transform.position +offset;
        Mathf.Clamp(this.transform.position.x, minLimit.x, maxLimit.x);
        Mathf.Clamp(this.transform.position.y, minLimit.y, maxLimit.y); 
    }

}
