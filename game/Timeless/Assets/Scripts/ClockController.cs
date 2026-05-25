using System;
using System.Collections.Generic;
using UnityEngine;

public class ClockController : MonoBehaviour
{
    [SerializeField]
    List<Vector3> validRotations;

    [SerializeField]
    GameObject clockInside;

    // Update is called once per frame
    void Update()
    {
        var face = ((int)Math.Floor(Time.timeSinceLevelLoad/10)) % 9;
        if (clockInside.transform.rotation != Quaternion.Euler(validRotations[face]))
        {
            clockInside.transform.rotation = Quaternion.Slerp(clockInside.transform.rotation, Quaternion.Euler(validRotations[face]), 2*Time.deltaTime);
        }
    }
}
