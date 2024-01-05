using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerUI : MonoBehaviour
{
    public Image healthBarForegroundImage, staminaBarForegroundImage;
    public TextMeshProUGUI maxHealthNumberUI;
    public TextMeshProUGUI currentHealthNumberUI;
    [HideInInspector] public CombatController combatController;
    [HideInInspector] public PlayerHealth playerHealth;
    [HideInInspector] public PlayerStamina playerStamina;
    public GameObject portalInteractUI;
    [HideInInspector] float healthBarInitialWidth, staminaBarInitialWidth;

    GameObject playerHealthBarParentGameObject, PlayerStaminaBarParentGameObject;
    private void Awake()
    {
        combatController = GetComponentInParent<CombatController>();
        playerHealth = GetComponentInParent<PlayerHealth>();
        playerStamina = GetComponentInParent<PlayerStamina>();
    }
    private void Start()
    {
        maxHealthNumberUI.text = ("/ ") + playerHealth.maxHealth.ToString();
        currentHealthNumberUI.text = playerHealth.currentHealth.ToString();
        playerHealthBarParentGameObject = transform.Find("PlayerHealthBar").gameObject;
        PlayerStaminaBarParentGameObject = transform.Find("PlayerStaminaBar").gameObject;
        healthBarInitialWidth = playerHealthBarParentGameObject.transform.Find("PlayerHealthBarForeground").GetComponent<RectTransform>().rect.width;
        staminaBarInitialWidth = PlayerStaminaBarParentGameObject.transform.GetComponent<RectTransform>().rect.width;
    }
    void LateUpdate()
    {

    }
    private void Update()
    {
        ShowPortalInteractionUI();
    }

    public void SetHealthBarPercentage(float currentHealth, float maxHealth)
    {
        float width = healthBarInitialWidth * (currentHealth / maxHealth);
        healthBarForegroundImage.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, width);
        string updatedNumber = currentHealth.ToString();
        currentHealthNumberUI.text = updatedNumber;
    }
    public void SetBlockStaminaBar(float currentStamina, float maxStamina)
    {
        float width = staminaBarInitialWidth * (currentStamina / maxStamina);
        staminaBarForegroundImage.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, width);
    }
    void ShowPortalInteractionUI()
    {
        portalInteractUI.SetActive(combatController.inPortalRadius);
    }
}
