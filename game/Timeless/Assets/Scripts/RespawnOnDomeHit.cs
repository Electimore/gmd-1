using System;
using UnityEngine;

public class RespawnOnDomeHit : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GameObject.Find("TimeManager").GetComponent<TimeController>().EndTimeEarly();
        }
    }
}
