using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIHealth : MonoBehaviour
{
    public float maxHealth;
    public float currentHealth;
    AIRagdoll ragdoll;
    // Start is called before the first frame update
    void Start()
    {
        ragdoll = GetComponent<AIRagdoll>();
        currentHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void Takedamage(float amount)
    {
        currentHealth -= amount;
        if (currentHealth <= 0.0f)
        {
            Die();
        }
    }
    public void Die()
    {
        ragdoll.ActivateRagdoll();
    }
}
