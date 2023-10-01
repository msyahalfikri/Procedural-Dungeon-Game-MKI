using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class ChaseSensor : MonoBehaviour
{

    public delegate void PlayerEnterEvent(Transform player);
    public delegate void PlayerExitEvent(Vector3 lastKnowPosition);

    public event PlayerEnterEvent onPlayerEnter;
    public event PlayerExitEvent onPlayerExit;
    private Vector3 lastKnownPlayerPosition;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player detected");
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player left");
            lastKnownPlayerPosition = other.transform.position;
            onPlayerExit?.Invoke(lastKnownPlayerPosition);
        }
    }
}
