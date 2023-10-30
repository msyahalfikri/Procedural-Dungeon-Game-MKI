using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIHealth : MonoBehaviour
{
    public float maxHealth;
    public UIHealthBar healthBar;
    [HideInInspector] public float currentHealth;
    AIAgent agent;
    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<AIAgent>();
        currentHealth = maxHealth;
        healthBar = GetComponentInChildren<UIHealthBar>();
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void Takedamage(float amount)
    {
        currentHealth -= amount;
        healthBar.SetHealthBarPercentage(currentHealth / maxHealth);
        if (currentHealth <= 0.0f)
        {
            Die();
        }
    }
    public void Die()
    {
        DeathState deathState = agent.stateMachine.GetState(AIStateID.DeathState) as DeathState;
        agent.stateMachine.ChangeState(AIStateID.DeathState);
    }
}
