using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIHealthBar : MonoBehaviour
{
    public Transform target;
    public Vector3 offset;
    public Image backgroundImage;
    public Image foregroundImage;
    public TextMeshProUGUI enemyNameUI;
    public string enemyName;
    AIAgent agent;
    [HideInInspector] float InitialWidth;
    private void Awake()
    {
        foregroundImage = transform.Find("Foreground").GetComponent<Image>();
        backgroundImage = transform.Find("Background").GetComponent<Image>();
        enemyNameUI = transform.Find("EnemyName").GetComponent<TextMeshProUGUI>();
        InitialWidth = transform.Find("Foreground").GetComponent<RectTransform>().rect.width;
    }
    private void Start()
    {
        agent = GetComponentInParent<AIAgent>();
        enemyNameUI.text = enemyName;
    }

    void LateUpdate()
    {
        if (!agent.isInChaseRange)
        {
            foregroundImage.enabled = false;
            backgroundImage.enabled = false;
            enemyNameUI.enabled = false;
        }
        else
        {
            Vector3 direction = (target.position - Camera.main.transform.position).normalized;
            bool isBehind = Vector3.Dot(direction, Camera.main.transform.forward) <= 0.0f;
            foregroundImage.enabled = !isBehind;
            backgroundImage.enabled = !isBehind;
            enemyNameUI.enabled = !isBehind;
            transform.position = Camera.main.WorldToScreenPoint(target.position + offset);
        }

    }

    public void SetHealthBarPercentage(float percentage)
    {
        float width = InitialWidth * percentage;
        foregroundImage.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, width);
    }
}
