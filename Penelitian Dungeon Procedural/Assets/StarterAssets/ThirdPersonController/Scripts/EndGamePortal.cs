using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndGamePortal : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (other.transform.TryGetComponent(out CombatController combatController))
            {
                combatController.inPortalRadius = true;
                Debug.Log("In Portal Radius");
            }

        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (other.transform.TryGetComponent(out CombatController combatController))
            {
                combatController.inPortalRadius = false;
                Debug.Log("Out Of Portal Radius");
            }

        }
    }
}
