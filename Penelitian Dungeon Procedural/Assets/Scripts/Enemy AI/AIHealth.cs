using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AIHealth : MonoBehaviour
{
    public float maxHealth;
    public UIHealthBar healthBar;
    public UIHealthBarAndEmotion healthBarAndEmotion;
    [HideInInspector] public float currentHealth;
    AIAgent agent;
    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<AIAgent>();
        currentHealth = maxHealth;
        healthBar = GetComponentInChildren<UIHealthBar>();

        if (healthBar == null)
        {
            healthBarAndEmotion = GetComponentInChildren<UIHealthBarAndEmotion>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (agent.isTakingDamage)
        {
            StartCoroutine(resetTakingDamage());
        }
    }
    public void TakeDamage(float amount)
    {
        if (agent.stateMachine.currentState != AIStateID.BlockingState)
        {
            if (agent.stateMachine.currentState != AIStateID.EBA_BlockingState)
            {
                currentHealth -= amount;
                if (healthBar == null)
                {
                    healthBarAndEmotion.SetHealthBarPercentage(currentHealth / maxHealth);
                }
                else
                {
                    healthBar.SetHealthBarPercentage(currentHealth / maxHealth);
                }

                agent.isTakingDamage = true;
                if (currentHealth <= 0.0f)
                {
                    Die();
                }
            }

        }

        if (agent.stateMachine.currentState == AIStateID.BlockingState || agent.stateMachine.currentState == AIStateID.EBA_BlockingState)
        {
            agent.blockNow = true;
        }
    }
    public void Die()
    {
        if (agent.isEmotionBasedAgent)
        {
            EBA_DeathState EBA_deathState = agent.stateMachine.GetState(AIStateID.EBA_DeathState) as EBA_DeathState;
            agent.stateMachine.ChangeState(AIStateID.EBA_DeathState);
        }
        else
        {
            DeathState deathState = agent.stateMachine.GetState(AIStateID.DeathState) as DeathState;
            agent.stateMachine.ChangeState(AIStateID.DeathState);
        }

    }

    IEnumerator resetTakingDamage()
    {
        yield return new WaitForEndOfFrame();
        agent.isTakingDamage = false;
    }
}
