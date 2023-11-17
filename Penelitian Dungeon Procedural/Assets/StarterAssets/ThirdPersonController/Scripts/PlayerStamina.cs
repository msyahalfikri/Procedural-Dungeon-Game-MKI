using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStamina : MonoBehaviour
{
    public float maxStamina = 100f;
    [HideInInspector] public float currentStamina;
    public float staminaDecay = 2.0f;
    public float staminaRegen = 4.0f;
    public float staminaRegenDelay = 2.0f;
    public bool staminaIsFull = false;
    CombatController combatController;
    PlayerUI playerUI;
    // Start is called before the first frame update
    private void Awake()
    {
        combatController = GetComponentInParent<CombatController>();
        playerUI = GetComponentInChildren<PlayerUI>();
    }
    void Start()
    {
        currentStamina = maxStamina;
    }
    private void Update()
    {
        CalculateStamina();
        playerUI.SetBlockStaminaBar(currentStamina, maxStamina);
    }
    public void CalculateStamina()
    {
        if (combatController.isBlocking)
        {
            // Decrease stamina while blocking
            staminaIsFull = false;
            currentStamina -= staminaDecay * Time.deltaTime;

            if (currentStamina <= 0)
            {
                currentStamina = 0;
                combatController.isExhausted = true;
                Debug.Log("Stamina Depleted!");
            }

            StopAllCoroutines();
        }
        else
        {
            // If not blocking, wait for the regen delay to pass before starting stamina regenerate
            StartCoroutine(StartStaminaRegenDelay());
        }
        if (staminaIsFull)
        {
            combatController.isExhausted = false;
        }

    }

    IEnumerator StartStaminaRegenDelay()
    {
        yield return new WaitForSeconds(staminaRegenDelay);
        currentStamina += staminaRegen * Time.deltaTime;
        currentStamina = Mathf.Clamp(currentStamina, 0, maxStamina);
        if (currentStamina >= maxStamina)
        {
            staminaIsFull = true;
        }
    }

    public void DecreaseCurrentStamina(float value)
    {
        if (combatController.isBlocking)
        {
            currentStamina -= value;
        }
    }
}
