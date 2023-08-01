using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FaceTowardGameObject : MonoBehaviour
{
    [SerializeField] private Transform targetTransform;
    public bool startFacingTarget = true;
    private void Update() 
    {
        if(startFacingTarget)
        {
            FaceTarget();
        }
    }

    private void FaceTarget()
    {
        Vector2 lookDirection = targetTransform.position - transform.position;
        float angle = Mathf.Atan2(lookDirection.y, lookDirection.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle);    
    }
}
