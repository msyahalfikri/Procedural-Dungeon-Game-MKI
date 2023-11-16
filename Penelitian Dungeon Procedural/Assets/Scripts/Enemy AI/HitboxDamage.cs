using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitboxDamage : MonoBehaviour
{
    AIHealth enemyHealth;
    private void Start()
    {
        enemyHealth = GetComponentInParent<AIHealth>();
    }
    // Start is called before the first frame update
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Weapon"))
        {
            enemyHealth.TakeDamage(10f);
        }
    }

    // Update is called once per frame
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Debug"))
        {

        }

    }
}
