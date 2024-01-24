using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CallRobotConfig : MonoBehaviour
{
    /// <summary>
    /// true if the robot has been called
    /// </summary>
    public bool CallRobotAsked { get; private set; }
    public Vector3 TargetPoint { get; private set; }

    // Start is called before the first frame update
    void Start()
    {
        CallRobotAsked = false;
    }



    // Update is called once per frame
    public void CallMe(Vector3 position)
    {
        TargetPoint = position;
        CallRobotAsked = true; 
    }

    public void ResetCall()
    {
        CallRobotAsked = false;
    }


    public bool HasReachedPoint()
    {
        return (Vector3.Distance(transform.position, TargetPoint) <= 0.10f) ? true : false;
    }

    public Vector3 GetTargetPointDirection()
    {
        return (TargetPoint - transform.position).normalized;
    }
}
