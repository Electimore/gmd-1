using System;
using UnityEngine;

public class RespawnOnDomeHit : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        Debug.LogError("DIE");
        if (other.tag == "Player")
        {
            GameObject.Find("TimeManager").GetComponent<TimeController>().EndTimeEarly();
        }
    }
}
