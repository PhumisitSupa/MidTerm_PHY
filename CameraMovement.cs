using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    private const float SMOOTH_TIME = 0.3f;
    public bool LockX, LockY, LockZ;
    public float offSetZ = -9.52f;
    public bool useSmooth = true;
    public Transform target;
    private Vector3 velocity;
    private Transform thisTransform;
    void Awake()
    {
        thisTransform = transform;
        velocity = new Vector3(0.5f, 0.5f, 0.5f);
    }

    
    void Update()
    {
        var newPos = Vector3.zero;
        if (useSmooth)
        {
            newPos.x = Mathf.SmoothDamp(thisTransform.position.x, target.position.x,ref velocity.x,SMOOTH_TIME);
            newPos.y = Mathf.SmoothDamp(thisTransform.position.y, target.position.y,ref velocity.y,SMOOTH_TIME);
            newPos.z = Mathf.SmoothDamp(thisTransform.position.z, target.position.z + offSetZ,ref velocity.y,SMOOTH_TIME);
        }
        else
        {
            newPos.x = target.position.x;
            newPos.y = target.position.y;
            newPos.z = target.position.z + offSetZ;
        }

        if (LockX)
        {
            newPos.x = thisTransform.position.x;
        }
        if (LockY)
        {
            newPos.y = thisTransform.position.y;
        }
        if (LockZ)
        {
            newPos.z = thisTransform.position.z;
        }

        transform.position = Vector3.Slerp(thisTransform.position, newPos, Time.time);
    }
}
