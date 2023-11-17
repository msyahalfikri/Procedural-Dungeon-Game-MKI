using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamageDealer : MonoBehaviour
{
    bool canDealDamage;
    public bool hitRegistered;
    List<GameObject> hasDealtDamage;
    [SerializeField] float weaponLength;
    AIAgent agent;
    void Start()
    {
        canDealDamage = false;
        hasDealtDamage = new List<GameObject>();
        agent = GetComponentInParent<AIAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        if (canDealDamage)
        {
            RaycastHit hit;
            int layerMask = 1 << 3;
            if (Physics.Raycast(transform.position, -transform.up, out hit, weaponLength, layerMask))
            {
                hitRegistered = true;

                //Deal damage to player
                if (hit.transform.TryGetComponent(out PlayerHealth playerHealth) && !hasDealtDamage.Contains(hit.transform.gameObject))
                {
                    DecideDamageDealt(playerHealth);
                }

                //Decrease player stamina when the player is blocking
                if (hit.transform.TryGetComponent(out PlayerStamina playerStamina) && !hasDealtDamage.Contains(hit.transform.gameObject))
                {

                    DecideStaminaDamageDealt(playerStamina);
                }

                hasDealtDamage.Add(hit.transform.gameObject);
            }
        }
    }
    public void DecideDamageDealt(PlayerHealth playerHealthComponent)
    {
        if (agent.attackLeft || agent.attackRight)
        {
            playerHealthComponent.TakeDamage(agent.config.normalAttackDamage);
        }
        else if (agent.heavyAttack)
        {
            playerHealthComponent.TakeDamage(agent.config.heavyAttackDamage);
        }
    }

    public void DecideStaminaDamageDealt(PlayerStamina playerStaminaComponent)
    {
        if (agent.attackLeft || agent.attackRight)
        {
            playerStaminaComponent.DecreaseCurrentStamina(agent.config.normalAttackDamage);
        }
        else if (agent.heavyAttack)
        {
            playerStaminaComponent.DecreaseCurrentStamina(agent.config.heavyAttackDamage);
        }
    }

    public void EnemyStartDealDamage()
    {
        canDealDamage = true;
        hasDealtDamage.Clear();
    }

    public void EnemyEndDealDamage()
    {
        canDealDamage = false;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(transform.position, transform.position - transform.up * weaponLength);
    }
}
