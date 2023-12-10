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
    public void TakeDamage(float amount)
    {
        if (agent.stateMachine.currentState != AIStateID.BlockingState)
        {
            if (agent.stateMachine.currentState != AIStateID.EBA_BlockingState)
            {
                currentHealth -= amount;
                healthBar.SetHealthBarPercentage(currentHealth / maxHealth);
                agent.TakingDamage = true;
                if (currentHealth <= 0.0f)
                {
                    Die();
                }
            }
        }

        if (agent.stateMachine.currentState == AIStateID.BlockingState || agent.stateMachine.currentState == AIStateID.EBA_BlockingState)
        {
            agent.BlockNow = true;
        }
    }
    public void Die()
    {
        DeathState deathState = agent.stateMachine.GetState(AIStateID.DeathState) as DeathState;
        agent.stateMachine.ChangeState(AIStateID.DeathState);
    }
}
